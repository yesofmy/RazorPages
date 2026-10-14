using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Models;
using ProductCatalog.Services;

namespace ProductCatalog.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;

        public Product? Product { get; set; }

        public DetailsModel(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult OnGet(int id)
        {
            Product = _productService
                .GetProducts()
                .FirstOrDefault(p => p.Id == id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}