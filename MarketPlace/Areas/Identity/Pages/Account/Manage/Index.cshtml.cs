// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using MarketPlace.Data;
using MarketPlace.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MarketPlace.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<RegisteredUser> _userManager;
        private readonly SignInManager<RegisteredUser> _signInManager;
        private readonly MarketPlaceDbContext _dbContext;

        public IndexModel(
            UserManager<RegisteredUser> userManager,
            SignInManager<RegisteredUser> signInManager,
            MarketPlaceDbContext dbContext)
        {
            _dbContext= dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public RegisteredUser CurrentUser { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            CurrentUser = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(float amount)
        {
            var user = await _userManager.GetUserAsync(User);
            user.Balance += amount;
            await _dbContext.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
