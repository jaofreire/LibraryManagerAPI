using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.DTOs.Author.Input
{
    public record UpdateAuthorDTO
        (
        long Id,
        string Name,
        string? Bio,
        DateTime? DateOfBirth
        );
     

}
