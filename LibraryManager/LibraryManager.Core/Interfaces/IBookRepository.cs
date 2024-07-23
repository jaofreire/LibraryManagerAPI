using LibraryManager.Core.DTOs.Book.InputModel;
using LibraryManager.Core.DTOs.Book.ViewModel;
using LibraryManager.Core.Responses;
using Microsoft.AspNetCore.Http;

namespace LibraryManager.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<APIResponse<CreateBookDTO>> RegisterBook(CreateBookDTO createBookDTO);
        Task<APIResponse<CreateBookDTO>> RegisterBooks(List<CreateBookDTO> createBookDTOList);
        Task<APIResponse<ViewBookDTO>> GetAllBooks();
        Task<APIResponse<ViewBookDTO>> GetBookById(long id);
        Task<APIResponse<ViewBookDTO>> GetBookByName(string name);
        Task<APIResponse<ViewBookDTO>> GetBookByCategory(string category);
        Task<APIResponse<ViewBookDTO>> GetBooksByCategories(List<string> categorys);
        Task<APIResponse<ViewBookDTO>> GetBookByAuthor(string authorName);
        Task<APIResponse<ViewBookDTO>> GetBooksByAuthors(List<string> authorName);
        Task<APIResponse<UpdateBookDTO>> UpdateBook(IFormFile file , long id, UpdateBookDTO updateBookDTO);
        Task<APIResponse<ViewBookDTO>> DeleteBook(long id);
    }
}
