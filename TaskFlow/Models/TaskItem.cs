using System.ComponentModel.DataAnnotations;

namespace TaskFlow.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Task title is required.")]
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please select a priority.")]
    [RegularExpression(
        "^(High|Medium|Low)$",
        ErrorMessage = "Please select a valid priority.")]
    public string Priority { get; set; } = "Medium";

    public bool IsCompleted { get; set; }

    [StringLength(150)]
    public string? AttachedFileName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}