using Juno.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Juno.Infrastructure.Data
{
    public class JunoDbContext : DbContext
    {
        public JunoDbContext(DbContextOptions<JunoDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<OfficeMembership> OfficeMemberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(JunoDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
