using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages
{
    public class Product
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Category { get; set; } = "";
    }

    public class IndexModel : PageModel
    {
        [BindProperty]
        public Product Product { get; set; } = new Product();

        public decimal FinalPrice { get; set; }

        public string Message { get; set; } = "";

        public bool IsCalculated { get; set; }

        public void OnGet()
        {
        }

        public void OnPost(double discount)
        {
            decimal discountAmount =
                Product.Price * (decimal)(discount / 100);

            FinalPrice = Product.Price - discountAmount;

            Message =
                $"The product \"{Product.Name}\" costs " +
                $"{FinalPrice:F2} after applying a {discount:F2}% discount.";

            IsCalculated = true;
        }
    }
}