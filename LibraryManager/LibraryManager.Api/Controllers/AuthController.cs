using LibraryManager.Application.Helper.Tokens.Interfaces;
using LibraryManager.Application.DTOs.User.Output;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokensGenerator _tokensGenerator;

        public AuthController(ITokensGenerator tokensGenerator)
        {
            _tokensGenerator = tokensGenerator;
        }

        [HttpPost("/token")]
        public ActionResult<string> CreateToken(ViewValidateCredentialsUserDTO DTOcredentials)
        {
            return _tokensGenerator.GenerateToken(DTOcredentials);
        }
    }
}
