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
		public IEnumerable<Product> Products { get; set; }

		public IndexModel(MarketPlaceDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task OnGetAsync()
		{
			Products = await _dbContext.Products
				.Include(p => p.RegisteredUser)
				.Include(p => p.Category)
				.ToListAsync();
		}
	}
}