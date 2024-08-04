using LibraryManager.Application.DTOs.Book.Output;
using LibraryManager.Domain.Enums;

namespace LibraryManager.Application.DTOs.Order.Input
{
    public record CreateOrderDTO
        (
         long UserId,
         List<ViewBooksInOrderDTO> Items,
         double Amount,
         string PaymentMethod,
         DateTime OrderTime
        );
}
