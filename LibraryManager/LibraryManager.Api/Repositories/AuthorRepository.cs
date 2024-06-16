using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Core.DTOs.Author.InputModel;
using LibraryManager.Core.DTOs.Author.ViewModel;

namespace LibraryManager.Api.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        public Task<bool> DeleteAuthor(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewAuthorDTO>> GetAllAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<ViewAuthorDTO> GetAuthorById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<CreateAuthorDTO> RegisterAuthor(CreateAuthorDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateAuthorDTO> UpdateAuthor(long id, UpdateAuthorDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
