using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentAccounting.Models;

namespace StudentAccounting.Pages;

public class IndexModel : PageModel
{
    private readonly ApplicationContext _context;

    public IndexModel(ApplicationContext context)
    {
        _context = context;
    }

    public IList<Student> Students { get; set; } = new List<Student>();

    public async Task OnGetAsync()
    {
        Students = await _context.Students
            .AsNoTracking()
            .OrderBy(s => s.Id)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        return RedirectToPage();
    }
}