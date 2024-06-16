using LibraryManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Core.DTOs.Author.ViewModel
{
    public class ViewAuthorDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<BookModel> Books { get; set; } = [];
    }
}
