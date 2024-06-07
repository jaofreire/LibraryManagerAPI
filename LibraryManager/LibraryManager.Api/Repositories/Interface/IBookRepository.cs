using LibraryManager.Api.Models;

namespace LibraryManager.Api.Repositories.Interface
{
    public interface IBookRepository
    {
        Task<List<BookModel>> GetAllAsync();
        Task<BookModel> GetByIdAsync(long id);
        Task<List<BookModel>> GetByNameAsync(string name);
        Task<List<BookModel>> GetByAuthorAsync(long authorId);
        Task<List<BookModel>> GetByCategoryAsync(string categoryName);
        Task<BookModel> CreateAsync(BookModel newModel);
        Task<BookModel> UpdateAsync(long id, BookModel model);
        Task<bool> DeleteAsync(long id);
    }
}
