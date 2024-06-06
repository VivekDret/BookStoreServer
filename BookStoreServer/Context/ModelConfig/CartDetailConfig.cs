using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Context.ModelConfig
{
    public class CartDetailConfig : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.ToTable("CartDetail");
            builder.HasKey(cd => cd.CartDetailID);
            builder.Property(cd => cd.CartDetailID).ValueGeneratedOnAdd();
            builder.Property(cd => cd.CartQuantity).IsRequired();
            builder.Property(cd => cd.SubTotalCart).IsRequired();
            builder.HasOne(cd => cd.Cart).WithMany(c => c.CartDetails)
                .OnDelete(DeleteBehavior.Cascade).HasForeignKey(cd => cd.CartID);
            builder.HasOne(cd => cd.Book)
                .WithMany(b => b.CartDetails).OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(b => b.BookId).HasConstraintName("FK_Book_CartDetail");


        }
    }
}
