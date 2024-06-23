using LibraryManager.Api.Repositories;
using LibraryManager.Api.Repositories.Interfaces;
using LibraryManager.Core.DTOs.Order.InputModel;
using LibraryManager.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace LibraryManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderRepository orderRepository, ILogger<OrderController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        [HttpPost("/order")]
        public async Task<ActionResult<CreateOrderDTO>> Create(CreateOrderDTO DTO)
        {
            try
            {
                return await _orderRepository.CreateOrder(DTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible create the order");
                throw;
            }
        }

        [HttpGet("/order/{userId}")]
        public async Task<ActionResult<List<OrderModel>>> GetAll(long userId)
        {
            try
            {
                return await _orderRepository.GetAllOrders(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the order");
                throw;
            }
        }

        [HttpGet("/order/{userId}/{id}")]
        public async Task<ActionResult<OrderModel>> GetById(long userId, ObjectId id)
        {
            try
            {
                return await _orderRepository.GetOrderById(id, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible get the order");
                throw;
            }

        }

        [HttpPut("/order/{id}")]
        public async Task<ActionResult<UpdateOrderDTO>> Update(long userId, ObjectId id, UpdateOrderDTO DTO)
        {
            try
            {
                return await _orderRepository.UpdateOrder(id, userId, DTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible update the order");
                throw;
            }

        }

        [HttpDelete("/order/{userId}/{id}")]
        public async Task<ActionResult<bool>> Delete(long userId, ObjectId id)
        {
            try
            {
                return await _orderRepository.DeleteOrder(id, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "There is not possible delete the order");
                throw;
            }

        }


    }
}
