using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPlace.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public string RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

        public IList<RegisteredUser> WatchlistUsers { get; set; }
        public IList<UserProductWatchlist> UserProductWatchlist { get; set; }
    }
}
