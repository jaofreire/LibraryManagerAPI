using LibraryManager.Data.Context;
using LibraryManager.Domain.Interfaces;
using LibraryManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManager.Data.Repositories;

public class AuthorRepository : IAuthorRepository
{

    private readonly LibraryDbContext _dbContext;

    public AuthorRepository(LibraryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AuthorModel> Register(AuthorModel model)
    {
        await _dbContext.Authors.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<List<AuthorModel>> RegisterMany(List<AuthorModel> models)
    {
        await _dbContext.Authors.AddRangeAsync(models);
        await _dbContext.SaveChangesAsync();
        return models;
    }

    public async Task<List<AuthorModel>> GetAll()
        => await _dbContext.Authors
            .Include(x => x.Books)
            .ToListAsync();

    public async Task<AuthorModel> GetById(long id)
        => await _dbContext.Authors
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<AuthorModel> GetByIdAsNoTracking(long id)
        => await _dbContext.Authors
            .AsNoTracking()
            .Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<AuthorModel>> GetByTerm(Expression<Func<AuthorModel, bool>> predicate)
        => await _dbContext.Authors
            .Where(predicate)
            .Include(x => x.Books)
            .ToListAsync();

    public async Task<List<AuthorModel>> GetByTermAsNoTracking(Expression<Func<AuthorModel, bool>> predicate)
        => await _dbContext.Authors
            .AsNoTracking()
            .Where(predicate)
            .Include(x => x.Books)
            .ToListAsync();

    public async Task<AuthorModel> Update(AuthorModel model)
    {
        _dbContext.Authors.Update(model);
        await _dbContext.SaveChangesAsync();
        return model;
    }

    public async Task<bool> Delete(AuthorModel model)
    {
        _dbContext.Authors.Remove(model);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}