using System;
using System.IO;
using System.Linq;
using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

public class AddCommand(TaskFileService fileService, TaskHelperService helperService) : ICommand
{
    public string Name => "add";
    public string Description => "Додати нову задачу";
    
    private readonly TaskFileService _fileService = fileService;
    private readonly TaskHelperService _helperService = helperService;

    public void Execute(string[] args)
    {
        try
        {
            if (args.Length < 2)
            {
                Console.WriteLine(@"
    Потрібно вказати ціль задачі.
    add ""назва""
            ");
                return;
            }
        
            string description = args[1];
        
            if (args.Length > 2)
            {
                description = string.Join(" ", args.Skip(1));
            }

            var tasks = _fileService.LoadTasks();

            var newTask = new TaskItem
            {
                Id = _helperService.GetNextId(tasks),
                Description = description,
                CreatedAt = DateTime.Now
            };
        
            tasks.Add(newTask);
            _fileService.SaveTasks(tasks);
            Console.WriteLine($"Задачу було успішно додано! Id задачі: {newTask.Id}");
        }
        catch (FormatException ex)
        {
            Console.WriteLine($"Помилка формату даних: {ex.Message}");
            Console.WriteLine("Перевірте коректність даних у файлі завдань.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Помилка при збереженні файлу: {ex.Message}");
            Console.WriteLine("Можливо, файл зайнятий або диск заповнений.");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Помилка доступу: {ex.Message}");
            Console.WriteLine("У вас немає прав на запис у теку з файлами.");
        }
        catch (System.Text.Json.JsonException ex)
        {
            Console.WriteLine($"Помилка формату JSON: {ex.Message}");
            Console.WriteLine("Файл завдань пошкоджений. Можливо, потрібно створити новий файл.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неочікувана помилка при додаванні задачі:");
            Console.WriteLine($"Тип: {ex.GetType().Name}");
            Console.WriteLine($"Повідомлення: {ex.Message}");
        }
    }
}