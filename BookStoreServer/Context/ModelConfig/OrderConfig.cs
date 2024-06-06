using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Context.ModelConfig
{
    public class OrderConfig : IEntityTypeConfiguration<OrderTbl>
    {
        public void Configure(EntityTypeBuilder<OrderTbl> builder)
        {
            builder.ToTable("OrderTbl");
            builder.HasKey(o => o.OrderID);
            builder.Property(o => o.OrderID).ValueGeneratedOnAdd();
            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.OrderTotal).IsRequired();
            builder.Property(o => o.UserId);
            builder.HasOne(o => o.User).WithMany(u => u.Orders).HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.NoAction);
                        
        }
    }
}
