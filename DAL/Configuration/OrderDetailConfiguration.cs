using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DAL.Entities; // Replace with your actual namespace

namespace DAL.Configuration // Replace with your actual namespace
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(od => od.Id);
            builder.Property(od => od.Quantity)
                   .IsRequired();
            // Relationships
            builder.HasOne(od => od.Order)
                   .WithMany(o => o.OrderDetails)
                   .HasForeignKey(od => od.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(od => od.Product)
                   .WithMany(p => p.OrderDetails)
                   .HasForeignKey(od => od.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(
                new OrderDetail { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2 },
                new OrderDetail { Id = 2, OrderId = 1, ProductId = 2, Quantity = 1 },
                new OrderDetail { Id = 3, OrderId = 2, ProductId = 2, Quantity = 3 }
            );
        }
    }
}

