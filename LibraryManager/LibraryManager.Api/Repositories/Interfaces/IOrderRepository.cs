using LibraryManager.Core.DTOs.Order.InputModel;
using LibraryManager.Core.Models;
using MongoDB.Bson;

namespace LibraryManager.Api.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<CreateOrderDTO> CreateOrder(CreateOrderDTO model);
        Task<List<OrderModel>> GetAllOrders(long UserId);
        Task<OrderModel> GetOrderById(ObjectId id, long UserId);
        Task<UpdateOrderDTO> UpdateOrder(ObjectId id, long UserId, UpdateOrderDTO model);
        Task<bool> DeleteOrder(ObjectId id, long UserId);
    }
}
