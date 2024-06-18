using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Core.DTOs.Author.ViewModel
{
    public class ViewAuthorInBooksDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
    }
}
