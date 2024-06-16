using LibraryManager.Core.DTOs.Author.InputModel;
using LibraryManager.Core.DTOs.Author.ViewModel;
using LibraryManager.Core.DTOs.User.InputModels;


namespace LibraryManager.Api.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Task<CreateAuthorDTO> RegisterAuthor(CreateAuthorDTO model);
        Task<List<ViewAuthorDTO>> GetAllAuthors();
        Task<ViewAuthorDTO> GetAuthorById(long id);
        Task<UpdateAuthorDTO> UpdateAuthor(long id, UpdateAuthorDTO model);
        Task<bool> DeleteAuthor(long id);
    }
}
