using System;
using System.Collections.Generic;
using System.Linq;
using TaskTrackerCLI.Commands;
using TaskTrackerCLI.Services;
using TaskTrackerCLI.Utils;

namespace TaskTrackerCLI;

class Program
{
    private static readonly string s_fileName = "tasks.json";
    private static Dictionary<string, ICommand>? s_commands;
    
    static int Main(string[] args)
    {
        try
        {
            InitializeCommands();
            
            if (args.Length == 0)
            {
                GeneralHelper.ShowHelp();
                return 0;
            }

            if (args.Length > 0 && args[0] == "task-cli")
            {
                args = args.Skip(1).ToArray();
            }
            
            string commandName = args[0].ToLower();

            if (!s_commands.TryGetValue(commandName, out var command))
            {
                Console.WriteLine($"Невідома команда: {commandName}");
                Console.WriteLine("Використовуйте 'task-cli help' для перегляду команд.");
                return 1;
            }

            command.Execute(args);
            return 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 1;
        }
    }
    
    private static void InitializeCommands()
    {
        try
        {
            var fileService = new TaskFileService(s_fileName);
            var helperService = new TaskHelperService();

            s_commands = new Dictionary<string, ICommand>();

            s_commands["add"] = new AddCommand(fileService, helperService);
            // ["list"] = new ListCommand();
            // ["update"] = new UpdateCommand();
            s_commands["delete"] = new DeleteCommand(fileService, helperService);
            // ["mark-in-progress"] = new MarkInProgressCommand();
            // ["mark-done"] = new MarkDoneCommand();
            s_commands["help"] = new HelpCommand(s_commands);
            
            // Console.WriteLine($"Завантажено {s_commands.Count} команд");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка ініціалізації команд: {ex.Message}");
            s_commands = new Dictionary<string, ICommand>
            {
                ["help"] = new HelpCommand(new Dictionary<string, ICommand>())
            };
        }
    }
}