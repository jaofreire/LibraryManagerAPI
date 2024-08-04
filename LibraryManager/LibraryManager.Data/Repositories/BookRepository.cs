using LibraryManager.Data.Context;
using LibraryManager.Domain.Interfaces;
using LibraryManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace LibraryManager.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        public BookRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BookModel> Register(BookModel model)
        {
            await _dbContext.Books.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<BookModel>> RegisterMany(List<BookModel> models)
        {
            await _dbContext.Books.AddRangeAsync(models);
            await _dbContext.SaveChangesAsync();

            return models;
        }

        public async Task<List<BookModel>> GetAll()
            => await _dbContext.Books
            .Include(x => x.Author)
            .ToListAsync();

        public async Task<BookModel> GetById(long id)
            => await _dbContext.Books
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<BookModel> GetByIdAsNoTracking(long id)
            => await _dbContext.Books
            .AsNoTracking()
            .Include(x => x.Author)
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<BookModel>> GetByTerm(Expression<Func<BookModel, bool>> predicate)
            => await _dbContext.Books
            .Where(predicate)
            .Include(x => x.Author)
            .ToListAsync();

        public async Task<List<BookModel>> GetByTermAsNoTracking(Expression<Func<BookModel, bool>> predicate)
            => await _dbContext.Books
            .AsNoTracking()
            .Where(predicate)
            .Include(x => x.Author)
            .ToListAsync();

        public async Task<BookModel> Update(BookModel model)
        {
            _dbContext.Books.Update(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> Delete(BookModel model)
        {
            _dbContext.Books.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;
        }



    }
}
