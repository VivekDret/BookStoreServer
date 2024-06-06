using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Context.ModelConfig
{
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            
            builder.ToTable("Payment");
            builder.HasKey(p => p.PaymentId);

            builder.Property(p => p.PaymentId).ValueGeneratedOnAdd();
            builder.Property(p => p.UserId);
            builder.HasOne(p => p.User).WithMany(u => u.Payments)
                .OnDelete(DeleteBehavior.NoAction).HasForeignKey(p => p.UserId);
            builder.Property(p => p.OrderId);
            builder.HasOne(p => p.Order).WithOne(o => o.Payment)
                .OnDelete(DeleteBehavior.NoAction).HasForeignKey<OrderTbl>(p => p.OrderID);
            builder.Property(p => p.RefundId);
            builder.HasOne(p => p.Refund).WithOne(r => r.Payment)
                .OnDelete(DeleteBehavior.NoAction).HasForeignKey<Refund>(p => p.RefundId);
            builder.Property(p => p.PaymentMode).HasMaxLength(50);
            builder.Property(p => p.Status).HasMaxLength(50);
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.PaymentDate).IsRequired();

        }
    }
}
