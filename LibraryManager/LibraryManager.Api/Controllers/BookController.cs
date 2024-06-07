using LibraryManager.Api.Models;
using LibraryManager.Api.Repositories;
using LibraryManager.Api.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("/book")]
        public async Task<ActionResult<List<BookModel>>> GetAll()
        {
            try
            {
                return await _bookRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }


        [HttpGet("/book/id/{id}")]
        public async Task<ActionResult<BookModel>> GetById(long id)
        {
            try
            {
                return await _bookRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }

        [HttpGet("/book/title/{title}")]
        public async Task<ActionResult<List<BookModel>>> GetByTitle(string title)
        {
            try
            {
                return await _bookRepository.GetByNameAsync(title);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }

        [HttpGet("/book/author/{authorId}")]
        public async Task<ActionResult<List<BookModel>>> GetByAuthor(long authorId)
        {
            try
            {
                return await _bookRepository.GetByAuthorAsync(authorId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }

        [HttpGet("/book/category/{categoryName}")]
        public async Task<ActionResult<List<BookModel>>> GetByCategory(string categoryName)
        {
            try
            {
                return await _bookRepository.GetByCategoryAsync(categoryName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }

        [HttpPost("/book")]
        public async Task<ActionResult<BookModel>> Create(BookModel newModel)
        {
            try
            {
                return await _bookRepository.CreateAsync(newModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }

        [HttpPut("/book/{id}")]
        public async Task<ActionResult<BookModel>> Update(long id, BookModel model)
        {
            try
            {
                return await _bookRepository.UpdateAsync(id, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }

        [HttpDelete("/book/{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                return await _bookRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }

    }
}
