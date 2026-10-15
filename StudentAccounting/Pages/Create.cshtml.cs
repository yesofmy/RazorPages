using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentAccounting.Models;

namespace StudentAccounting.Pages;

public class CreateModel : PageModel
{
    private readonly ApplicationContext _context;

    public CreateModel(ApplicationContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Student Student { get; set; } = new();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _context.Students.Add(Student);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }
}