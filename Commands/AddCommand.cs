using System;
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
}