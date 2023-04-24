using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [BindNever]
        public IList<Product> Products { get; set; }
    }
}
