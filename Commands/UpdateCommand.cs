using System;
using System.IO;
using System.Linq;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

public class UpdateCommand(TaskFileService fileService, TaskHelperService helperService) : ICommand
{
    public string Name => "update";
    public string Description => "Оновлення статусу завдання";

    private readonly TaskFileService _fileService = fileService;
    private readonly TaskHelperService _helperService = helperService;
    
    public void Execute(string[] args)
    {
        try
        {
            if (args.Length < 3)
            {
                Console.WriteLine(@"
    Потрібно прописати ID задачі та новий опис
    Приклад -> ""update 1 'Купити хліб та зробити бутери'""
            ");
                return;
            }

            var id = int.TryParse(args[1], out var taskId)
                ? taskId
                : 0;

            if (taskId == 0)
            {
                Console.WriteLine($"Не можливо зчитати {args[1]} як ID завдання.");
                return;
            }

            var newDescription = string.Join(" ", args.Skip(2));
        
            var tasks = _fileService.LoadTasks();
            var updatedDescription = _helperService.UpdateTaskDescriptionById(tasks, id, newDescription);
            _fileService.SaveTasks(tasks);
        
            Console.WriteLine(updatedDescription);
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Помилка формату даних: {ex.Message}");
            Console.WriteLine("Перевірте коректність даних у файлі завдань.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка вводу/виводу: {ex.Message}");
            Console.WriteLine("Перевірте доступ до файлу завдань.");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Помилка доступу: {ex.Message}");
            Console.WriteLine("У вас немає прав на читання/запис файлу.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася неочікувана помилка: {ex.Message}");
            Console.WriteLine("Деталі помилки:");
            Console.WriteLine(ex.ToString());
        }
    }
}