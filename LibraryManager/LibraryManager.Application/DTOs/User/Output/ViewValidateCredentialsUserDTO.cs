using LibraryManager.Application.Enums;

namespace LibraryManager.Application.DTOs.User.Output
{
    public record ViewValidateCredentialsUserDTO
        (
        long Id,
        string FirstName,
        string LastName,
        string Email,
        string Role
        );
    
}
