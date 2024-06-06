using BookStoreServer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookStoreServer.Context.ModelConfig
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId).ValueGeneratedOnAdd();
            builder.Property(u => u.UserFirstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.UserProfilePic).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(u => u.UserLastName).IsRequired().HasMaxLength(50);

            builder.Property(u => u.UserEmail).IsRequired().HasMaxLength(100);
            builder.Property(u => u.UserPassword).IsRequired().HasMaxLength(100);
            builder.Property(u => u.UserRole).IsRequired().HasMaxLength(50);
            builder.Property(u => u.UserGender).IsRequired().HasMaxLength(10);
            builder.Property(u => u.UserAddress).IsRequired().HasMaxLength(100);
            builder.Property(u => u.UserContact).IsRequired().HasMaxLength(20);
        }
    }
}
