using LibraryManager.Application.DTOs.Order.Input;
using LibraryManager.Domain.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Application.Utils.Mappings
{
    public static class OrderMapping
    {
        public static OrderModel ToModel(this CreateOrderDTO DTO)
        {
            return new OrderModel()
            {
                UserId = DTO.UserId,
                Items = DTO.Items.ToModelList(),
                Amount = DTO.Amount,
                PaymentMethod = DTO.PaymentMethod,
                OrderTime = DTO.OrderTime,
            };
        }
    }
}
