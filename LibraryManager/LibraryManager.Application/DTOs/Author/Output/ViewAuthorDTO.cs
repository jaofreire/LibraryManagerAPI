using LibraryManager.Application.DTOs.Book.Output;

namespace LibraryManager.Application.DTOs.Author.Output
{
    public record ViewAuthorDTO
        (
        long Id,
        string Name,
        string Bio,
        DateTime? DateOfBirth,
        List<ViewBooksInAuthorDTO> Books
        );

}
