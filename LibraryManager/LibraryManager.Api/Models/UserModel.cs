using LibraryManager.Api.Enum;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models
{
    public class UserModel
    {
        public long Id { get; set; }

        [MinLength(5, ErrorMessage = "Minimum lenght is 5")]
        [Required(ErrorMessage = "The name field is required")]
        public string Name { get; set; } = string.Empty;

        [MinLength(8, ErrorMessage = "Minimum lenght is 8")]
        [Required(ErrorMessage = "The password field is required")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "The role field is required")]
        public EUserRole Role { get; set; }
    }
}
