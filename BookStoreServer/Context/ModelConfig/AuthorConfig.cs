using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStoreServer.Context.ModelConfig
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");
            builder.HasKey(a => a.AuthorID);
            builder.Property(a => a.AuthorID).ValueGeneratedOnAdd();
            builder.Property(a => a.AuthorName).IsRequired().HasMaxLength(200);
            builder.Property(a => a.AuthorAddress).IsRequired().HasMaxLength(200);
            builder.Property(a => a.AuthorGender).IsRequired().HasMaxLength(200);
            builder.Property(a => a.AuthorContact).IsRequired().HasMaxLength(200);

            builder.HasMany(a => a.Books).WithOne(b => b.Author)
                .OnDelete(DeleteBehavior.SetNull).HasForeignKey(a => a.AuthorID)
                .HasConstraintName("FK_Book_Author");

        }
    }
}
