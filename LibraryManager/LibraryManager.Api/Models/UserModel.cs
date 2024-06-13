using LibraryManager.Api.Enum;
using System.ComponentModel.DataAnnotations;

namespace LibraryManager.Api.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public EUserRole Role { get; set; }
    }
}
