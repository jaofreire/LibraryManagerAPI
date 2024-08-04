using LibraryManager.Application.DTOs.Author.Input;
using LibraryManager.Application.DTOs.Author.Output;
using LibraryManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Utils.Mappings
{
    public static class AuthorMapping
    {

        public static AuthorModel ToModel(this CreateAuthorDTO DTO)
        {
            return new AuthorModel()
            {
                Name = DTO.Name,
                Bio = DTO.Bio,
                DateOfBirth = DTO.DateOfBirth,
            };
        }

        public static AuthorModel ToModel(this UpdateAuthorDTO DTO)
        {
            return new AuthorModel()
            {
                Name = DTO.Name,
                Bio = DTO.Bio,
                DateOfBirth = DTO.DateOfBirth,
            };
        }

        public static ViewAuthorDTO ToViewDTO(this AuthorModel model)
        {
            return new ViewAuthorDTO
                (
                model.Id,
                model.Name,
                model.Bio,
                model.DateOfBirth,
                model.Books.ToViewBooksInAuthorDTOList()
                );
        }

        public static ViewAuthorInBooksDTO ToViewAuthorInBooks(this AuthorModel model)
        {
            return new ViewAuthorInBooksDTO
                (
                model.Id,
                model.Name,
                model.Bio,
                model.DateOfBirth
                );
        }

        public static List<ViewAuthorDTO> ToViewDTOList(this List<AuthorModel> models)
        {
            return models.Select(x => x.ToViewDTO()).ToList();
        }

        public static List<AuthorModel> ToModelList(this List<CreateAuthorDTO> DTOs)
        {
            return DTOs.Select(x => x.ToModel()).ToList();
        }


    }
}
