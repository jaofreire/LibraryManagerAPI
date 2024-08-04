using LibraryManager.Application.DTOs.Book.Output;
using LibraryManager.Domain.Enums;

namespace LibraryManager.Application.DTOs.Order.Input
{
    public record UpdateOrderDTO
        (
         List<ViewBooksInOrderDTO> Items,
         string PaymentMethod
        );
}
