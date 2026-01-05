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
    /// Видаляє задачу за ID
    /// </summary>
    public bool DeleteTaskById(List<TaskItem> tasks, int id)
    {
        var task = FindTaskById(tasks, id);
        
        return task != null && tasks.Remove(task);
    }

    /// <summary>
    /// Видаляємо всі задачі з якими передали id
    /// </summary>
    public int MultiDeleteTaskById(List<TaskItem> tasks, List<int> ids)
    {
        return ids.Count(id => DeleteTaskById(tasks, id));
    }

    /// <summary>
    /// Повертає список id, які змогло розпізнати як числа
    /// </summary>
    public List<int> ParseIds(string[] rawData)
    {
        var ids = new List<int>();

        foreach (var idRaw in rawData)
        {
            if (int.TryParse(idRaw, out var id))
            {
                ids.Add(id);
            }
        }
        
        return ids;
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
    /// Змінюємо опис задачі
    /// </summary>
    public string UpdateTaskDescriptionById(List<TaskItem> tasks, int id, string description)
    {
        var task = FindTaskById(tasks, id);
        var oldDescription = task?.Description;
        
        return $"{oldDescription} було змінено на => {task?.Description = description}";
    }
}