using LibraryManager.Core.Interfaces;
using LibraryManager.Core.DTOs.User.InputModels;
using LibraryManager.Core.DTOs.User.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpPost("/user")]
        public async Task<ActionResult<CreateUserDTO>> Register(CreateUserDTO modelDTO)
        {
            try
            {
                return await _userRepository.RegisterUser(modelDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible register the user");
                throw;
            }
        }

        [HttpGet("/user")]
        public async Task<ActionResult<List<ViewUserDTO>>> GetAll()
        {
            try
            {
                return await _userRepository.GetAllUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the user");
                throw;
            }
        }

        [HttpGet("/user/{id}")]
        public async Task<ActionResult<ViewUserDTO>> GetById(long id)
        {
            try
            {
                return await _userRepository.GetUserById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the user");
                throw;
            }
        }

        [HttpGet("/validateCredentials/{id}")]
        public async Task<ActionResult<ViewValidateCredentialsUserDTO>> GetByIdValidateCredentials(long id)
        {
            try
            {
                return await _userRepository.GetUserByIdValidateCredentials(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the user");
                throw;
            }
        }

        [HttpPut("/user/{id}")]
        public async Task<ActionResult<UpdateInputUserDTO>> Update(long id, UpdateInputUserDTO DTO)
        {
            try
            {
                return await _userRepository.UpdateUser(id, DTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible update the user");
                throw;
            }
        }

        [HttpDelete("/user/{id}")]
        public async Task<ActionResult<bool>> Delete(long id)
        {
            try
            {
                return await _userRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible delete the user");
                throw;
            }
        }



    }
}
