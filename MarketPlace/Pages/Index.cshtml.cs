using MarketPlace.Data;
using MarketPlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Pages
{
	public class IndexModel : PageModel
	{
		private readonly MarketPlaceDbContext _dbContext;
		public IEnumerable<Product> Products { get; set; } = new List<Product>();
		public int? CategoryId { get; set; }

		public IndexModel(MarketPlaceDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task OnGetAsync(int? categoryId, bool ascSort, bool descSort)
		{
			IQueryable<Product> products;
			if (categoryId == null)
			{
				products = _dbContext.Products
					.Include(p => p.RegisteredUser)
					.Include(p => p.Category);
			}
			else
			{
				CategoryId = categoryId;
                products = _dbContext.Products
					.Where(p => p.Category.Id == categoryId)
					.Include(p => p.RegisteredUser)
					.Include(p => p.Category);
            }
			if (ascSort)
			{
				products = products.OrderBy(p => p.Price);
			}
			if (descSort)
			{
                products = products.OrderByDescending(p => p.Price);
            }
			Products = await products.ToListAsync();
		}
	}
}