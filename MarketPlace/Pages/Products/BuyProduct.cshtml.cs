using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MarketPlace.Data;
using MarketPlace.Models;

namespace MarketPlace.Pages.Products
{
    public class BuyProductModel : PageModel
    {
        private readonly MarketPlaceDbContext _dbContext;

        public BuyProductModel(MarketPlaceDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> OnPostAsync(int? productId, int amount, string  buyerId)
        {
            if (productId == null || _dbContext.Products == null || amount < 1)
            {
                return RedirectToPage("/Index");
                
            }
            var product = await _dbContext.Products
                .Include(p => p.RegisteredUser)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            if (product == null || amount > product.Amount)
            {
                return RedirectToPage("/Index");
            }

            var buyer = await _dbContext.Users.FindAsync(buyerId);
            
            if (buyer == null)
            {
                return RedirectToPage("/Index");
            }

            await MakeTransaction(product, amount, buyer);

            if (product.Amount <= 0)
            {
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Products/ProductPage", new { productId = product.Id });
        }

        private async Task<bool> MakeTransaction(Product product, int amount, RegisteredUser buyer)
        {
            double totalPrice = product.Price * amount;
            if (buyer.Balance < totalPrice)
            {
                return false;
            }
            buyer.Balance -= totalPrice;
            product.RegisteredUser.Balance += totalPrice;
            product.Amount -= amount;
            if (product.Amount <= 0)
            {
                _dbContext.Products.Remove(product);
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
