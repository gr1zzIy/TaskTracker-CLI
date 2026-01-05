using System;
using System.Linq;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

public class DeleteCommand(TaskFileService fileService, TaskHelperService helperService) : ICommand
{
    public string Name => "delete";
    public string Description => "Видалити задачу";

    private readonly TaskHelperService _helperService = helperService;
    private readonly TaskFileService _fileService = fileService;
    
    public void Execute(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine(@"
    Потрібно прописати ID задачі на видалення
    Приклад -> ""delete 1""
            ");
            return;
        }

        try
        {
            if (args[0].ToLower() == "delete")
            {
                args = args.Skip(1).ToArray();
            }
            
            var tasks = _fileService.LoadTasks();
            var idsToDelete = _helperService.ParseIds(args);

            if (idsToDelete.Count == 0)
            {
                Console.WriteLine($"Не вдалося знайти жодного числового ID. З поміж: {string.Join(", ", idsToDelete)}");
                return;
            }

            int deletedCount = _helperService.MultiDeleteTaskById(tasks, idsToDelete);

            if (deletedCount > 0)
            {
                Console.WriteLine($"Було видалено {deletedCount} задач. \nА саме під такими ID: {string.Join(", ", idsToDelete)}.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Помилка: ID повинні бути числами");
        }
        catch (Exception ex) when (ex is ArgumentException or InvalidOperationException)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }
    }
}