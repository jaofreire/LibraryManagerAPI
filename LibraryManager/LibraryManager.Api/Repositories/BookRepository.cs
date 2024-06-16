using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Core.DTOs.Book.InputModel;
using LibraryManager.Core.DTOs.Book.ViewModel;

namespace LibraryManager.Api.Repositories
{
    public class BookRepository : IBookRepository
    {
        public Task<bool> DeleteBook(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewBookDTO>> GetAllBooks()
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewBookDTO>> GetBookByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Task<ViewBookDTO> GetBookById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewBookDTO>> GetBookByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<CreateBookDTO> RegisterBook(CreateBookDTO createBookDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateBookDTO> UpdateBook(long id, UpdateBookDTO updateBookDTO)
        {
            throw new NotImplementedException();
        }
    }
}
