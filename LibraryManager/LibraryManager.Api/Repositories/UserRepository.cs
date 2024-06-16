using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Core.DTOs.User.InputModels;
using LibraryManager.Core.DTOs.User.ViewModels;

namespace LibraryManager.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        public Task<bool> DeleteUser(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewUserDTO>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<ViewUserDTO> GetUserById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ViewValidateCredentialsUserDTO> GetUserByIdValidateCredentials(long id)
        {
            throw new NotImplementedException();
        }

        public Task<CreateUserDTO> RegisterUser(CreateUserDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateInputUserDTO> UpdateUser(long id, UpdateInputUserDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
