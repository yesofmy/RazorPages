using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskFlow.Models;
using TaskFlow.Services;

namespace TaskFlow.Pages;

public class DetailsModel : PageModel
{
    private readonly ITaskService _taskService;

    public DetailsModel(ITaskService taskService)
    {
        _taskService = taskService;
    }

    public TaskItem? Item { get; private set; }

    public IActionResult OnGet(int id)
    {
        Item = _taskService.GetById(id);

        if (Item is null)
        {
            return NotFound();
        }

        return Page();
    }

    public IActionResult OnGetDownload(int id)
    {
        var item = _taskService.GetById(id);

        if (item is null)
        {
            return NotFound();
        }

        var report = new StringBuilder()
            .AppendLine("TASKFLOW - TASK REPORT")
            .AppendLine("======================")
            .AppendLine($"ID: {item.Id}")
            .AppendLine($"Title: {item.Title}")
            .AppendLine($"Description: {item.Description}")
            .AppendLine($"Priority: {item.Priority}")
            .AppendLine($"Status: {(item.IsCompleted ? "Completed" : "In progress")}")
            .AppendLine($"Attached file: {item.AttachedFileName ?? "None"}")
            .AppendLine($"Created at: {item.CreatedAt:yyyy-MM-dd HH:mm:ss}")
            .AppendLine($"Report generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
            .ToString();

        var bytes = Encoding.UTF8.GetPreamble()
            .Concat(Encoding.UTF8.GetBytes(report))
            .ToArray();

        return File(
            bytes,
            "text/plain; charset=utf-8",
            $"task-{item.Id}-report.txt");
    }
}