using Microsoft.EntityFrameworkCore;
using MarketPlace.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MarketPlace.Data
{
    public class MarketPlaceDbContext : IdentityDbContext<RegisteredUser>
    {
        public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserProductWatchlist> UsersProductsWatchlist { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                        .HasOne(product => product.RegisteredUser)
                        .WithMany(user => user.SellingProducts)
                        .HasForeignKey(product => product.RegisteredUserId)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                        .HasOne(product => product.Category)
                        .WithMany(category => category.Products)
                        .HasForeignKey(product => product.CategoryId)
                        .IsRequired(false)
						.OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserProductWatchlist>()
                .HasKey(w => new { w.RegisteredUserId, w.ProductId });

            modelBuilder.Entity<RegisteredUser>()
                .HasMany(user => user.WatchlistProducts)
                .WithMany(product => product.WatchlistUsers)
                .UsingEntity<UserProductWatchlist>();
        }
    }
}
