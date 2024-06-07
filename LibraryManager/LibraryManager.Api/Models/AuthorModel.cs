using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models
{
    public class AuthorModel
    {
        public long Id { get; set; }

        [MinLength(5)]
        [Required(ErrorMessage = "The name field is required")]
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
