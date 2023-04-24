using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MarketPlace.Models
{
    public class RegisteredUser : IdentityUser
    {
        public double Balance { get; set; }
        public IList<Product> SellingProducts { get; set; }
    }
}
