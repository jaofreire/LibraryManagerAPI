
using LibraryManager.Domain.Models;
using System.Linq.Expressions;

namespace LibraryManager.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserModel> RegisterUser(UserModel model);
        Task<List<UserModel>> GetAllUsers();
        Task<UserModel> GetUserById(long id);
        Task<UserModel> GetUserByIdAsNoTracking(long id);
        Task<List<UserModel>> GetUserByTerm(Expression<Func<UserModel, bool>> predicate);
        Task<UserModel> UpdateUser(UserModel model);
        Task<bool> DeleteUser(UserModel model);
    }

}
