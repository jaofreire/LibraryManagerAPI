using LibraryManager.Application.Responses;
using LibraryManager.Application.DTOs.User.Input;
using LibraryManager.Application.DTOs.User.Output;

namespace LibraryManager.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<APIResponse<CreateUserDTO>> RegisterUser(CreateUserDTO model);
        Task<APIResponse<ViewUserDTO>> GetAllUsers();
        Task<APIResponse<ViewUserDTO>> GetUserById(long id);
        Task<APIResponse<ViewValidateCredentialsUserDTO>> ValidateUserCredentials(ValidateCredentialsUserDTO DTOrequest);
        Task<APIResponse<UpdateInputUserDTO>> UpdateUser(long id, UpdateInputUserDTO model);
        Task<APIResponse<ViewUserDTO>> DeleteUser(long id);
    }
}
