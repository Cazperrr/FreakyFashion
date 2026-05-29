using FreakyFashion.Models;
using Microsoft.EntityFrameworkCore;

namespace FreakyFashion.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}
