using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Core.DTOs.Book.InputModel
{
    public class CreateBookDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public long AuthorId { get; set; }
        public DateTime PublishedTime { get; set; }
    }
}
