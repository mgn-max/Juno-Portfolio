using Juno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juno.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Login).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(200);
            builder.Property(u => u.PasswordHash).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Status).IsRequired();
            builder.Property(u => u.PhotoUrl).HasMaxLength(500);
            builder.Property(u => u.CreatedAt).IsRequired();

            builder.HasIndex(u => u.Login).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
