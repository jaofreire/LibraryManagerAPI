using LibraryManager.Api.DTOs.User;
using LibraryManager.Api.Models;

namespace LibraryManager.Api.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllAsync();
        Task<UserModel> GetByIdAsync(long id);
        Task<List<UserModel>> GetByNameAsync(string name);
        Task<UserModel> CreateAsync(CreateUserDTO newModel);
        Task<UserModel> UpdateAsync(long id, UserModel model);
        Task<bool> DeleteAsync(long id);
    }
}
