using LibraryManager.Core.Interfaces;
using LibraryManager.Core.DTOs.Book.InputModel;
using LibraryManager.Core.DTOs.Book.ViewModel;
using LibraryManager.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Amazon.Auth.AccessControlPolicy;

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


        [Authorize(Policy = "MerchantPolicy")]
        [HttpPost("/book")]
        public async Task<ActionResult<APIResponse<CreateBookDTO>>> Register([FromForm]CreateBookDTO DTO)
            => await _bookRepository.RegisterBook(DTO);

        [HttpPost("/books")]
        public async Task<ActionResult<APIResponse<CreateBookDTO>>> RegisterBooks([FromBody]List<CreateBookDTO> DTOList)
            => await _bookRepository.RegisterBooks(DTOList);

        [HttpGet("/book")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetAll()
            => await _bookRepository.GetAllBooks();

        [HttpGet("/book/{id}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetById(long id)
            => await _bookRepository.GetBookById(id);

        [HttpGet("/book/category/{category}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByCategory(string category)
            => await _bookRepository.GetBookByCategory(category);

        [HttpGet("/book/categories/")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByCategories([FromQuery]List<string> categoriesList)
            => await _bookRepository.GetBooksByCategories(categoriesList);

        [HttpGet("/book/author/{authorName}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByAuthor(string authorName)
            => await _bookRepository.GetBookByAuthor(authorName);

        [HttpGet("/book/authors")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByAuthors([FromQuery]List<string> authorNameList)
            => await _bookRepository.GetBooksByAuthors(authorNameList);

        [HttpGet("/book/name/{name}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByName(string name)
            => await _bookRepository.GetBookByName(name);

        [HttpPut("/book/{id}")]
        public async Task<ActionResult<APIResponse<UpdateBookDTO>>> Update(IFormFile file, long id, UpdateBookDTO DTO)
            => await _bookRepository.UpdateBook(file, id, DTO);

        [HttpDelete("/book/{id}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> Delete(long id)
            => await _bookRepository.DeleteBook(id);

    }
}
