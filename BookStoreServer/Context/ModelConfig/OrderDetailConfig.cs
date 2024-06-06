using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Context.ModelConfig
{
    public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetail");
            builder.HasKey(od => od.OrderID);
            builder.Property(od => od.OrderDetailID).ValueGeneratedOnAdd();            
            builder.Property(od => od.BookID);            
            builder.Property(od => od.OrderQuantity).IsRequired();
            builder.Property(od => od.SubTotalOrder).IsRequired();
            builder.HasOne(od => od.Order).WithMany(o => o.OrderDetails)
                .OnDelete(DeleteBehavior.Cascade).HasForeignKey(od => od.OrderID);
            builder.HasOne(od => od.Book).WithMany(b => b.OrdertDetails)
                .OnDelete(DeleteBehavior.NoAction).HasForeignKey(od => od.BookID);
        }
    }
}
