using LibraryManager.Api.Models;
using LibraryManager.Api.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("/user")]
        public async Task<ActionResult<List<UserModel>>> GetAll()
        {
            try
            {
                return await _userRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
           
        }

        [HttpGet("/user/id/{id}")]
        public async Task<ActionResult<UserModel>> GetById(long id)
        {
            try
            {
                return await _userRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
           
        }


        [HttpGet("/user/name/{name}")]
        public async Task<ActionResult<List<UserModel>>> GetByName(string name)
        {
            try
            {
                return await _userRepository.GetByNameAsync(name);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
           
        }

        [HttpPost("/user")]
        public async Task<ActionResult<UserModel>> Create(UserModel newModel)
        {
            try
            {
                return await _userRepository.CreateAsync(newModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }

        [HttpPut("/user/{id}")]
        public async Task<ActionResult<UserModel>> Update(long id, UserModel model)
        {
            try
            {
                return await _userRepository.UpdateAsync(id, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }

        [HttpDelete("/user/{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                return await _userRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.InnerException);
            }
        }



        
    }
}
