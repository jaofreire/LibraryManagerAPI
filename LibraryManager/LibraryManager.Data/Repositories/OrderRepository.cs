using LibraryManager.Data.Context;
using LibraryManager.Domain.Interfaces;
using LibraryManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManager.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {

        private readonly LibraryMongoDbContext _dbContext;

        public OrderRepository(LibraryMongoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OrderModel> Register(OrderModel model)
        {
            await _dbContext.Orders.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<List<OrderModel>> GetAll()
            => await _dbContext.Orders.ToListAsync();
               

        public async Task<List<OrderModel>> GetByTerm(Expression<Func<OrderModel, bool>> predicate)
            => await _dbContext.Orders
                .Where(predicate)
                .ToListAsync();

        public async Task<List<OrderModel>> GetByTermAsNoTracking(Expression<Func<OrderModel, bool>> predicate)
            => await _dbContext.Orders
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync();

        public async Task<OrderModel> Update(OrderModel model)
        {
            _dbContext.Orders.Update(model);
            await _dbContext.SaveChangesAsync();

            return model;
        }

        public async Task<bool> Delete(OrderModel model)
        {
            _dbContext.Orders.Remove(model);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
