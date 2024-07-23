using LibraryManager.Core.DTOs.User.ViewModels;
using LibraryManager.Core.Services.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;

        public AuthController(TokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost("/token")]
        public ActionResult<string> CreateToken(ViewValidateCredentialsUserDTO DTOcredentials)
        {
            return _tokenGenerator.GenerateToken(DTOcredentials);
        }
    }
}
