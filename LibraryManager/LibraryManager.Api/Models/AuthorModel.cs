using LibraryManager.Api.DTOs.Author;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models
{
    public class AuthorModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public List<GetAuthorBooksDTO> Books { get; set; } = [];
        public DateTime? DateOfBirth { get; set; }
    }
}
