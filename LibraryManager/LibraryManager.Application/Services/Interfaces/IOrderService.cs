using LibraryManager.Application.DTOs.Order.Input;
using LibraryManager.Application.Responses;
using LibraryManager.Domain.Models;
using MongoDB.Bson;

namespace LibraryManager.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<APIResponse<CreateOrderDTO>> CreateOrder(CreateOrderDTO model);
        Task<APIResponse<OrderModel>> GetAllOrders();
        Task<APIResponse<OrderModel>> GetOrderByUserId(long UserId);
    }
}
