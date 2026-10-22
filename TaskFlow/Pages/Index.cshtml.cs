using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Models;
using TaskFlow.Services;

namespace TaskFlow.Pages;

public class IndexModel : PageModel
{
    private readonly ITaskService _taskService;

    public IndexModel(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public IReadOnlyList<TaskItem> Items { get; private set; }
        = Array.Empty<TaskItem>();

    public string? SelectedPriority { get; private set; }

    public int TotalCount => Items.Count;

    public int CompletedCount => Items.Count(x => x.IsCompleted);

    public int PendingCount => Items.Count(x => !x.IsCompleted);

    public void OnGet(string? priority)
    {
        ViewData["WelcomeMessage"] = "Here is what's happening with your tasks today.";
        SelectedPriority = priority;

        Items = string.IsNullOrWhiteSpace(priority)
            ? _taskService.GetAll()
            : _taskService.GetByPriority(priority);
    }

    public IActionResult OnPostToggle(int id)
    {
        if (!_taskService.ToggleStatus(id))
        {
            return NotFound();
        }

        return RedirectToPage("/Index");
    }
}