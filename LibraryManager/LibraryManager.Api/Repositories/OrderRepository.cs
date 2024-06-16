using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Core.DTOs.Order.InputModel;
using LibraryManager.Core.Models;

namespace LibraryManager.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Task<CreateOrderDTO> CreateOrder(CreateOrderDTO model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOrder(long id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderModel>> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public Task<OrderModel> GetOrderById(long id, long UserId)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateOrderDTO> UpdateOrder(long id, UpdateOrderDTO model)
        {
            throw new NotImplementedException();
        }
    }
}
