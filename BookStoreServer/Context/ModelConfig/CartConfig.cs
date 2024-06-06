using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Context.ModelConfig
{
    public class CartConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Cart");
            builder.HasKey(c => c.CartID);
            builder.Property(c => c.CartID).ValueGeneratedOnAdd();
            builder.Property(c => c.UserId);
            builder.Property(c => c.CartTotal).IsRequired();

            builder.HasOne(c => c.User)
                .WithMany(u => u.Carts).HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade).HasForeignKey(u => u.UserId).HasConstraintName("FK_User_Cart");

        }
    }
}
