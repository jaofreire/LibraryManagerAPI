using LibraryManager.Application.Responses;
using LibraryManager.Application.DTOs.Author.Input;
using LibraryManager.Application.DTOs.Author.Output;

namespace LibraryManager.Application.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<APIResponse<CreateAuthorDTO>> RegisterAuthor(CreateAuthorDTO model);
        Task<APIResponse<CreateAuthorDTO>> RegisterAuthors(List<CreateAuthorDTO> models);
        Task<APIResponse<ViewAuthorDTO>> GetAllAuthors();
        Task<APIResponse<ViewAuthorDTO>> GetAuthorById(long id);
        Task<APIResponse<UpdateAuthorDTO>> UpdateAuthor(UpdateAuthorDTO model);
        Task<APIResponse<ViewAuthorDTO>> DeleteAuthor(long id);
    }
}
