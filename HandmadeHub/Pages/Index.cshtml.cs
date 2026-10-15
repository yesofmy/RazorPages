using HandmadeHub.Data;
using HandmadeHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HandmadeHub.Pages;

public class IndexModel : PageModel
{
    private readonly ShopContext _context;
    private readonly IWebHostEnvironment _environment;

    public IndexModel(
        ShopContext context,
        IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public IList<Product> Products { get; set; } = new List<Product>();

    public async Task OnGetAsync()
    {
        Products = await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Title)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrWhiteSpace(product.ImagePath))
        {
            var imagePath = Path.Combine(
                _environment.WebRootPath,
                product.ImagePath.TrimStart('/')
                    .Replace('/', Path.DirectorySeparatorChar));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }
}