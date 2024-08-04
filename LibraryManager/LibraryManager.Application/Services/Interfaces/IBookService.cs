using Microsoft.AspNetCore.Http;
using LibraryManager.Application.Responses;
using LibraryManager.Application.DTOs.Book.Input;
using LibraryManager.Application.DTOs.Book.Output;

namespace LibraryManager.Application.Services.Interfaces
{
    public interface IBookService
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
        Task<APIResponse<UpdateBookDTO>> UpdateBook(UpdateBookDTO updateBookDTO);
        Task<APIResponse<ViewBookDTO>> DeleteBook(long id);
    }
}
