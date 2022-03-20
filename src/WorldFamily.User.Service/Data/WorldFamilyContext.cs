using Microsoft.EntityFrameworkCore;
using WorldFamily.User.Service.Entities;

namespace WorldFamily.User.Service.Data
{
    public class WorldFamilyDbContext : DbContext
    {
        public WorldFamilyDbContext() { }
        public WorldFamilyDbContext(DbContextOptions<WorldFamilyDbContext> options)
            : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Member> Members { get; set; }
    }
}