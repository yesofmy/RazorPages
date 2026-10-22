using TaskFlow.Models;

namespace TaskFlow.Services;

public interface ITaskService
{
    IReadOnlyList<TaskItem> GetAll();

    TaskItem? GetById(int id);

    IReadOnlyList<TaskItem> GetByPriority(string priority);

    TaskItem Add(TaskItem item);

    bool ToggleStatus(int id);
}