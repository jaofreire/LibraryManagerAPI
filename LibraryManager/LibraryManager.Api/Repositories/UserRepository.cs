using LibraryManager.Api.Data;
using LibraryManager.Api.Models;
using LibraryManager.Api.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _dbContext;

        public UserRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> GetByIdAsync(long id)
        {
            return await _dbContext.Users.FindAsync(id) ??
                throw new Exception("The user is not found");
        }

        public async Task<List<UserModel>> GetByNameAsync(string name)
        {
            return await _dbContext.Users.Where(x => x.Name.Contains(name)).ToListAsync();
        }

        public async Task<UserModel> CreateAsync(UserModel newModel)
        {
            await _dbContext.Users.AddAsync(newModel);
            await _dbContext.SaveChangesAsync();

            return newModel;
        }

        public async Task<UserModel> UpdateAsync(long id, UserModel model)
        {
            var modelUpdate = await GetByIdAsync(id);

            modelUpdate.Name = model.Name;
            modelUpdate.Password = model.Password;
            modelUpdate.Role = model.Role;

            _dbContext.Users.Update(modelUpdate);
            await _dbContext.SaveChangesAsync();

            return modelUpdate;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var model = await GetByIdAsync(id);

            _dbContext.Users.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;
        }
  
    }
}
