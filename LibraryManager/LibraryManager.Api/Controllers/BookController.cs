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


        [Authorize(Policy = "JustMerchantAndAdminPolicy")]
        [HttpPost("/book")]
        public async Task<ActionResult<APIResponse<CreateBookDTO>>> Register([FromForm]CreateBookDTO DTO)
            => await _bookRepository.RegisterBook(DTO);


        [Authorize(Policy = "JustMerchantAndAdminPolicy")]
        [HttpPost("/books")]
        public async Task<ActionResult<APIResponse<CreateBookDTO>>> RegisterBooks([FromBody]List<CreateBookDTO> DTOList)
            => await _bookRepository.RegisterBooks(DTOList);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetAll()
            => await _bookRepository.GetAllBooks();


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/{id}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetById(long id)
            => await _bookRepository.GetBookById(id);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/category/{category}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByCategory(string category)
            => await _bookRepository.GetBookByCategory(category);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/categories/")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByCategories([FromQuery]List<string> categoriesList)
            => await _bookRepository.GetBooksByCategories(categoriesList);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/author/{authorName}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByAuthor(string authorName)
            => await _bookRepository.GetBookByAuthor(authorName);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/authors")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByAuthors([FromQuery]List<string> authorNameList)
            => await _bookRepository.GetBooksByAuthors(authorNameList);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/book/name/{name}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> GetByName(string name)
            => await _bookRepository.GetBookByName(name);


        [Authorize(Policy = "JustMerchantAndAdminPolicy")]
        [HttpPut("/book/{id}")]
        public async Task<ActionResult<APIResponse<UpdateBookDTO>>> Update(IFormFile file, long id, UpdateBookDTO DTO)
            => await _bookRepository.UpdateBook(file, id, DTO);


        [Authorize(Policy = "JustMerchantAndAdminPolicy")]
        [HttpDelete("/book/{id}")]
        public async Task<ActionResult<APIResponse<ViewBookDTO>>> Delete(long id)
            => await _bookRepository.DeleteBook(id);


    }
}
