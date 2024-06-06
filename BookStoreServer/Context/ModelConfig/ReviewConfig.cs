using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Context.ModelConfig
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            
            builder.ToTable("Review");
            builder.HasKey(r => r.ReviewID);
            builder.Property(r => r.ReviewID).ValueGeneratedOnAdd();
            builder.Property(r => r.ReviewComment).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Rating).HasColumnType("int").IsRequired();
            builder.Property(r => r.BookID).HasColumnType("int").IsRequired(false);
            builder.Property(r => r.UserId).HasColumnType("int").IsRequired();
            builder.HasOne(r => r.Book)
                .WithMany(r => r.Reviews)
                .HasForeignKey(p => p.BookID)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("FK_Review_Book");
            builder.HasOne(r => r.User)
                .WithMany(r => r.Reviews)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction   )
                .HasConstraintName("FK_Review_User");
        }
    }
}
