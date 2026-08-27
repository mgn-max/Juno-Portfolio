using Juno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Juno.Infrastructure.Data.Configuration
{
    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable("Offices");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Name).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Email).HasMaxLength(200);
            builder.Property(o => o.DocumentNumber).HasMaxLength(50);
            builder.Property(o => o.PhoneNumber).HasMaxLength(15);
            builder.Property(o => o.LogoUrl).HasMaxLength(500);

            builder.Property(o => o.ZipCode).HasMaxLength(15);
            builder.Property(o => o.Street).HasMaxLength(255);
            builder.Property(o => o.AddressNumber).HasMaxLength(20);
            builder.Property(o => o.Neighborhood).HasMaxLength(100);
            builder.Property(o => o.City).HasMaxLength(100);
            builder.Property(o => o.State).HasMaxLength(50);
            builder.Property(o => o.Country).HasMaxLength(60);

            builder.Property(o => o.Status).IsRequired();
            builder.Property(o => o.CreatedAt).IsRequired();

            builder.HasIndex(o => o.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
            builder.HasIndex(o => o.DocumentNumber).IsUnique().HasFilter("[DocumentNumber] IS NOT NULL");
        }
    }
}
