using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Context.ModelConfig
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(c => c.CategoryID);
            builder.Property(c => c.CategoryID).ValueGeneratedOnAdd();
            builder.Property(c => c.CategoryName).IsRequired().HasMaxLength(50);                        
        }
    }
}
