using LibraryManager.Core.DTOs.Author.InputModel;
using LibraryManager.Core.DTOs.Author.ViewModel;
using Microsoft.AspNetCore.Mvc;
using LibraryManager.Core.Interfaces;
using LibraryManager.Core.Responses;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;

        }

        [HttpPost("/author")]
        public async Task<ActionResult<APIResponse<CreateAuthorDTO>>> Register(CreateAuthorDTO modelDTO)
            => await _authorRepository.RegisterAuthor(modelDTO);

        [HttpPost("/authors")]
        public async Task<ActionResult<APIResponse<CreateAuthorDTO>>> RegisterAuthors(List<CreateAuthorDTO> modelDTO)
            => await _authorRepository.RegisterAuthors(modelDTO);

        [HttpGet("/author")]
        public async Task<ActionResult<APIResponse<ViewAuthorDTO>>> GetAll()
            => await _authorRepository.GetAllAuthors();
        
        [HttpGet("/author/{id}")]
        public async Task<ActionResult<APIResponse<ViewAuthorDTO>>> GetById(long id)
            => await _authorRepository.GetAuthorById(id);

        [HttpPut("/author/{id}")]
        public async Task<ActionResult<APIResponse<UpdateAuthorDTO>>> Update(long id, UpdateAuthorDTO DTO)
            => await _authorRepository.UpdateAuthor(id, DTO);

        [HttpDelete("/author/{id}")]
        public async Task<ActionResult<APIResponse<ViewAuthorDTO>>> Delete(long id)
            => await _authorRepository.DeleteAuthor(id);

    }
}
