using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.DTOs.Author.Output
{
    public record ViewAuthorInBooksDTO
        (
        long Id,
        string Name,
        string Bio, 
        DateTime? DateOfBirth 
        );
    
    
}
