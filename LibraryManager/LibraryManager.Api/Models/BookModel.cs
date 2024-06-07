using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models
{
    public class BookModel
    {
        public long Id { get; set; }

        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [Required(ErrorMessage = "The title field is required")]
        public string Title { get; set; } = string.Empty;

        [MinLength(10, ErrorMessage = "Minimum lenght is 10")]
        public string? Description { get; set; }

        public long? AuthorId { get; set; }
        public AuthorModel Author { get; set; } = null!;

        [MinLength(3, ErrorMessage = "Minimum lenght is 3")]
        [Required(ErrorMessage = "The category field is required")]
        public string Category { get; set; } = string.Empty;

        public DateTime? PublishedDate { get; set; }
    }
}
