using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.DTOs.Book.Output
{
    public record ViewBooksInAuthorDTO
        (
        long Id,
        string Title,
        string? Description,
        double Price,
        string Category,
        long AuthorId,
        DateTime PublishedTime
        );
    
}
