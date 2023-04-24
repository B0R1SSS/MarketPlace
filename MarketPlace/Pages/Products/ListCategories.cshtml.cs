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
    public class ListCategoriesModel : PageModel
    {
        private readonly MarketPlaceDbContext _dbContext;

        public ListCategoriesModel(MarketPlaceDbContext context)
        {
            _dbContext = context;
        }

        public IList<Category> Categories { get; set; } = new List<Category>();

        public async Task OnGetAsync()
        {
            Categories = await _dbContext.Categories
                .Include(x => x.Products)
                .ToListAsync();
        }
    }
}
