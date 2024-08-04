using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManager.Application.Services.Interfaces;
using LibraryManager.Application.Responses;
using LibraryManager.Application.DTOs.Book.Input;
using LibraryManager.Application.DTOs.Book.Output;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }


        [Authorize(Policy = "JustMerchantAndAdminPolicy")]
        [HttpPost("/book")]
        public async Task<ActionResult<APIResponse<CreateBookDTO>>> Register([FromForm]CreateBookDTO DTO)
            => await _bookService.RegisterBook(DTO);


        [Authorize(Policy = "JustMerchantAndAdminPolicy")]
        [HttpPost("/books")]
        public async Task<ActionResult<APIResponse<CreateBookDTO>>> RegisterBooks([FromBody]List<CreateBookDTO> DTOList)
            => await _bookService.RegisterBooks(DTOList);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetAll()
            => await _bookService.GetAllBooks();


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/{id}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetById(long id)
            => await _bookService.GetBookById(id);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/category/{category}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByCategory(string category)
            => await _bookService.GetBookByCategory(category);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/categories/")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByCategories([FromQuery]List<string> categoriesList)
            => await _bookService.GetBooksByCategories(categoriesList);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/author/{authorName}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByAuthor(string authorName)
            => await _bookService.GetBookByAuthor(authorName);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/authors")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByAuthors([FromQuery]List<string> authorNameList)
            => await _bookService.GetBooksByAuthors(authorNameList);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/name/{name}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByName(string name)
            => await _bookService.GetBookByName(name);


        [Authorize(Policy = "JustMerchantAndAdminPolicy")]
        [HttpPut("/book/{id}")]
        public async Task<ActionResult<APIResponse<UpdateBookDTO>>> Update([FromForm]UpdateBookDTO DTO)
            => await _bookService.UpdateBook(DTO);


        [Authorize(Policy = "JustMerchantAndAdminPolicy")]
        [HttpDelete("/book/{id}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> Delete(long id)
            => await _bookService.DeleteBook(id);


    }
}
