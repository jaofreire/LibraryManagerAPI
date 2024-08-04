using Microsoft.AspNetCore.Mvc;
using LibraryManager.Application.Services.Interfaces;
using LibraryManager.Application.DTOs.Author.Input;
using LibraryManager.Application.DTOs.Author.Output;
using LibraryManager.Application.Responses;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;

        }

        [HttpPost("/author")]
        public async Task<ActionResult<APIResponse<CreateAuthorDTO>>> Register(CreateAuthorDTO modelDTO)
            => await _authorService.RegisterAuthor(modelDTO);

        [HttpPost("/authors")]
        public async Task<ActionResult<APIResponse<CreateAuthorDTO>>> RegisterAuthors(List<CreateAuthorDTO> modelDTO)
            => await _authorService.RegisterAuthors(modelDTO);

        [HttpGet("/author")]
        public async Task<ActionResult<APIResponse<ViewAuthorDTO>>> GetAll()
            => await _authorService.GetAllAuthors();
        
        [HttpGet("/author/{id}")]
        public async Task<ActionResult<APIResponse<ViewAuthorDTO>>> GetById(long id)
            => await _authorService.GetAuthorById(id);

        [HttpPut("/author/{id}")]
        public async Task<ActionResult<APIResponse<UpdateAuthorDTO>>> Update(UpdateAuthorDTO DTO)
            => await _authorService.UpdateAuthor(DTO);

        [HttpDelete("/author/{id}")]
        public async Task<ActionResult<APIResponse<ViewAuthorDTO>>> Delete(long id)
            => await _authorService.DeleteAuthor(id);

    }
}
