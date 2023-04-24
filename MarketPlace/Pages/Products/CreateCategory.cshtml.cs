using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MarketPlace.Data;
using MarketPlace.Models;

namespace MarketPlace.Pages.Products
{
    public class CreateCategoryModel : PageModel
    {
        private readonly MarketPlaceDbContext _context;

        public CreateCategoryModel(MarketPlaceDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.ErrorCount > 1)
            {
                return Page();
            }

            await _context.Categories.AddAsync(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
