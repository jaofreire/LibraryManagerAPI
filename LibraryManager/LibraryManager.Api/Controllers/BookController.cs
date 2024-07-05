using LibraryManager.Core.Interfaces;
using LibraryManager.Core.DTOs.Book.InputModel;
using LibraryManager.Core.DTOs.Book.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookRepository bookRepository, ILogger<BookController> logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        [HttpPost("/book")]
        public async Task<ActionResult<CreateBookDTO>> Register(CreateBookDTO DTO)
        {
            try
            {
                return await _bookRepository.RegisterBook(DTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible create the book");
                throw;
            }
        }

        [HttpPost("/books")]
        public async Task<ActionResult<List<CreateBookDTO>>> RegisterBooks([FromBody]List<CreateBookDTO> DTOList)
        {
            try
            {
                return await _bookRepository.RegisterBooks(DTOList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible create the book");
                throw;
            }
        }

        [HttpGet("/book")]
        public async Task<ActionResult<List<ViewBookDTO>>> GetAll()
        {
            try
            {
                return await _bookRepository.GetAllBooks();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the book");
                throw;
            }
        }

        [HttpGet("/book/{id}")]
        public async Task<ActionResult<ViewBookDTO>> GetById(long id)
        {
            try
            {
                return await _bookRepository.GetBookById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the book");
                throw;
            }
        }

        [HttpGet("/book/category/{category}")]
        public async Task<ActionResult<List<ViewBookDTO>>> GetByCategory(string category)
        {
            try
            {
                return await _bookRepository.GetBookByCategory(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the book");
                throw;
            }
        }

        [HttpGet("/book/categories/")]
        public async Task<ActionResult<List<ViewBookDTO>>> GetByCategories([FromQuery]List<string> categoriesList)
        {
            try
            {
                return await _bookRepository.GetBooksByCategories(categoriesList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the book");
                throw;
            }
        }

        [HttpGet("/book/author/{authorName}")]
        public async Task<ActionResult<List<ViewBookDTO>>> GetByAuthor(string authorName)
        {
            try
            {
                return await _bookRepository.GetBookByAuthor(authorName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the book");
                throw;
            }
        }

        [HttpGet("/book/authors")]
        public async Task<ActionResult<List<ViewBookDTO>>> GetByAuthors([FromQuery]List<string> authorNameList)
        {
            try
            {
                return await _bookRepository.GetBooksByAuthors(authorNameList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the book");
                throw;
            }
        }

        [HttpGet("/book/name/{name}")]
        public async Task<ActionResult<List<ViewBookDTO>>> GetByName(string name)
        {
            try
            {
                return await _bookRepository.GetBookByName(name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the book");
                throw;
            }
        }

        [HttpPut("/book/{id}")]
        public async Task<ActionResult<UpdateBookDTO>> Update(long id, UpdateBookDTO DTO)
        {
            try
            {
                return await _bookRepository.UpdateBook(id,DTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible update the book");
                throw;
            }

        }

        [HttpDelete("/book/{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                return await _bookRepository.DeleteBook(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible delete the book");
                throw;
            }

        }

    }
}
