using System;
using System.Collections.Generic;
using System.IO;
using TaskTrackerCLI.Models;
using TaskTrackerCLI.Services;
using TaskTrackerCLI.Utils;

namespace TaskTrackerCLI.Commands;

public class ListCommand(TaskFileService fileService, TaskHelperService helperService) : ICommand
{
    public string Name => "list";
    public string Description => "Показати всі задачі";
    
    private readonly TaskFileService _fileService = fileService;
    private readonly TaskHelperService _helperService = helperService;

    public void Execute(string[] args)
    {
        try
        {
            var tasks = _fileService.LoadTasks();
            
            if (args.Length == 1)
            {
                PrintTasks(tasks);
                return;
            }
           
            var status = StatusHelper.ParseStatus(args[1].ToLower());
            
            var tasksByStatus = _helperService.ReturnTasksByStatus(tasks, status);
            
            PrintTasks(tasksByStatus);
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
            Console.WriteLine("У вас немає прав на читання файлу.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Сталася невідома помилка: {ex.Message}");
        }
    }
    
    private void PrintTasks(List<TaskItem> tasks)
    {
        foreach (var task in tasks)
        {
            Console.WriteLine(_helperService.FormatTaskForDisplay(task));
        }
    }
}