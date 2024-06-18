using LibraryManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Api.Map
{
    public class AuthorMap : IEntityTypeConfiguration<AuthorModel>
    {
        public void Configure(EntityTypeBuilder<AuthorModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(180);

            builder.Property(x => x.Bio)
                .IsRequired(false)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(260);

            builder.Property(x => x.DateOfBirth)
                .IsRequired();

        }
    }
}
