using LibraryManager.Api.Data;
using LibraryManager.Api.Models;
using LibraryManager.Api.Repositories.Interface;
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

        public async Task<List<AuthorModel>> GetAllAsync()
        {
            return await _dbContext.Authors.ToListAsync();
        }

        public async Task<AuthorModel> GetByIdAsync(long id)
        {
            return await _dbContext.Authors.FindAsync(id) ??
                throw new Exception("The auhor is not found");
        }

        public async Task<List<AuthorModel>> GetByNameAsync(string name)
        {
            return await _dbContext.Authors.Where(x => x.Name.Contains(name)).ToListAsync();
        }

        public async Task<AuthorModel> CreateAsync(AuthorModel newModel)
        {
            await _dbContext.Authors.AddAsync(newModel);
            await _dbContext.SaveChangesAsync();

            return newModel;
        }

        public async Task<AuthorModel> UpdateAsync(long id, AuthorModel model)
        {
            var modelUpdate = await GetByIdAsync(id);

            modelUpdate.Name = model.Name;
            modelUpdate.Bio = model.Bio;
            modelUpdate.DateOfBirth = model.DateOfBirth;

            _dbContext.Authors.Update(modelUpdate);
            await _dbContext.SaveChangesAsync();

            return modelUpdate;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var model = await GetByIdAsync(id);

            _dbContext.Authors.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;
        }

            
    }
}
