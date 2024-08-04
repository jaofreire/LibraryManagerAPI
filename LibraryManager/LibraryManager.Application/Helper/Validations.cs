using LibraryManager.Application.DTOs.Author.Input;
using LibraryManager.Application.DTOs.Book.Input;
using LibraryManager.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Helper
{
    public static class Validations
    {
        public static UpdateBookDTO UpdateValidation(UpdateBookDTO DTO, BookModel model, out bool isNewFileForm)
        {
            long Id = DTO.Id;
            string Title = DTO.Title;
            IFormFile FormFile = DTO.FormFile;
            string? Description = DTO.Description;
            double Price = DTO.Price;
            string Category = DTO.Category;
            long AuthorId = DTO.AuthorId;
            DateTime PublishedDate = DTO.PublishedDate;

            isNewFileForm = false;

            if (DTO.Title == model.Title)
                Title = model.Title;

            if (DTO.FormFile != null)
                isNewFileForm = true;

            if(DTO.Description == model.Description)
                Description = model.Description;

            if(DTO.Price == model.Price)
                Price = model.Price;
                
            if(DTO.Category == model.Category)
                Category = model.Category;

            if (DTO.PublishedDate == model.PublishedTime)
                PublishedDate = model.PublishedTime;


            return new UpdateBookDTO
                (
                Id,
                Title,
                FormFile,
                Description,
                Price,
                Category,
                AuthorId,
                PublishedDate
                );
        }

        public static UpdateAuthorDTO UpdateValidation(UpdateAuthorDTO DTO, AuthorModel model)
        {
            long Id = model.Id;
            string Name = DTO.Name;
            string? Bio = DTO.Bio;
            DateTime? DateOfBirth = DTO.DateOfBirth;

            if(DTO.Name == model.Name)
                Name = model.Name;

            if(DTO.Bio == model.Bio)
                Bio = model.Bio;

            if(DTO.DateOfBirth == model.DateOfBirth)
                DateOfBirth = model.DateOfBirth;

            return new UpdateAuthorDTO
                (
                Id,
                Name,
                Bio,
                DateOfBirth
                );

        }

    }
}
