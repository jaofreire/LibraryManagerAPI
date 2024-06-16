using LibraryManager.Core.Models;
using LibraryManager.Core.DTOs;
using LibraryManager.Core.DTOs.User.InputModels;
using LibraryManager.Core.DTOs.User.ViewModels;

namespace LibraryManager.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<CreateUserDTO> RegisterUser(CreateUserDTO model);
        Task<List<ViewUserDTO>> GetAllUsers();
        Task<ViewUserDTO> GetUserById(long id);
        Task<ViewValidateCredentialsUserDTO> GetUserByIdValidateCredentials(long id);
        Task<UpdateInputUserDTO> UpdateUser(long id, UpdateInputUserDTO model);
        Task<bool> DeleteUser(long id);
    }
}
