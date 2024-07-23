using Data.Context;
using LibraryManager.Core.DTOs.Author.InputModel;
using LibraryManager.Core.DTOs.Author.ViewModel;
using LibraryManager.Core.DTOs.Book.ViewModel;
using LibraryManager.Core.Enums;
using LibraryManager.Core.Interfaces;
using LibraryManager.Core.Models;
using LibraryManager.Core.Responses;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryDbContext _dbContext;

        public AuthorRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<APIResponse<CreateAuthorDTO>> RegisterAuthor(CreateAuthorDTO modelDTO)
        {
            var model = new AuthorModel()
            {
                Name = modelDTO.Name,
                Bio = modelDTO.Bio,
                DateOfBirth = modelDTO.DateOfBirth,
            };

            await _dbContext.Authors.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return new APIResponse<CreateAuthorDTO>(
                operationType: EOperationType.Create.ToString(),
                true,
                200,
                message: "Author registered successfully!",
                dataResponse: modelDTO,
                dataResponseList: null
                );
        }

        public async Task<APIResponse<CreateAuthorDTO>> RegisterAuthors(List<CreateAuthorDTO> modelsDTO)
        {
            List<AuthorModel> modelList = [];
            foreach(var authors in modelsDTO)
            {
                var model = new AuthorModel()
                {
                    Name = authors.Name,
                    Bio = authors.Bio,
                    DateOfBirth = authors.DateOfBirth,
                };
                modelList.Add(model);
            }

            await _dbContext.Authors.AddRangeAsync(modelList);
            await _dbContext.SaveChangesAsync();

            return new APIResponse<CreateAuthorDTO>(
                operationType: EOperationType.CreateMany.ToString(),
                true,
                200,
                message: "Authors registered successfully!",
                dataResponse: null,
                dataResponseList: modelsDTO!
                );
        }

        public async Task<APIResponse<ViewAuthorDTO>> GetAllAuthors()
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

            return new APIResponse<ViewAuthorDTO>(
                 operationType: EOperationType.Get.ToString(),
                 true,
                 200,
                 message: "Listing all authors registered successfully!",
                 dataResponse: null,
                 dataResponseList: authorsDTO!
                 );
        }

        public async Task<APIResponse<ViewAuthorDTO>> GetAuthorById(long id)
        {
            var model = await _dbContext.Authors
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if(model is null) return new APIResponse<ViewAuthorDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 false,
                 404,
                 message: "The author is not found"
                 );

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

            return new APIResponse<ViewAuthorDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 true,
                 200,
                 message: "Listing authors with specify id successfully!",
                 dataResponse: DTO,
                 dataResponseList: null
                 );
        }

        public async Task<APIResponse<UpdateAuthorDTO>> UpdateAuthor(long id, UpdateAuthorDTO modelDTO)
        {
            var modelUpdate = await _dbContext.Authors
                .FindAsync(id);

            if(modelUpdate is null) return new APIResponse<UpdateAuthorDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 false,
                 404,
                 message: "The author is not found"
                 );

            modelUpdate.Name = modelDTO.Name;
            modelUpdate.Bio = modelDTO.Bio;
            modelUpdate.DateOfBirth = modelDTO.DateOfBirth;

            _dbContext.Authors.Update(modelUpdate);
            await _dbContext.SaveChangesAsync();

            return new APIResponse<UpdateAuthorDTO>(
                 operationType: EOperationType.Update.ToString(),
                 true,
                 200,
                 message: "Author updated successfully!",
                 dataResponse: modelDTO,
                 dataResponseList: null
                 );

        }

        public async Task<APIResponse<ViewAuthorDTO>> DeleteAuthor(long id)
        {
            var model = await _dbContext.Authors
                .FindAsync(id);

            if(model is null) return new APIResponse<ViewAuthorDTO>(
                 operationType: EOperationType.GetById.ToString(),
                 false,
                 404,
                 message: "The author is not found"
                 );

            _dbContext.Authors.Remove(model);
            await _dbContext.SaveChangesAsync();

            return new APIResponse<ViewAuthorDTO>(
                 operationType: EOperationType.Delete.ToString(),
                 true,
                 200,
                 message: "Author updated successfully!"
                 );

        }

    }
}
