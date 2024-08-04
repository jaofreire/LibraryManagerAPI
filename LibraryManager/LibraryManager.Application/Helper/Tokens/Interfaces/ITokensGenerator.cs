using LibraryManager.Application.DTOs.User.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Helper.Tokens.Interfaces
{
    public interface ITokensGenerator
    {
        string GenerateToken(ViewValidateCredentialsUserDTO DTOcredentials);
    }
}
