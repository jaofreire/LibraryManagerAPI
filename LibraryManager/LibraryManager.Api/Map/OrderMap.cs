using LibraryManager.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManager.Api.Map
{
    public class OrderMap : IEntityTypeConfiguration<OrderModel>
    {
        public void Configure(EntityTypeBuilder<OrderModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Items)
                .IsRequired();

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnType("DECIMAL");

            builder.Property(x => x.PaymentMethod)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasMaxLength(60);

            builder.Property(x => x.OrderTime)
                .IsRequired();

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        }
    }
}
