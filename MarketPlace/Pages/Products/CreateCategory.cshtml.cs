using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MarketPlace.Data;
using MarketPlace.Models;
using Microsoft.AspNetCore.Authorization;
using System.Configuration;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Pages.Products
{
    [Authorize]
    public class CreateCategoryModel : PageModel
    {
        private readonly MarketPlaceDbContext _context;

        public CreateCategoryModel(MarketPlaceDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string CategoryName { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Category category = new Category { Name = CategoryName };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Products/ListCategories");
        }
    }
}
