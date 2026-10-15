using HandmadeHub.Data;
using HandmadeHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HandmadeHub.Pages;

public class CreateModel : PageModel
{
    private readonly ShopContext _context;
    private readonly IWebHostEnvironment _environment;

    public CreateModel(ShopContext context, IWebHostEnvironment environment)
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

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Upload != null && Upload.Length > 0)
        {
            var extension = Path.GetExtension(Upload.FileName).ToLowerInvariant();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("Upload",
                    "Allowed formats: JPG, JPEG, PNG, WEBP.");
            }

            if (Upload.Length > 5 * 1024 * 1024)
            {
                ModelState.AddModelError("Upload",
                    "The maximum image size is 5 MB.");
            }
        }

        if (!ModelState.IsValid)
            return Page();

        if (Upload != null && Upload.Length > 0)
        {
            var uploadsFolder = Path.Combine(
                _environment.WebRootPath, "uploads");

            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid():N}" +
                           Path.GetExtension(Upload.FileName).ToLowerInvariant();

            var filePath = Path.Combine(uploadsFolder, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await Upload.CopyToAsync(stream);

            Product.ImagePath = "/uploads/" + fileName;
        }

        _context.Products.Add(Product);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Details", new { id = Product.Id });
    }
}