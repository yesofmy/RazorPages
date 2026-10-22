using TaskFlow.Models;

namespace TaskFlow.Services;

public class InMemoryTaskService : ITaskService
{
    private readonly List<TaskItem> _items = new();
    private readonly object _lock = new();
    private int _nextId = 1;

    public InMemoryTaskService()
    {
        Add(new TaskItem
        {
            Title = "Prepare project report",
            Description = "Collect the required materials and prepare the final project report.",
            Priority = "High",
            IsCompleted = false,
            AttachedFileName = "project-report.txt"
        });

        Add(new TaskItem
        {
            Title = "Review project documentation",
            Description = "Review the existing documentation and update outdated information.",
            Priority = "Medium",
            IsCompleted = true
        });

        Add(new TaskItem
        {
            Title = "Update contact directory",
            Description = "Verify the contact details and update the internal directory.",
            Priority = "Low",
            IsCompleted = false
        });
    }

    public IReadOnlyList<TaskItem> GetAll()
    {
        lock (_lock)
        {
            return _items.Select(Copy).ToList();
        }
    }

    public TaskItem? GetById(int id)
    {
        lock (_lock)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            return item is null ? null : Copy(item);
        }
    }

    public IReadOnlyList<TaskItem> GetByPriority(string priority)
    {
        lock (_lock)
        {
            return _items
                .Where(x => string.Equals(
                    x.Priority,
                    priority,
                    StringComparison.OrdinalIgnoreCase))
                .Select(Copy)
                .ToList();
        }
    }

    public TaskItem Add(TaskItem item)
    {
        lock (_lock)
        {
            var saved = Copy(item);
            saved.Id = _nextId++;
            saved.CreatedAt = DateTime.Now;

            _items.Add(saved);

            return Copy(saved);
        }
    }

    public bool ToggleStatus(int id)
    {
        lock (_lock)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);

            if (item is null)
            {
                return false;
            }

            item.IsCompleted = !item.IsCompleted;
            return true;
        }
    }

    private static TaskItem Copy(TaskItem item)
    {
        return new TaskItem
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            Priority = item.Priority,
            IsCompleted = item.IsCompleted,
            AttachedFileName = item.AttachedFileName,
            CreatedAt = item.CreatedAt
        };
    }
}