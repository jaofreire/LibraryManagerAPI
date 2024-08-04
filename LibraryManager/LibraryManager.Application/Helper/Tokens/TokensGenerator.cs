using LibraryManager.Application.DTOs.User.Output;
using LibraryManager.Application.Helper.Tokens.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Helper.Tokens
{
    public class TokensGenerator : ITokensGenerator
    {
        private readonly IConfiguration _configuration;

        public TokensGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(ViewValidateCredentialsUserDTO DTOcredentials)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]!);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var claims = CreateClaims(DTOcredentials.Id, DTOcredentials.Role.ToString());

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = claims,
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                SigningCredentials = credentials,
                Expires = DateTime.Now.AddDays(2)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity CreateClaims(long userId, string role)
        {
            var claimsIdentity = new ClaimsIdentity();

            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));

            return claimsIdentity;
        }
    }
}
