using LibraryManager.Application.DTOs.User.Input;
using LibraryManager.Application.DTOs.User.Output;
using LibraryManager.Application.Responses;
using LibraryManager.Application.Services.Interfaces;
using LibraryManager.Domain.Interfaces;
using LibraryManager.Data.Helper;
using LibraryManager.Application.Utils;
using LibraryManager.Application.Utils.Mappings;
using LibraryManager.Application.Enums;

namespace LibraryManager.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<APIResponse<CreateUserDTO>> RegisterUser(CreateUserDTO modelDTO)
        {

            var passwordHash = HashPassword.CreatePasswordHash(modelDTO.Password);
            var model = modelDTO.ToModelWithPasswordHash(passwordHash);

            await _userRepository.RegisterUser(model);

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
            var models = await _userRepository.GetAllUsers();

            var DTOs = models.ToViewDTOList();

            return new APIResponse<ViewUserDTO>(
                operationType: EOperationType.Get.ToString(),
                true,
                codeReponse: 200,
                message: "Listing all registered users successfully!",
                dataResponse: null,
                dataResponseList: DTOs!
                );
        }

        public async Task<APIResponse<ViewUserDTO>> GetUserById(long id)
        {
            var model = await _userRepository.GetUserByIdAsNoTracking(id);

            if(model is null)
                return new APIResponse<ViewUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               false,
               codeReponse: 404,
               message: "The user is not found!"
               );

            var DTO = model.ToViewDTO();

            return new APIResponse<ViewUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               true,
               codeReponse: 200,
               message: "Listing user with specify id successfully!",
               dataResponse: DTO,
               dataResponseList: null
               );

        }

        public async Task<APIResponse<UpdateInputUserDTO>> UpdateUser(long id, UpdateInputUserDTO model)
        {
            var updateModel = await _userRepository.GetUserById(id);

            if (updateModel is null)
                return new APIResponse<UpdateInputUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               false,
               codeReponse: 404,
               message: "The user is not found!"
               );

            updateModel.FirstName = model.FirstName;
            updateModel.LastName = model.LastName;
            updateModel.Email = model.Email;
            updateModel.PasswordHash = model.Password;

            await _userRepository.UpdateUser(updateModel);

            return new APIResponse<UpdateInputUserDTO>(
                operationType: EOperationType.Update.ToString(),
                true,
                codeReponse: 200,
                message: "User updated successfully!",
                dataResponse: model,
                dataResponseList: null
                );

        }

        public async Task<APIResponse<ViewUserDTO>> DeleteUser(long id)
        {
            var removeModel = await _userRepository.GetUserById(id);

            if (removeModel is null)
                return new APIResponse<ViewUserDTO>(
               operationType: EOperationType.GetById.ToString(),
               false,
               codeReponse: 404,
               message: "The user is not found!"
               );

           await _userRepository.DeleteUser(removeModel);

            return new APIResponse<ViewUserDTO>(
               operationType: EOperationType.Delete.ToString(),
               true,
               codeReponse: 200,
               message: "User removed successfully!"
               );

        }

        public async Task<APIResponse<ViewValidateCredentialsUserDTO>> ValidateUserCredentials(ValidateCredentialsUserDTO DTOrequest)
        {
            var model = await _userRepository.GetUserByTerm(x => x.FirstName + " " + x.LastName == DTOrequest.Name
                && x.Email == DTOrequest.Email);

            if(model is null || model.Count == 0)
                return new APIResponse<ViewValidateCredentialsUserDTO>(
              operationType: EOperationType.GetById.ToString(),
              false,
              codeReponse: 404,
              message: "The user is not found"
              );

            bool isCorretPassword = HashPassword.VerifyPassword(DTOrequest.Password, model.First().PasswordHash);

            if (isCorretPassword == false)
                return new APIResponse<ViewValidateCredentialsUserDTO>(
              operationType: EOperationType.ValidateCredentials.ToString(),
              false,
              codeReponse: 401,
              message: "Some credential is incorret, try again"
              );

            var DTO = model.First().ToViewValidateCredentialsDTO();

            return new APIResponse<ViewValidateCredentialsUserDTO>(
              operationType: EOperationType.ValidateCredentials.ToString(),
              true,
              codeReponse: 200,
              message: "Credentials validate successfully!",
              dataResponse: DTO,
              dataResponseList: null
              );

        }
    }
}
