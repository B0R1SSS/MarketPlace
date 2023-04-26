using MarketPlace.Data;
using MarketPlace.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace MarketPlace.Pages.Products
{
    [Authorize]
    public class CreateProductModel : PageModel
    {
        private readonly MarketPlaceDbContext _dbContext;
        private readonly UserManager<RegisteredUser> _userManager;

        [BindProperty]
        public InputModel Input { get; set; }
        public SelectList Categories { get; set; }

        public CreateProductModel(MarketPlaceDbContext dbContext, UserManager<RegisteredUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            Categories = new SelectList(_dbContext.Categories, "Id", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Categories = new SelectList(_dbContext.Categories, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await _userManager.GetUserAsync(User);
            var product = new Product
            {
                Name = Input.Name,
                Price = Input.Price,
                Description = Input.Description,
                Amount = Input.Amount,
                RegisteredUserId = user.Id,
                CategoryId = Input.CategoryId
            };
			await _dbContext.Products.AddAsync(product);
			await _dbContext.SaveChangesAsync();
			return RedirectToPage("/Index");
        }

        public class InputModel
        {
            [Required]
            [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
            public string Name { get; set; }

            [Required]
            public string Description { get; set; }

            [Required]
            [Range(0.01, double.MaxValue, ErrorMessage = "The value must be greater than 0")]
            public double Price { get; set; }

            [Required]
            [Range(1, int.MaxValue, ErrorMessage = "The value must be a positive integer.")]
            public int Amount { get; set; }

            [DisplayName("Category")]
            public int? CategoryId { get; set; }
        }
    }
}
