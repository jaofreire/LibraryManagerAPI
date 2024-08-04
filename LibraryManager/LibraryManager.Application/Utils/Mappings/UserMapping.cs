using LibraryManager.Domain.Models;
using LibraryManager.Application.DTOs.User.Input;
using LibraryManager.Application.DTOs.User.Output;
using LibraryManager.Domain.Enums;


namespace LibraryManager.Application.Utils.Mappings
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
                Role = ERoleType.User.ToString()
            };
        }

        public static UserModel ToModelWithPasswordHash(this CreateUserDTO DTO, string hash)
        {
            return new UserModel()
            {
                FirstName = DTO.FirstName,
                LastName = DTO.LastName,
                Email = DTO.Email,
                PasswordHash = hash,
                Role = ERoleType.User.ToString()
            };
        }

        public static ViewUserDTO ToViewDTO(this UserModel model)
        {
            return new ViewUserDTO(
                model.Id,
                model.FirstName,
                model.LastName,
                model.Email
                );
        }

        public static ViewValidateCredentialsUserDTO ToViewValidateCredentialsDTO(this UserModel model)
        {
            return new ViewValidateCredentialsUserDTO(
                model.Id,
                model.FirstName,
                model.LastName,
                model.Email,
                model.Role
                );
            
        }

        public static List<ViewUserDTO> ToViewDTOList(this List<UserModel> models)
        {
            return models.Select(x => x.ToViewDTO()).ToList();
        }
    }
}
