using MarketPlace.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Pages.Products
{
    [Authorize]
    public class EditWatchlist : PageModel
    {
        private readonly MarketPlaceDbContext _dbContext;

        public EditWatchlist(MarketPlaceDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> OnPostAsync(int? productId, string userId, bool add)
        {
            if (productId == null || _dbContext.Products == null)
            {
                return RedirectToPage("/Index");

            }
            var product = await _dbContext.Products.FindAsync(productId);

            if (product == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _dbContext.Users
                .Include(u => u.WatchlistProducts)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return RedirectToPage("/Index");
            }
            
            if (add)
            {
                user.WatchlistProducts.Add(product);
            }
            else
            {
                user.WatchlistProducts.Remove(product);
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToPage("/Products/ProductPage", new { productId = product.Id });
        }
    }
}
