using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.DTOs.User.Input
{
    public record CreateUserDTO
        (
        string FirstName,
        string LastName,
        string Email,
        string Password
        );
    
}
