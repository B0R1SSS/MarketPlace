using MarketPlace.Data;
using MarketPlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MarketPlace.Pages
{
	public class IndexModel : PageModel
	{
		private readonly MarketPlaceDbContext _dbContext;
		public IEnumerable<Product> Products { get; set; } = new List<Product>();
		public int? CategoryId { get; set; }
		public string? UserId { get; set; }
		public string? Heading { get; set; }

		public IndexModel(MarketPlaceDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task OnGetAsync(int? categoryId, bool ascSort, bool descSort, string? userId)
		{
			Heading = "Products for Sale";
            if (userId != null)
            {
				Products = await GetWatchlistProducts(userId);
            }
            else if (categoryId != null)
			{
				Products = await GetCategoryProducts((int) categoryId);
			}
			else
			{
				Products = await _dbContext.Products
					.Include(p => p.RegisteredUser)
					.Include(p => p.Category)
					.ToListAsync();
			}
			if (ascSort)
			{
				Products = Products.OrderBy(p => p.Price);
			}
			if (descSort)
			{
                Products = Products.OrderByDescending(p => p.Price);
            }
		}

		private async Task<IList<Product>> GetCategoryProducts(int categoryId)
		{
			CategoryId = categoryId;
			Heading = $"{(await _dbContext.Categories.FindAsync(categoryId))?.Name} Products";
			return await _dbContext.Products
                    .Include(p => p.RegisteredUser)
                    .Include(p => p.Category)
					.Where(p => p.Category.Id == categoryId)
					.ToListAsync();
        }

		private async Task<IList<Product>> GetWatchlistProducts(string userId)
		{
            var user = await _dbContext.Users
                    .Include(u => u.WatchlistProducts)
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();
            IList<Product> products = new List<Product>();
            if (user != null)
            {
				products = await _dbContext.Products
					.Include(p => p.RegisteredUser)
					.Include(p => p.Category)
					.Where(p => user.WatchlistProducts.Contains(p))
					.ToListAsync();
            }
			Heading = "Watchlist";
			UserId = userId;
			return products;
        }
	}
}