namespace MarketPlace.Models
{
    public class UserProductWatchlist
    {
        public string RegisteredUserId { get; set; }
        public RegisteredUser RegisteredUser { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
