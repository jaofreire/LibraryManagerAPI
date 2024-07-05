using LibraryManager.Core.DTOs.Author.InputModel;
using LibraryManager.Core.DTOs.Author.ViewModel;
using Microsoft.AspNetCore.Mvc;
using LibraryManager.Core.Interfaces;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ILogger<AuthorController> _logger;

        public AuthorController(IAuthorRepository authorRepository, ILogger<AuthorController> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }

        [HttpPost("/author")]
        public async Task<ActionResult<CreateAuthorDTO>> Register(CreateAuthorDTO modelDTO)
        {
            try
            {
                return await _authorRepository.RegisterAuthor(modelDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible register the author");
                throw;
            }
        }

        public async Task<ActionResult<List<CreateAuthorDTO>>> RegisterAuthors(List<CreateAuthorDTO> modelDTO)
        {
            try
            {
                return await _authorRepository.RegisterAuthors(modelDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible register the author");
                throw;
            }
        }

        [HttpGet("/author")]
        public async Task<ActionResult<List<ViewAuthorDTO>>> GetAll()
        {
            try
            {
                return await _authorRepository.GetAllAuthors();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the author");
                throw;
            }
        }

        [HttpGet("/author/{id}")]
        public async Task<ActionResult<ViewAuthorDTO>> GetById(long id)
        {
            try
            {
                return await _authorRepository.GetAuthorById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the author");
                throw;
            }
        }

        [HttpPut("/author/{id}")]
        public async Task<ActionResult<UpdateAuthorDTO>> Update(long id, UpdateAuthorDTO DTO)
        {
            try
            {
                return await _authorRepository.UpdateAuthor(id, DTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible update the author");
                throw;
            }
        }

        [HttpDelete("/author/{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                return await _authorRepository.DeleteAuthor(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible delete the author");
                throw;
            }
        }
    }
}
