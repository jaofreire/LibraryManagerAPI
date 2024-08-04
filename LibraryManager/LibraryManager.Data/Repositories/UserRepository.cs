using LibraryManager.Data.Context;
using LibraryManager.Domain.Interfaces;
using LibraryManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManager.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LibraryDbContext _dbContext;

        public UserRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserModel> RegisterUser(UserModel model)
        {
            await _dbContext.Users.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }
            
        public async Task<List<UserModel>> GetAllUsers()
            => await _dbContext.Users
            .AsNoTracking()
            .ToListAsync();

        public async Task<UserModel> GetUserById(long id)
            => await _dbContext.Users
            .FindAsync(id);

        public async Task<UserModel> GetUserByIdAsNoTracking(long id)
            => await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<List<UserModel>> GetUserByTerm(Expression<Func<UserModel, bool>> predicate)
        {
            var models =  await _dbContext.Users
                .Where(predicate)
                .ToListAsync();

            return models;
        }

        public async Task<UserModel> UpdateUser(UserModel model)
        {
            _dbContext.Users.Update(model);

            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> DeleteUser(UserModel model)
        {
            _dbContext.Users.Remove(model);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
