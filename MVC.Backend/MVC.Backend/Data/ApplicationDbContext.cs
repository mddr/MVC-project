using Microsoft.EntityFrameworkCore;
using MVC.Backend.Models;

namespace MVC.Backend.Data
{
    /// <summary>
    /// Kontekst bazodanowy
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(a => a.Email);

            modelBuilder.Entity<CartItem>()
                .HasKey(c => new {c.Id, c.ProductId});
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set;}
        public DbSet<Category> Categories { get; set;}
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
