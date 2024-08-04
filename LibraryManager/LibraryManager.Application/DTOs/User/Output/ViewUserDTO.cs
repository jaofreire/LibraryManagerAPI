using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.DTOs.User.Output
{
    public record ViewUserDTO
        (
        long Id,
        string FirstName,
        string LastName,
        string Email
        );
    
}
