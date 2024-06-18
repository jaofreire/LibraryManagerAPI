using LibraryManager.Core.DTOs.Book.ViewModel;
using LibraryManager.Core.Enums;

namespace LibraryManager.Core.DTOs.Order.InputModel
{
    public class CreateOrderDTO
    {
        public long UserId { get; set; }
        public List<ViewOrderBookDTO> Items { get; set; } = [];
        public double Amount { get; set; }
        public EPaymentMethod PaymentMethod { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
    }
}
