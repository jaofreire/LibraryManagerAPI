using LibraryManager.Api.Data;
using LibraryManager.Api.Models;
using LibraryManager.Api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Api.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<BookModel>> GetAllAsync()
        {
            return await _dbContext.Books.Include(x => x.Author).ToListAsync();

        }

        public async Task<BookModel> GetByIdAsync(long id)
        {
            return await _dbContext.Books.Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == id) ??
                throw new Exception("The book is not found");
        }

        public async Task<List<BookModel>> GetByNameAsync(string name)
        {
            return await _dbContext.Books.Include(x => x.Author).Where(x => x.Title.Contains(name)).ToListAsync();
        }

        public async Task<List<BookModel>> GetByAuthorAsync(long authorId)
        {
            return await _dbContext.Books.Include(x => x.Author).Where(x => x.AuthorId == authorId).ToListAsync();
        }

        public async Task<List<BookModel>> GetByCategoryAsync(string categoryName)
        {
            return await _dbContext.Books.Include(x => x.Author).Where(x => x.Category.Contains(categoryName)).ToListAsync();
        }

        public async Task<BookModel> CreateAsync(BookModel newModel)
        {
            await _dbContext.Books.AddAsync(newModel);
            await _dbContext.SaveChangesAsync();

            return newModel;
        }

        public async Task<BookModel> UpdateAsync(long id, BookModel model)
        {
            var modelUpdate = await GetByIdAsync(id);

            modelUpdate.Title = model.Title;
            modelUpdate.Description = model.Description;
            modelUpdate.AuthorId = model.AuthorId;
            modelUpdate.Category = model.Category;
            modelUpdate.PublishedDate = model.PublishedDate;

            _dbContext.Books.Update(modelUpdate);
            await _dbContext.SaveChangesAsync();

            return modelUpdate;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var model = await GetByIdAsync(id);

            _dbContext.Books.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;
        }

       
    }
}
