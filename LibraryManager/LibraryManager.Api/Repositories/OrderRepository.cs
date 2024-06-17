using LibraryManager.Api.Data;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Core.DTOs.Order.InputModel;
using LibraryManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LibraryDbContext _dbContext;

        public OrderRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateOrderDTO> CreateOrder(CreateOrderDTO modelDTO)
        {
            var model = new OrderModel()
            {
                UserId = modelDTO.UserId,
                Items = modelDTO.Items,
                Amount = modelDTO.Amount,
                PaymentMethod = modelDTO.PaymentMethod,
                OrderTime = modelDTO.OrderTime,
            };

            await _dbContext.Orders.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return modelDTO;

        }

        public async Task<List<OrderModel>> GetAllOrders(long UserId)
            => await _dbContext.Orders
            .AsNoTracking()
            .Where(x => x.UserId == UserId)
            .ToListAsync();

        public async Task<OrderModel> GetOrderById(long id, long UserId)
            => await _dbContext.Orders
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == UserId) ??
            throw new Exception("The order is not found");


        public async Task<UpdateOrderDTO> UpdateOrder(long id, long UserId, UpdateOrderDTO modelDTO)
        {
           var modelUpdate = await _dbContext.Orders
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == UserId) ??
            throw new Exception("The order is not found");

            modelUpdate.Items = modelDTO.Items;
            modelUpdate.PaymentMethod = modelDTO.PaymentMethod;

            _dbContext.Orders.Update(modelUpdate);
            await _dbContext.SaveChangesAsync();

            return modelDTO;
        }

        public async Task<bool> DeleteOrder(long id, long UserId)
        {
            var modelUpdate = await _dbContext.Orders
           .FirstOrDefaultAsync(x => x.Id == id && x.UserId == UserId) ??
           throw new Exception("The order is not found");

            _dbContext.Orders.Remove(modelUpdate);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
