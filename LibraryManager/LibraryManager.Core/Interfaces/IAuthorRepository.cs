using LibraryManager.Core.DTOs.Author.InputModel;
using LibraryManager.Core.DTOs.Author.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Core.Interfaces
{
    public interface IAuthorRepository
    {
        Task<CreateAuthorDTO> RegisterAuthor(CreateAuthorDTO model);
        Task<List<CreateAuthorDTO>> RegisterAuthors(List<CreateAuthorDTO> models);
        Task<List<ViewAuthorDTO>> GetAllAuthors();
        Task<ViewAuthorDTO> GetAuthorById(long id);
        Task<UpdateAuthorDTO> UpdateAuthor(long id, UpdateAuthorDTO model);
        Task<bool> DeleteAuthor(long id);
    }
}
