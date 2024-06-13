namespace LibraryManager.Api.DTOs.Book
{
    public class CreateBookDTO
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long? AuthorId { get; set; }
        public string Category { get; set; } = string.Empty;
        public DateTime? PublishedDate { get; set; }
    }
}
