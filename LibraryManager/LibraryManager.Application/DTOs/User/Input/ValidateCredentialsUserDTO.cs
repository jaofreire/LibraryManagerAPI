using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.DTOs.User.Input
{
    public record ValidateCredentialsUserDTO
        (
        string Name,
        string Email,
        string Password
        );
    
}
