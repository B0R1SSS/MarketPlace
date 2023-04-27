using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MarketPlace.Data;
using MarketPlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MarketPlace.Pages.Products
{
    [Authorize]
    public class ProductPageModel : PageModel
    {
        private readonly MarketPlaceDbContext _dbContext;
        private readonly UserManager<RegisteredUser> _userManager;


        public ProductPageModel(MarketPlaceDbContext dbContext, UserManager<RegisteredUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public Product Product { get; set; } = default!; 
        public bool IsInWatchlist { get; set; }

        public async Task<IActionResult> OnGetAsync(int? productId)
        {
            if (productId == null || _dbContext.Products == null)
            {
                return RedirectToPage("/Index");
            }

            var product = await _dbContext.Products
                .Include(product => product.Category)
                .Include(product => product.RegisteredUser)
                .FirstOrDefaultAsync(product => product.Id == productId);
            if (product == null)
            {
                return RedirectToPage("/Index");
            }
            Product = product;
            var user = await _userManager.GetUserAsync(User);
            IsInWatchlist = await _dbContext.UsersProductsWatchlist.FindAsync(user.Id, productId) != null;
            return Page();
        }
    }
}
