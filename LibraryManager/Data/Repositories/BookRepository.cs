using Data.Context;
using Data.Services.APIs;
using Data.Services.Utils;
using LibraryManager.Core.DTOs.Author.ViewModel;
using LibraryManager.Core.DTOs.Book.InputModel;
using LibraryManager.Core.DTOs.Book.ViewModel;
using LibraryManager.Core.Interfaces;
using LibraryManager.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;
        private readonly CacheHandler _cacheHandler;
        private readonly AWSS3 _s3Service;

        public BookRepository(LibraryDbContext dbContext,
            CacheHandler cacheHandler,
            AWSS3 s3Service)
        {
            _dbContext = dbContext;
            _cacheHandler = cacheHandler;
            _s3Service = s3Service;
        }

        public async Task<CreateBookDTO> RegisterBook(CreateBookDTO createBookDTO)
        {
            string photoUrl = await _s3Service.PutNewS3ImageObject(createBookDTO.FileForm, createBookDTO.Title);

            var model = new BookModel()
            {
                Title = createBookDTO.Title,
                PhotoUrl = photoUrl,
                Description = createBookDTO.Description,
                Price = createBookDTO.Price,
                Category = createBookDTO.Category,
                AuthorId = createBookDTO.AuthorId,
                PublishedTime = createBookDTO.PublishedTime,
            };

            await _dbContext.Books.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            await _cacheHandler.RemoveCache(DataConfigurations.SearchBooksCacheFactor);

            return createBookDTO;
        }

        public async Task<List<CreateBookDTO>> RegisterBooks(List<CreateBookDTO> createBookDTOList)
        {
            List<BookModel> modelList = [];

            foreach (var books in createBookDTOList)
            {
                var model = new BookModel()
                {
                    Title = books.Title,
                    Description = books.Description,
                    Price = books.Price,
                    Category = books.Category,
                    AuthorId = books.AuthorId,
                    PublishedTime = books.PublishedTime,
                };

                modelList.Add(model);
            }

            await _dbContext.AddRangeAsync(modelList);
            await _dbContext.SaveChangesAsync();

            await _cacheHandler.RemoveCache(DataConfigurations.SearchBooksCacheFactor);

            return createBookDTOList;
        }

        public async Task<List<ViewBookDTO>> GetAllBooks()
        {

            var viewBooksCache = await _cacheHandler.GetCacheObject<List<ViewBookDTO>>(DataConfigurations.SearchBooksCacheFactor);

            if (viewBooksCache != default) return viewBooksCache;


            var models = await _dbContext.Books
                .AsNoTracking()
                .Include(x => x.Author)
                .ToListAsync();

            List<ViewBookDTO> booksDTO = [];

            foreach (var booksModel in models)
            {
                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    PhotoUrl = booksModel.PhotoUrl,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };

                booksDTO.Add(DTO);
            }

            await _cacheHandler.SetCacheObject<List<ViewBookDTO>>(DataConfigurations.SearchBooksCacheFactor, booksDTO);

            return booksDTO;
        }

        public async Task<ViewBookDTO> GetBookById(long id)
        {
            var model = await _dbContext.Books
                .AsNoTracking()
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id) ??
                throw new Exception("The book is not found");

            var AuthorDTO = new ViewAuthorInBooksDTO()
            {
                Id = model.AuthorId,
                Name = model.Author.Name,
                Bio = model.Author.Bio,
                DateOfBirth = model.Author.DateOfBirth
            };

            var DTO = new ViewBookDTO()
            {
                Id = model.Id,
                Title = model.Title,
                PhotoUrl = model.PhotoUrl,
                Description = model.Description,
                Price = model.Price,
                Category = model.Category,
                AuthorId = model.AuthorId,
                Author = AuthorDTO,
                PublishedTime = model.PublishedTime
            };

            return DTO;
        }

        public async Task<List<ViewBookDTO>> GetBookByCategory(string category)
        {
            List<ViewBookDTO> booksDTO = [];

            var viewBooksCache = await _cacheHandler.GetCacheObject<List<ViewBookDTO>>(DataConfigurations.SearchBooksCacheFactor);

            if (viewBooksCache != default)
            {

                foreach (var book in viewBooksCache)
                {
                    if (book.Category == category)
                    {
                        booksDTO.Add(book);
                    }
                }

                return booksDTO;
            }

            var models = await _dbContext.Books
                .AsNoTracking()
                .Where(x => x.Category == category)
                .Include(x => x.Author)
                .ToListAsync();


            foreach (var booksModel in models)
            {
                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    PhotoUrl = booksModel.PhotoUrl,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };
                booksDTO.Add(DTO);
            }
            return booksDTO;
        }

        public async Task<List<ViewBookDTO>> GetBooksByCategories(List<string> categorysList)
        {
            List<ViewBookDTO> booksDTO = [];

            var viewBooksCache = await _cacheHandler.GetCacheObject<List<ViewBookDTO>>(DataConfigurations.SearchBooksCacheFactor);

            if (viewBooksCache != default)
            {

                foreach (var book in viewBooksCache)
                {
                    if (categorysList.Contains(book.Category))
                    {
                        booksDTO.Add(book);
                    }
                }

                return booksDTO;
            }


            var models = await _dbContext.Books
                .AsNoTracking()
                .Where(x => categorysList.Contains(x.Category))
                .Include(x => x.Author)
                .ToListAsync();


            foreach (var booksModel in models)
            {
                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    PhotoUrl = booksModel.PhotoUrl,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };
                booksDTO.Add(DTO);
            }
            return booksDTO;
        }

        public async Task<List<ViewBookDTO>> GetBookByAuthor(string authorName)
        {

            List<ViewBookDTO> booksDTO = [];

            var viewBooksCache = await _cacheHandler.GetCacheObject<List<ViewBookDTO>>(DataConfigurations.SearchBooksCacheFactor);

            if (viewBooksCache != default)
            {

                foreach (var book in viewBooksCache)
                {
                    if (book.Author.Name.Contains(authorName))
                    {
                        booksDTO.Add(book);
                    }
                }

                return booksDTO;
            }


            var models = await _dbContext.Books
                .AsNoTracking()
                .Where(x => x.Author.Name.Contains(authorName))
                .Include(x => x.Author)
                .ToListAsync();

            foreach (var booksModel in models)
            {
                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    PhotoUrl = booksModel.PhotoUrl,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };
                booksDTO.Add(DTO);
            }
            return booksDTO;
        }

        public async Task<List<ViewBookDTO>> GetBooksByAuthors(List<string> authorNameList)
        {

            List<ViewBookDTO> booksDTO = [];
            var viewBooksCache = await _cacheHandler.GetCacheObject<List<ViewBookDTO>>(DataConfigurations.SearchBooksCacheFactor);

            if (viewBooksCache != default)
            {

                foreach (var book in viewBooksCache)
                {
                    if (authorNameList.Contains(book.Author.Name))
                    {
                        booksDTO.Add(book);
                    }
                }

                return booksDTO;
            }


            var models = await _dbContext.Books
                .AsNoTracking()
                .Where(x => authorNameList.Contains(x.Author.Name))
                .Include(x => x.Author)
                .ToListAsync();

            foreach (var booksModel in models)
            {
                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    PhotoUrl = booksModel.PhotoUrl,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };

                booksDTO.Add(DTO);
            }

            return booksDTO;
        }

        public async Task<List<ViewBookDTO>> GetBookByName(string name)
        {
            List<ViewBookDTO> booksDTO = [];


            var models = await _dbContext.Books
                .AsNoTracking()
                .Where(x => _dbContext.FuzzySearch(x.Title) == _dbContext.FuzzySearch(name))
                .Include(x => x.Author)
                .ToListAsync();


            foreach (var booksModel in models)
            {

                var AuthorDTO = new ViewAuthorInBooksDTO()
                {
                    Id = booksModel.AuthorId,
                    Name = booksModel.Author.Name,
                    Bio = booksModel.Author.Bio,
                    DateOfBirth = booksModel.Author.DateOfBirth
                };

                var DTO = new ViewBookDTO()
                {
                    Id = booksModel.Id,
                    Title = booksModel.Title,
                    PhotoUrl = booksModel.PhotoUrl,
                    Description = booksModel.Description,
                    Price = booksModel.Price,
                    Category = booksModel.Category,
                    AuthorId = booksModel.AuthorId,
                    Author = AuthorDTO,
                    PublishedTime = booksModel.PublishedTime
                };

                booksDTO.Add(DTO);
            }

            return booksDTO;
        }

        public async Task<UpdateBookDTO> UpdateBook(IFormFile file, long id, UpdateBookDTO updateBookDTO)
        {
            var model = await _dbContext.Books
                .FindAsync(id) ??
                throw new Exception("The book is not found");

            string photoUrl = await _s3Service.PutNewS3ImageObject(file, updateBookDTO.Title);

            model.Title = updateBookDTO.Title;
            model.PhotoUrl = photoUrl;
            model.Description = updateBookDTO.Description;
            model.Price = updateBookDTO.Price;
            model.Category = updateBookDTO.Category;
            model.AuthorId = updateBookDTO.AuthorId;
            model.PublishedTime = updateBookDTO.PublishedTime;

            _dbContext.Books.Update(model);
            await _dbContext.SaveChangesAsync();

            await _cacheHandler.RemoveCache(DataConfigurations.SearchBooksCacheFactor);

            return updateBookDTO;

        }

        public async Task<bool> DeleteBook(long id)
        {
            var model = await _dbContext.Books
                .FindAsync(id) ??
                throw new ArgumentNullException("id", "The book is not found");

            if(!string.IsNullOrEmpty(model.PhotoUrl))
                await _s3Service.DeleteS3ImageObject(model.Title);


            _dbContext.Books.Remove(model);
            await _dbContext.SaveChangesAsync();

            await _cacheHandler.RemoveCache(DataConfigurations.SearchBooksCacheFactor);

            return true;
        }
    }
}
