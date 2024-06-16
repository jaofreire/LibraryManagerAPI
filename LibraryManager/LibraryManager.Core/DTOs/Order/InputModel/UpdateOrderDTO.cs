using LibraryManager.Core.Enums;
using LibraryManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.Core.DTOs.Order.InputModel
{
    public class UpdateOrderDTO
    {
        public List<BookModel> Items { get; set; } = [];
        public EPaymentMethod PaymentMethod { get; set; }
    }
}
