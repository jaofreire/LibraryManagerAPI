using LibraryManager.Core.DTOs.Author.InputModel;
using LibraryManager.Core.DTOs.Author.ViewModel;
using LibraryManager.Core.Responses;


namespace LibraryManager.Core.Interfaces
{
    public interface IAuthorRepository
    {
        Task<APIResponse<CreateAuthorDTO>> RegisterAuthor(CreateAuthorDTO model);
        Task<APIResponse<CreateAuthorDTO>> RegisterAuthors(List<CreateAuthorDTO> models);
        Task<APIResponse<ViewAuthorDTO>> GetAllAuthors();
        Task<APIResponse<ViewAuthorDTO>> GetAuthorById(long id);
        Task<APIResponse<UpdateAuthorDTO>> UpdateAuthor(long id, UpdateAuthorDTO model);
        Task<APIResponse<ViewAuthorDTO>> DeleteAuthor(long id);
    }
}
