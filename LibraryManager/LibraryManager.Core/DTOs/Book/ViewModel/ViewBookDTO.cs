using LibraryManager.Core.DTOs.Author.ViewModel;
using LibraryManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Core.DTOs.Book.ViewModel
{
    public class ViewBookDTO
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? PhotoUrl { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public long AuthorId { get; set; }
        public ViewAuthorInBooksDTO? Author { get; set; }
        public DateTime PublishedTime { get; set; }
    }
}
