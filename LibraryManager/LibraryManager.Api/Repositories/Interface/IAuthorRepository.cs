using LibraryManager.Api.DTOs.Author;
using LibraryManager.Api.Models;

namespace LibraryManager.Api.Repositories.Interface
{
    public interface IAuthorRepository
    {
        Task<List<AuthorModel>> GetAllAsync();
        Task<AuthorModel> GetByIdAsync(long id);
        Task<List<AuthorModel>> GetByNameAsync(string name);
        Task<AuthorModel> CreateAsync(CreateAuthorDTO newModel);
        Task<AuthorModel> UpdateAsync(long id, UpdateAuthorDTO model);
        Task<bool> DeleteAsync(long id);
    }
}
