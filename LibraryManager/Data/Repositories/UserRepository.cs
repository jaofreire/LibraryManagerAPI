using Data.Context;
using Data.Services.Utils;
using LibraryManager.Core.DTOs.User.InputModels;
using LibraryManager.Core.DTOs.User.ViewModels;
using LibraryManager.Core.Enums;
using LibraryManager.Core.Interfaces;
using LibraryManager.Core.Models;
using LibraryManager.Core.Responses;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;


namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _dbContext;
        private readonly CacheHandler _cacheHandler;
        private readonly HashPassword _hash;

        public UserRepository(LibraryDbContext dbContext, CacheHandler cacheHandler, HashPassword hash)
        {
            _dbContext = dbContext;
            _cacheHandler = cacheHandler;
            _hash = hash;
        }

        public async Task<APIResponse<CreateUserDTO>> RegisterUser(CreateUserDTO modelDTO)
        {

            var passwordHash = _hash.CreatePasswordHash(modelDTO.Password);

            var model = new UserModel()
            {
                FirstName = modelDTO.FirstName,
                LastName = modelDTO.LastName,
                Email = modelDTO.Email,
                PasswordHash = passwordHash,
                Role = ERoleType.User,
            };

            await _dbContext.Users.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            var modelCache = await _dbContext.Users
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.PasswordHash == passwordHash);

            await _cacheHandler.SetCacheObject<UserModel>(modelCache.Id.ToString(), modelCache);


            return new APIResponse<CreateUserDTO>(
                operationType: EOperationType.Create.ToString(),
                true,
                codeReponse: 200,
                message: "User created successfully!",
                dataResponse: modelDTO,
                dataResponseList: null
                );
        }

        public async Task<APIResponse<ViewUserDTO>> GetAllUsers()
        {
            List<ViewUserDTO> usersDTO = [];

            var models = await _dbContext.Users
                 .AsNoTracking()
                 .ToListAsync();


            foreach (var usersModel in models)
            {
                var DTO = new ViewUserDTO()
                {
                    Id = usersModel.Id,
                    FirstName = usersModel.FirstName,
                    LastName = usersModel.LastName,
                    Email = usersModel.Email
                };

                usersDTO.Add(DTO);
            }

            return new APIResponse<ViewUserDTO>(
                operationType: EOperationType.Get.ToString(),
                true,
                codeReponse: 200,
                message: "Listing all registered users successfully!",
                dataResponse: null,
                dataResponseList: usersDTO!
                );
        }

        public async Task<APIResponse<ViewUserDTO>> GetUserById(long id)
        {
            var modelCache = await _cacheHandler.GetCacheObject<UserModel>(id.ToString());

            if (modelCache != default)
            {
                var DTOCache = new ViewUserDTO()
                {
                    Id = modelCache.Id,
                    FirstName = modelCache.FirstName,
                    LastName = modelCache.LastName,
                    Email = modelCache.Email
                };

                return new APIResponse<ViewUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               true,
               codeReponse: 200,
               message: "Listing user with specify id successfully!",
               dataResponse: DTOCache,
               dataResponseList: null
               );
            }

            var model = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if(model is null)
                return new APIResponse<ViewUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               false,
               codeReponse: 404,
               message: "The user is not found!"
               );


            await _cacheHandler.SetCacheObject<UserModel>(model.Id.ToString(), model);

            var DTO = new ViewUserDTO()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            return new APIResponse<ViewUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               true,
               codeReponse: 200,
               message: "Listing user with specify id successfully!",
               dataResponse: DTO,
               dataResponseList: null
               );
        }

        public async Task<APIResponse<ViewValidateCredentialsUserDTO>> GetUserByIdValidateCredentials(long id)
        {
            var modelCache = await _cacheHandler.GetCacheObject<UserModel>(id.ToString());

            if (modelCache != default)
            {
                var DTOCache = new ViewValidateCredentialsUserDTO()
                {
                    Id = modelCache.Id,
                    FirstName = modelCache.FirstName,
                    LastName = modelCache.LastName,
                    Email = modelCache.Email,
                    PasswordHash = modelCache.PasswordHash

                };

                return new APIResponse<ViewValidateCredentialsUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               true,
               codeReponse: 200,
               message: "Listing user with specify id to validate their credentials successfully!",
               dataResponse: DTOCache,
               dataResponseList: null
               );
            }

            var model = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if(model is null)
                return new APIResponse<ViewValidateCredentialsUserDTO>(
              operationType: EOperationType.GetById.ToString(),
              false,
              codeReponse: 404,
              message: "The user is not found"
              );


            await _cacheHandler.SetCacheObject<UserModel>(model.Id.ToString(), model);

            var DTO = new ViewValidateCredentialsUserDTO()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.PasswordHash
            };

            return new APIResponse<ViewValidateCredentialsUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               true,
               codeReponse: 200,
               message: "Listing user with specify id to validate their credentials successfully!",
               dataResponse: DTO,
               dataResponseList: null
               );
        }

        public async Task<APIResponse<UpdateInputUserDTO>> UpdateUser(long id, UpdateInputUserDTO modelDTO)
        {
            var modelUpdate = await _dbContext.Users.FindAsync(id);

            if(modelUpdate is null)
                return new APIResponse<UpdateInputUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               false,
               codeReponse: 404,
               message: "The user is not found"
               );

            modelUpdate.FirstName = modelDTO.FirstName;
            modelUpdate.LastName = modelDTO.LastName;
            modelUpdate.Email = modelDTO.Email;

            var IsEqual = _hash.VerifyPassword(modelDTO.Password, modelUpdate.PasswordHash);

            if (IsEqual == false)
            {
                var newHashPassword = _hash.CreatePasswordHash(modelDTO.Password);
                modelUpdate.PasswordHash = newHashPassword;
            }

            _dbContext.Users.Update(modelUpdate);
            await _dbContext.SaveChangesAsync();

            await _cacheHandler.SetCacheObject<UserModel>(modelUpdate.Id.ToString(), modelUpdate);

            return new APIResponse<UpdateInputUserDTO>(
               operationType: EOperationType.Update.ToString(),
               true,
               codeReponse: 200,
               message: "User updated successfully!",
               dataResponse: modelDTO,
               dataResponseList: null
               );

        }

        public async Task<APIResponse<ViewUserDTO>> DeleteUser(long id)
        {
            var model = await _dbContext.Users.FindAsync(id);

            if(model is null)
                return new APIResponse<ViewUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               false,
               codeReponse: 404,
               message: "The user is not found"
               );

            _dbContext.Users.Remove(model);
            await _dbContext.SaveChangesAsync();

             return new APIResponse<ViewUserDTO>(
               operationType: EOperationType.Delete.ToString(),
               true,
               codeReponse: 200,
               message: "User removed successfully!"
               );

        }
    }
}
