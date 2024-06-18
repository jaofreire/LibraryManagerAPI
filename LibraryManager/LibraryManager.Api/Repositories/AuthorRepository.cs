using LibraryManager.Api.Data;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Core.DTOs.Author.InputModel;
using LibraryManager.Core.DTOs.Author.ViewModel;
using LibraryManager.Core.DTOs.Book.ViewModel;
using LibraryManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Api.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _dbContext;

        public AuthorRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateAuthorDTO> RegisterAuthor(CreateAuthorDTO modelDTO)
        {
            var model = new AuthorModel()
            {
               Name = modelDTO.Name,
               Bio = modelDTO.Bio,
               DateOfBirth = modelDTO.DateOfBirth,
            };

            await _dbContext.Authors.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return modelDTO;
        }

        public async Task<List<ViewAuthorDTO>> GetAllAuthors()
        {
            List<ViewAuthorDTO> authorsDTO = [];

            var models = await _dbContext.Authors
                .AsNoTracking()
                .Include(x => x.Books)
                .ToListAsync();

            foreach (var authors in models)
            {
                List<ViewBooksInAuthorDTO> booksListDTO = [];
                foreach (var books in authors.Books)
                {
                    var bookDTO = new ViewBooksInAuthorDTO()
                    {
                        Id = books.Id,
                        Title = books.Title,
                        Description = books.Description,
                        Price = books.Price,
                        Category = books.Category,
                        AuthorId = books.AuthorId,
                        PublishedTime = books.PublishedTime,
                    };

                    booksListDTO.Add(bookDTO);
                }

                var DTO = new ViewAuthorDTO()
                {
                    Id = authors.Id,
                    Name = authors.Name,
                    Bio = authors.Bio,
                    DateOfBirth = authors.DateOfBirth,
                    Books = booksListDTO
                };


                authorsDTO.Add(DTO);
            }

            return authorsDTO;
        }

        public async Task<ViewAuthorDTO> GetAuthorById(long id)
        {
            var model = await _dbContext.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) ?? 
                throw new Exception("The author is not found");

            List<ViewBooksInAuthorDTO> booksListDTO = [];
            foreach (var books in model.Books)
            {
                var bookDTO = new ViewBooksInAuthorDTO()
                {
                    Id = books.Id,
                    Title = books.Title,
                    Description = books.Description,
                    Price = books.Price,
                    Category = books.Category,
                    AuthorId = books.AuthorId,
                    PublishedTime = books.PublishedTime,
                };

                booksListDTO.Add(bookDTO);
            }

            var DTO = new ViewAuthorDTO()
            {
                Id = model.Id,
                Name = model.Name,
                Bio = model.Bio,
                DateOfBirth = model.DateOfBirth,
                Books = booksListDTO
            };

            return DTO;
        }

        public async Task<UpdateAuthorDTO> UpdateAuthor(long id, UpdateAuthorDTO modelDTO)
        {
            var modelUpdate = await _dbContext.Authors
                .FindAsync(id) ??
                throw new Exception("The author is not found");

            modelUpdate.Name = modelDTO.Name;
            modelUpdate.Bio = modelDTO.Bio;
            modelUpdate.DateOfBirth = modelDTO.DateOfBirth;

            _dbContext.Authors.Update(modelUpdate);
            await _dbContext.SaveChangesAsync();

            return modelDTO;
        }

        public async Task<bool> DeleteAuthor(long id)
        {
            var model = await _dbContext.Authors
                .FindAsync(id) ??
                throw new Exception("The author is not found");

            _dbContext.Authors.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
