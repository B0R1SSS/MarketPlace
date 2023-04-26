﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MarketPlace.Data;
using MarketPlace.Models;
using Microsoft.AspNetCore.Authorization;

namespace MarketPlace.Pages.Products
{
    [Authorize]
    public class ProductPageModel : PageModel
    {
        private readonly MarketPlaceDbContext _dbContext;

        public ProductPageModel(MarketPlaceDbContext context)
        {
            _dbContext = context;
        }

      public Product Product { get; set; } = default!; 

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
            return Page();
        }
    }
}
