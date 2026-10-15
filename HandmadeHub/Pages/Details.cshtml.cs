using HandmadeHub.Data;
using HandmadeHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HandmadeHub.Pages;

public class DetailsModel : PageModel
{
    private readonly ShopContext _context;

    public DetailsModel(ShopContext context)
    {
        _context = context;
    }

    public Product? Product { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        if (id <= 0)
            return BadRequest();

        Product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (Product == null)
            return NotFound();

        ViewData["PageMessage"] = "Product Details";
        return Page();
    }

    public IActionResult OnGetBack()
    {
        return RedirectToPage("/Index");
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (id <= 0)
            return BadRequest();

        var exists = await _context.Products
            .AnyAsync(p => p.Id == id);

        if (!exists)
            return NotFound();

        return RedirectToPage("/Edit", new { id });
    }
}