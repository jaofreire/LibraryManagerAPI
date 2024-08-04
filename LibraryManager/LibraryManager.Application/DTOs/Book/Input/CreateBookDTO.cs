using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.DTOs.Book.Input
{
    public record CreateBookDTO
        (
        string Title,
        IFormFile? FileForm,
        string? Description,
        double Price,
        string Category,
        long AuthorId,
        DateTime PublishedDate
        );

}
