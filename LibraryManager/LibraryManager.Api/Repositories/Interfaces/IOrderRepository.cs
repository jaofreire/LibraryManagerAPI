using LibraryManager.Core.DTOs.Order.InputModel;
using LibraryManager.Core.Models;

namespace LibraryManager.Api.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<CreateOrderDTO> CreateOrder(CreateOrderDTO model);
        Task<List<OrderModel>> GetAllOrders();
        Task<OrderModel> GetOrderById(long id, long UserId);
        Task<UpdateOrderDTO> UpdateOrder(long id, UpdateOrderDTO model);
        Task<bool> DeleteOrder(long id);
    }
}
