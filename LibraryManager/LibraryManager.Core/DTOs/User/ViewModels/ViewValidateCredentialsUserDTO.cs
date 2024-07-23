using LibraryManager.Core.Enums;

namespace LibraryManager.Core.DTOs.User.ViewModels
{
    public class ViewValidateCredentialsUserDTO
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ERoleType Role { get; set; }
    }
}
