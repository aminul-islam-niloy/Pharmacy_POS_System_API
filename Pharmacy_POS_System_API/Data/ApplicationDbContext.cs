using Microsoft.EntityFrameworkCore;
using Pharmacy_POS_System_API.Models;

namespace Pharmacy_POS_System_API.Data
{
    public class ApplicationDbContext: DbContext
    {
     
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .HasMany(b => b.Categories)
                .WithOne(c => c.Brand);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category);

            modelBuilder.Entity<Order>()
                .OwnsMany(o => o.Items);
        }
    }
}
