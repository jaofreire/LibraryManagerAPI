using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.DTOs.Book.Input
{
    public record UpdateBookDTO
        (
        long Id,
        string Title,
        IFormFile FormFile,
        string? Description,
        double Price,
        string Category,
        long AuthorId,
        DateTime PublishedDate
        );
    
}
