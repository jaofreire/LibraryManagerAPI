using LibraryManager.Api.Models;
using LibraryManager.Api.Repositories;
using LibraryManager.Api.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("/author")]
        public async Task<ActionResult<List<AuthorModel>>> GetAll()
        {
            try
            {
                return await _authorRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }


        [HttpGet("/author/id/{id}")]
        public async Task<ActionResult<AuthorModel>> GetById(long id)
        {
            try
            {
                return await _authorRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }

        [HttpGet("/author/name/{name}")]
        public async Task<ActionResult<List<AuthorModel>>> GetByTitle(string name)
        {
            try
            {
                return await _authorRepository.GetByNameAsync(name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }

        [HttpPost("/author")]
        public async Task<ActionResult<AuthorModel>> Create(AuthorModel newModel)
        {
            try
            {
                return await _authorRepository.CreateAsync(newModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }

        [HttpPut("/author/{id}")]
        public async Task<ActionResult<AuthorModel>> Update(long id, AuthorModel model)
        {
            try
            {
                return await _authorRepository.UpdateAsync(id, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }

        }

        [HttpDelete("/author/{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                return await _authorRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }
    }
}
