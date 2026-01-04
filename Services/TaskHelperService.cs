using System.Collections.Generic;
using System.Linq;
using TaskTrackerCLI.Models;

namespace TaskTrackerCLI.Services;

public class TaskHelperService
{
    /// <summary>
    /// Генерує наступний доступний ID
    /// </summary>
    public int GetNextId(List<TaskItem> tasks)
    {
        return tasks.Count > 0 
            ? tasks.Max(t => t.Id) + 1 
            : 1;
    }
        
    /// <summary>
    /// Знаходить задачу за ID
    /// </summary>
    public TaskItem? FindTaskById(List<TaskItem> tasks, int id)
    {
        return tasks.FirstOrDefault(t => t.Id == id);
    }
        
    /// <summary>
    /// Перевіряє чи існує задача з таким ID
    /// </summary>
    public bool TaskExists(List<TaskItem> tasks, int id)
    {
        return tasks.Any(t => t.Id == id);
    }
    
    /// <summary>
    /// Фільтрує задачі за статусом
    /// </summary>
    public List<TaskItem> FilterByStatus(List<TaskItem> tasks, Status status)
    {
        return tasks.Where(t => t.Status == status).ToList();
    }
    
    /// <summary>
    /// Видаляє задачу за ID
    /// </summary>
    public bool DeleteTaskById(List<TaskItem> tasks, int id)
    {
        var task = FindTaskById(tasks, id);
        if (task != null)
        {
            return tasks.Remove(task);
        }
        return false;
    }
}