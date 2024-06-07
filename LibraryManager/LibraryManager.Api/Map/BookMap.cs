using LibraryManager.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Api.Map
{
    public class BookMap : IEntityTypeConfiguration<BookModel>
    {
        public void Configure(EntityTypeBuilder<BookModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title);
            builder.Property(x => x.Description);
            builder.Property(x => x.Category);
            builder.Property(x => x.PublishedDate);

            builder.HasOne(x => x.Author).WithMany().HasForeignKey(x => x.AuthorId);
        }
    }
}
