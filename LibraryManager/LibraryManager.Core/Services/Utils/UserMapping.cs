using LibraryManager.Core.DTOs.User.InputModels;
using LibraryManager.Core.DTOs.User.ViewModels;
using LibraryManager.Core.Enums;
using LibraryManager.Core.Models;

namespace LibraryManager.Core.Services.Utils
{
    public static class UserMapping
    {

        public static UserModel ToModel(this CreateUserDTO DTO)
        {
            return new UserModel()
            {
                FirstName = DTO.FirstName,
                LastName = DTO.LastName,
                Email = DTO.Email,
                PasswordHash = DTO.Password,
                Role = ERoleType.User
            };
        }

        public static ViewUserDTO ToViewDTO(this UserModel model)
        {
            return new ViewUserDTO()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email
            };
        }

        public static ViewValidateCredentialsUserDTO ToViewValidateCredentialsDTO(this UserModel model)
        {
            return new ViewValidateCredentialsUserDTO()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = model.PasswordHash,
                Role = model.Role
            };
        }

    }
}
