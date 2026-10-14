using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProductCatalog.Models;
using ProductCatalog.Services;

namespace ProductCatalog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;

        public List<Product> Products { get; set; } = new();

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult OnGet()
        {
            Products = _productService.GetProducts();

            ViewData["RenderTime"] = DateTime.Now.ToString(
                "dd MMMM yyyy, HH:mm:ss"
            );

            return Page();
        }

        public IActionResult OnGetCheap(decimal maxPrice)
        {
            Products = _productService
                .GetProducts()
                .Where(p => p.Price < maxPrice)
                .ToList();

            ViewData["RenderTime"] = DateTime.Now.ToString(
                "dd MMMM yyyy, HH:mm:ss"
            );

            return Page();
        }

        public IActionResult OnGetDownloadReport()
        {
            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "catalog_info.txt"
            );

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            return PhysicalFile(
                filePath,
                "text/plain",
                "catalog_info.txt"
            );
        }
    }
}