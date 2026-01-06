using System;
using System.IO;
using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;

namespace TaskTrackerCLI.Commands;

public class MarkInProgressCommand(TaskFileService fileService, TaskHelperService helperService) : ICommand
{
    public string Name => "mark-in-progress";
    public string Description => "Позначити задачу як 'В процесі'";
    
    private readonly TaskFileService _fileService = fileService;
    private readonly TaskHelperService _helperService = helperService;
    
    public void Execute(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine(@"
    Недостатньо аргументів.
    Потрібно писати в такому форматі: mark-in-progress <ID>
            ");
        }

        try
        {
            var tasks = _fileService.LoadTasks();
            var idToMark = int.TryParse(args[1], out var taskId)
                ? taskId
                : 0;

            if (idToMark == 0)
            {
                Console.WriteLine("Некоректний ID задачі.");
                return;
            }
            
            var resultMessage = _helperService.UpdateTaskStatusById(tasks, idToMark, Status.InProgress);
            _fileService.SaveTasks(tasks);
            
            Console.WriteLine(resultMessage);
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