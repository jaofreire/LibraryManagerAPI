using LibraryManager.Core.DTOs.Book.InputModel;
using LibraryManager.Core.DTOs.Book.ViewModel;

namespace LibraryManager.Api.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<CreateBookDTO> RegisterBook(CreateBookDTO createBookDTO);
        Task<List<ViewBookDTO>> GetAllBooks();
        Task<ViewBookDTO> GetBookById(long id);
        Task<List<ViewBookDTO>> GetBookByName(string name); 
        Task<List<ViewBookDTO>> GetBookByCategory(string category); 
        Task<UpdateBookDTO> UpdateBook(long id, UpdateBookDTO updateBookDTO);
        Task<bool> DeleteBook(long id);
    }
}
