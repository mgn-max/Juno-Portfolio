using Juno.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Juno.Infrastructure.Data.Configuration
{
    public class OfficeMembershipConfiguration : IEntityTypeConfiguration<OfficeMembership>
    {
        public void Configure(EntityTypeBuilder<OfficeMembership> builder)
        {
            builder.ToTable("OfficeMemberships");

            builder.HasKey(o => o.Id);

            builder.Property(r => r.UserId).IsRequired();
            builder.Property(r => r.OfficeId).IsRequired();
            builder.Property(r => r.Profile).HasConversion<int>().IsRequired();
            builder.Property(r => r.Status).HasConversion<int>().IsRequired();
            builder.Property(r => r.CreatedAt).IsRequired();

            builder.HasIndex(r => new { r.UserId, r.OfficeId }).IsUnique();

            builder.HasOne<User>().WithMany().HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Office>().WithMany().HasForeignKey(r => r.OfficeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
