using LibraryManager.Core.DTOs.Book.InputModel;
using LibraryManager.Core.DTOs.Book.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Core.Interfaces
{
    public interface IBookRepository
    {
        Task<CreateBookDTO> RegisterBook(CreateBookDTO createBookDTO);
        Task<List<CreateBookDTO>> RegisterBooks(List<CreateBookDTO> createBookDTOList);
        Task<List<ViewBookDTO>> GetAllBooks();
        Task<ViewBookDTO> GetBookById(long id);
        Task<List<ViewBookDTO>> GetBookByName(string name);
        Task<List<ViewBookDTO>> GetBookByCategory(string category);
        Task<List<ViewBookDTO>> GetBooksByCategories(List<string> categorys);
        Task<List<ViewBookDTO>> GetBookByAuthor(string authorName);
        Task<List<ViewBookDTO>> GetBooksByAuthors(List<string> authorName);
        Task<UpdateBookDTO> UpdateBook(long id, UpdateBookDTO updateBookDTO);
        Task<bool> DeleteBook(long id);
    }
}
