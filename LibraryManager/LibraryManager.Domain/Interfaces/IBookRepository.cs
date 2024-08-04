using LibraryManager.Domain.Models;
using System.Linq.Expressions;


namespace LibraryManager.Domain.Interfaces
{
    public interface IBookRepository
    {
        Task<BookModel> Register(BookModel model);
        Task<List<BookModel>> RegisterMany(List<BookModel> models);
        Task<List<BookModel>> GetAll();
        Task<BookModel> GetById(long id);
        Task<BookModel> GetByIdAsNoTracking(long id);
        Task<List<BookModel>> GetByTerm(Expression<Func<BookModel, bool>> predicate);
        Task<List<BookModel>> GetByTermAsNoTracking(Expression<Func<BookModel, bool>> predicate);
        Task<BookModel> Update(BookModel model);
        Task<bool> Delete(BookModel model);
    }
}
