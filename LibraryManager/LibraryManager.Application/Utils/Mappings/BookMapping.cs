using LibraryManager.Application.DTOs.Book.Input;
using LibraryManager.Application.DTOs.Book.Output;
using LibraryManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Utils.Mappings
{
    public static class BookMapping
    {
        public static BookModel ToModel(this CreateBookDTO DTO)
        {
            return new BookModel()
            {
                Title = DTO.Title,
                Description = DTO.Description,
                Category = DTO.Category,
                Price = DTO.Price,
                AuthorId = DTO.AuthorId,
                PublishedTime = DTO.PublishedDate
            };
        }

        public static BookModel ToModel(this UpdateBookDTO DTO)
        {
            return new BookModel()
            {
                Title = DTO.Title,
                Description = DTO.Description,
                Category = DTO.Category,
                Price = DTO.Price,
                AuthorId = DTO.AuthorId,
                PublishedTime = DTO.PublishedDate
            };
        }

        public static BookModel ToModel(this ViewBooksInOrderDTO DTO)
        {
            return new BookModel()
            {
                Title = DTO.Title,
                Description = DTO.Description,
                Category = DTO.Category,
                Price = DTO.Price,
                AuthorId = DTO.AuthorId,
                PublishedTime = DTO.PublishedTime
            };
        }

        public static BookModel ToModelWithPhotoUrl(this UpdateBookDTO DTO, string photoUrl)
        {
            return new BookModel()
            {
                Title = DTO.Title,
                PhotoUrl = photoUrl,
                Description = DTO.Description,
                Category = DTO.Category,
                Price = DTO.Price,
                AuthorId = DTO.AuthorId,
                PublishedTime = DTO.PublishedDate
            };
        }

        public static ViewBookDTO ToViewDTO(this BookModel model)
        {
            return new ViewBookDTO(
                model.Id,
                model.Title,
                model.PhotoUrl,
                model.Description,
                model.Price,
                model.Category,
                model.AuthorId,
                model.Author.ToViewAuthorInBooks(),
                model.PublishedTime
                );
            
        }

        public static ViewBooksInAuthorDTO ToViewBooksInAuthorDTO(this BookModel model)
        {
            return new ViewBooksInAuthorDTO
                (
                model.Id,
                model.Title,
                model.Description,
                model.Price,
                model.Category,
                model.AuthorId,
                model.PublishedTime
                );
        }

        public static List<BookModel> ToModelList(this List<CreateBookDTO> DTOs)
        {
            return DTOs.Select(x => x.ToModel()).ToList();
        }

        public static List<BookModel> ToModelList(this List<ViewBooksInOrderDTO> DTOs)
        {
            return DTOs.Select(x => x.ToModel()).ToList();
        }

        public static List<ViewBookDTO> ToViewDTOList(this List<BookModel> DTOs)
        {
            return DTOs.Select(x => x.ToViewDTO()).ToList();
        }

        public static List<ViewBooksInAuthorDTO> ToViewBooksInAuthorDTOList(this List<BookModel> models)
        {
            return models.Select(x => x.ToViewBooksInAuthorDTO()).ToList();
        }

    }
}
