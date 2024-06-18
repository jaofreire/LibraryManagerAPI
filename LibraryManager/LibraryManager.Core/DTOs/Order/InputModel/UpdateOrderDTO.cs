using LibraryManager.Core.DTOs.Book.ViewModel;
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
        public List<ViewOrderBookDTO> Items { get; set; } = [];
        public EPaymentMethod PaymentMethod { get; set; }
    }
}
