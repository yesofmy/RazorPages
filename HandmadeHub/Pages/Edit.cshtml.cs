using HandmadeHub.Data;
using HandmadeHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HandmadeHub.Pages;

public class EditModel : PageModel
{
    private readonly ShopContext _context;
    private readonly IWebHostEnvironment _environment;

    public EditModel(ShopContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [BindProperty]
    public Product Product { get; set; } = new();

    [BindProperty]
    public IFormFile? Upload { get; set; }

    public List<SelectListItem> Categories { get; } = new()
    {
        new("Embroidery", "Embroidery"),
        new("Knitting", "Knitting"),
        new("Decor", "Decor"),
        new("Beadwork", "Beadwork"),
        new("Tools", "Tools"),
        new("Materials", "Materials")
    };

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return NotFound();

        Product = product;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (id != Product.Id)
            return BadRequest();

        var existing = await _context.Products.FindAsync(id);

        if (existing == null)
            return NotFound();

        if (Upload != null && Upload.Length > 0)
        {
            var extension = Path.GetExtension(Upload.FileName).ToLowerInvariant();
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowed.Contains(extension))
                ModelState.AddModelError("Upload", "Invalid file format.");

            if (Upload.Length > 5 * 1024 * 1024)
                ModelState.AddModelError("Upload", "Maximum file size is 5 MB.");
        }

        if (!ModelState.IsValid)
            return Page();

        existing.Title = Product.Title;
        existing.Category = Product.Category;
        existing.Price = Product.Price;

        if (Upload != null && Upload.Length > 0)
        {
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath, "uploads");

            Directory.CreateDirectory(uploadsFolder);

            var extension = Path.GetExtension(Upload.FileName).ToLowerInvariant();
            var fileName = $"{Guid.NewGuid():N}{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            await using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Upload.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(existing.ImagePath))
            {
                var oldName = Path.GetFileName(existing.ImagePath);
                var oldPath = Path.Combine(uploadsFolder, oldName);

                if (System.IO.File.Exists(oldPath))
                    System.IO.File.Delete(oldPath);
            }

            existing.ImagePath = "/uploads/" + fileName;
        }

        await _context.SaveChangesAsync();

        return RedirectToPage("/Details", new { id = existing.Id });
    }
}