using LibraryManager.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Api.Map
{
    public class UserMap : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(180);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(40);
                
            builder.Property(x => x.Role)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(10);
        }
    }
}
