using LibraryManager.Core.DTOs.User.InputModels;
using LibraryManager.Core.DTOs.User.ViewModels;
using LibraryManager.Core.Responses;


namespace LibraryManager.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<APIResponse<CreateUserDTO>> RegisterUser(CreateUserDTO model);
        Task<APIResponse<ViewUserDTO>> GetAllUsers();
        Task<APIResponse<ViewUserDTO>> GetUserById(long id);
        Task<APIResponse<ViewValidateCredentialsUserDTO>> ValidateUserCredentials(ValidateCredentialsUserDTO DTOrequest);
        Task<APIResponse<UpdateInputUserDTO>> UpdateUser(long id, UpdateInputUserDTO model);
        Task<APIResponse<ViewUserDTO>> DeleteUser(long id);
    }
}
