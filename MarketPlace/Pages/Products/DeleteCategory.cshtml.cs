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
    public class DeleteCategoryModel : PageModel
    {
        private readonly MarketPlaceDbContext _dbContext;

        public DeleteCategoryModel(MarketPlaceDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> OnPostAsync(int? categoryId)
        {
            if (categoryId == null || _dbContext.Categories == null)
            {
                return NotFound();
            }
            var category = await _dbContext.Categories.FindAsync(categoryId);

            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToPage("/Products/ListCategories");
        }
    }
}
