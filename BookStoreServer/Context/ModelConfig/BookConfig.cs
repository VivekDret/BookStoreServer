using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Context.ModelConfig
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(b => b.BookID);
            builder.Property(b => b.BookID).ValueGeneratedOnAdd();
            builder.Property(b => b.BookTitle).IsRequired().HasMaxLength(100);
            builder.Property(b => b.BookYear).IsRequired();
            builder.Property(b => b.BookImageLink).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(b => b.BookSerialNumber).IsRequired().HasMaxLength(50);
            builder.Property(b => b.BookQuantity).IsRequired();
            builder.Property(b => b.BookPrice).IsRequired();
            builder.Property(b => b.AuthorID);
            builder.Property(b => b.CategoryID);
      
            builder.HasOne(b => b.Category)
                .WithMany(c => c.Books).HasForeignKey(b => b.CategoryID)
                .OnDelete(DeleteBehavior.NoAction).HasForeignKey(c => c.CategoryID)
                .HasConstraintName("FK_Book_Category");


        }
    }
}
