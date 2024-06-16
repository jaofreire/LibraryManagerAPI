using LibraryManager.Core.Enums;
using LibraryManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Core.DTOs.Order.InputModel
{
    public class CreateOrderDTO
    {
        public long UserId { get; set; }
        public UserModel? User { get; set; }
        public List<BookModel> Items { get; set; } = [];
        public double Amount { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
    }
}
