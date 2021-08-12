using GuruSoft.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GuruSoft.Data.Context
{
    public class GuruContext : DbContext
    {
        public GuruContext()
        {

        }
        public GuruContext(DbContextOptions<GuruContext> options) : base(options)
        {

        }
        // Specify DbSet properties etc
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(t => t.Price).HasPrecision(18, 2);
            modelBuilder.Entity<Product>().Property(t => t.Total).HasPrecision(18, 2);
        }
        public virtual DbSet<Product> Products { get; set; }
    }
}
