using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentAccounting.Models;

namespace StudentAccounting.Pages;

public class EditModel : PageModel
{
    private readonly ApplicationContext _context;

    public EditModel(ApplicationContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Student Student { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        Student = student;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        if (id != Student.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        var student = await _context.Students.FindAsync(id);

        if (student == null)
        {
            return NotFound();
        }

        student.FullName = Student.FullName;
        student.Group = Student.Group;
        student.AverageGrade = Student.AverageGrade;

        await _context.SaveChangesAsync();

        return RedirectToPage("/Index");
    }
}