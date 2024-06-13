namespace LibraryManager.Api.DTOs.Author
{
    public class UpdateAuthorDTO
    {
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
