using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LibraryManager.Application.Services.Interfaces;
using LibraryManager.Application.DTOs.User.Input;
using LibraryManager.Application.DTOs.User.Output;
using LibraryManager.Application.Responses;


namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/user")]
        public async Task<ActionResult<APIResponse<CreateUserDTO>>> Register(CreateUserDTO modelDTO)
            => await _userService.RegisterUser(modelDTO);


        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("/user")]
        public async Task<ActionResult<APIResponse<ViewUserDTO>>> GetAll()
            => await _userService.GetAllUsers();


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpGet("/user/{id}")]
        public async Task<ActionResult<APIResponse<ViewUserDTO>>> GetById(long id)
            => await _userService.GetUserById(id);


        [HttpPost("/validateCredentials")]
        public async Task<ActionResult<APIResponse<ViewValidateCredentialsUserDTO>>> ValidateCredentials(ValidateCredentialsUserDTO DTOresquest)
            => await _userService.ValidateUserCredentials(DTOresquest);


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpPut("/user/{id}")]
        public async Task<ActionResult<APIResponse<UpdateInputUserDTO>>> Update(long id, UpdateInputUserDTO DTO)
        {
            return await _userService.UpdateUser(id, DTO);
        }


        [Authorize(Policy = "EveryoneHasAccessPolicy")]
        [HttpDelete("/user/{id}")]
        public async Task<ActionResult<APIResponse<ViewUserDTO>>> Delete(long id)
            => await _userService.DeleteUser(id);

    }
}
