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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                        .HasOne(x => x.RegisteredUser)
                        .WithMany(x => x.SellingProducts)
                        .HasForeignKey(x => x.RegisteredUserId)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                        .HasOne(x => x.Category)
                        .WithMany(x => x.Products)
                        .HasForeignKey(x => x.CategoryId)
                        .IsRequired(false)
						.OnDelete(DeleteBehavior.SetNull);
        }
    }
}
