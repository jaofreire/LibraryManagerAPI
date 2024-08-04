using LibraryManager.Application.DTOs.Author.Output;


namespace LibraryManager.Application.DTOs.Book.Output
{
    public record ViewBookDTO
        (
        long Id,
        string Title,
        string? PhotoUrl,
        string? Description,
        double Price,
        string Category,
        long AuthorId,
        ViewAuthorInBooksDTO? Author,
        DateTime PublishedDate

        );
    
}
