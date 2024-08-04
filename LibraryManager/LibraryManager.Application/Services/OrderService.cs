using LibraryManager.Application.DTOs.Order.Input;
using LibraryManager.Application.Services.Interfaces;
using LibraryManager.Application.Utils.Mappings;
using LibraryManager.Domain.Interfaces;
using LibraryManager.Domain.Models;
using LibraryManager.Application.Responses;
using LibraryManager.Application.Enums;


namespace LibraryManager.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<APIResponse<CreateOrderDTO>> CreateOrder(CreateOrderDTO DTO)
        {
            var model = DTO.ToModel();
            await _orderRepository.Register(model);

            return new APIResponse<CreateOrderDTO>(
                operationType: EOperationType.Create.ToString(),
                true,
                201,
                "Order created successfully",
                dataResponse: DTO,
                dataResponseList: null
                );

        }

        public async Task<APIResponse<OrderModel>> GetAllOrders()
        {
            var models = await _orderRepository.GetAll();

            return new APIResponse<OrderModel>(
               operationType: EOperationType.Get.ToString(),
               true,
               200,
               "listing all registered orders sucessfully",
               dataResponse: null,
               dataResponseList: models!
               );

        }

        public async Task<APIResponse<OrderModel>> GetOrderByUserId(long userId)
        {
            var models = await _orderRepository.GetByTermAsNoTracking(x => x.UserId == userId);

            return new APIResponse<OrderModel>(
               operationType: EOperationType.Get.ToString(),
               true,
               200,
               "listing registered orders with specify UserId sucessfully",
               dataResponse: null,
               dataResponseList: models!
               );
        }
    }
}
