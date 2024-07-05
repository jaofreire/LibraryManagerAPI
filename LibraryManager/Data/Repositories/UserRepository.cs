using Data.Context;
using Data.Services.Utils;
using LibraryManager.Core.DTOs.User.InputModels;
using LibraryManager.Core.DTOs.User.ViewModels;
using LibraryManager.Core.Enums;
using LibraryManager.Core.Interfaces;
using LibraryManager.Core.Models;
using Microsoft.EntityFrameworkCore;


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

        public async Task<CreateUserDTO> RegisterUser(CreateUserDTO modelDTO)
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


            return modelDTO;
        }

        public async Task<List<ViewUserDTO>> GetAllUsers()
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

            return usersDTO;
        }

        public async Task<ViewUserDTO> GetUserById(long id)
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

                return DTOCache;
            }

            var model = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) ??
                throw new Exception("The user is not found");

            await _cacheHandler.SetCacheObject<UserModel>(model.Id.ToString(), model);

            var DTO = new ViewUserDTO()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };

            return DTO;
        }

        public async Task<ViewValidateCredentialsUserDTO> GetUserByIdValidateCredentials(long id)
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

                return DTOCache;
            }

            var model = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) ??
                throw new Exception("The user is not found");

            await _cacheHandler.SetCacheObject<UserModel>(model.Id.ToString(), model);

            var DTO = new ViewValidateCredentialsUserDTO()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.PasswordHash
            };

            return DTO;
        }

        public async Task<UpdateInputUserDTO> UpdateUser(long id, UpdateInputUserDTO modelDTO)
        {
            var modelUpdate = await _dbContext.Users.FindAsync(id) ??
                throw new Exception("The user is not found");

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

            return modelDTO;

        }
        public async Task<bool> DeleteUser(long id)
        {
            var model = await _dbContext.Users.FindAsync(id) ??
                throw new Exception("The user is not found");

            _dbContext.Users.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;

        }
    }
}
