using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Context.ModelConfig
{
    public class RefundConfig : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.ToTable("Refund");

            builder.HasKey(e => e.RefundId);
            builder.Property(e => e.RefundId).ValueGeneratedOnAdd();

            builder.Property(e => e.RefundId).HasColumnType("int").IsRequired();
            builder.Property(e => e.PaymentId).HasColumnType("int").IsRequired();
            builder.Property(e => e.RefundAmount).HasColumnType("decimal(10, 2)").IsRequired();
            builder.Property(e => e.RefundDate).HasColumnType("datetime").IsRequired();
            builder.Property(e => e.Reason).HasMaxLength(500);
            builder.Property(e => e.RefundStatus).HasMaxLength(50).HasDefaultValue("Not Approved");

            builder.HasOne(e => e.Payment)
                .WithOne(p => p.Refund)
                .HasForeignKey<Refund>(e => e.PaymentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Refund_Payment");
        }
    }
}
