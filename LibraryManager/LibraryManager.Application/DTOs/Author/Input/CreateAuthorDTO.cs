using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.DTOs.Author.Input
{
    public record CreateAuthorDTO
        (
        string Name,
        string? Bio,
        DateTime? DateOfBirth
        );

}
