using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Models;
using TaskFlow.Services;

namespace TaskFlow.Pages;

public class CreateModel : PageModel
{
    private readonly ITaskService _taskService;

    public CreateModel(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [BindProperty]
    public CreateTaskInput Input { get; set; } = new();

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        _taskService.Add(new TaskItem
        {
            Title = Input.Title.Trim(),
            Description = Input.Description?.Trim() ?? string.Empty,
            Priority = Input.Priority,
            IsCompleted = false,
            AttachedFileName = string.IsNullOrWhiteSpace(Input.AttachedFileName)
                ? null
                : Input.AttachedFileName.Trim()
        });

        TempData["SuccessMessage"] = "Your task has been created successfully.";

        return RedirectToPage("/Index");
    }

    public class CreateTaskInput
    {
        [Required(ErrorMessage = "Please enter a task title.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select a priority.")]
        [RegularExpression(
            "^(High|Medium|Low)$",
            ErrorMessage = "Please select a valid priority.")]
        public string Priority { get; set; } = "Medium";

        [StringLength(150, ErrorMessage = "File name cannot exceed 150 characters.")]
        public string? AttachedFileName { get; set; }
    }
}