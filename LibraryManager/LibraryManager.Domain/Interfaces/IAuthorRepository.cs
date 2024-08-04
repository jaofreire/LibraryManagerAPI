using LibraryManager.Domain.Models;
using System.Linq.Expressions;

namespace LibraryManager.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Task<AuthorModel> Register(AuthorModel model);
        Task<List<AuthorModel>> RegisterMany(List<AuthorModel> models);
        Task<List<AuthorModel>> GetAll();
        Task<AuthorModel> GetById(long id);
        Task<AuthorModel> GetByIdAsNoTracking(long id);
        Task<List<AuthorModel>> GetByTerm(Expression<Func<AuthorModel, bool>> predicate);
        Task<List<AuthorModel>> GetByTermAsNoTracking(Expression<Func<AuthorModel, bool>> predicate);
        Task<AuthorModel> Update(AuthorModel model);
        Task<bool> Delete(AuthorModel model);
    }
}
