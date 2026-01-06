using System;
using System.Collections.Generic;
using System.IO;
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
            return RunApplication(args);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
        finally
        {
            Console.ResetColor();
        }
    }
    
    private static int RunApplication(string[] args)
    {
        if (args.Length == 0)
        {
            GeneralHelper.ShowHelp();
            return 0;
        }

        args = NormalizeArgs(args);

        var commandName = args[0].ToLower();

        if (!TryGetCommand(commandName, out var command))
        {
            ShowUnknownCommandError(commandName);
            return 1;
        }

        command?.Execute(args);
        return 0;
    }

    private static string[] NormalizeArgs(string[] args)
    {
        if (args.Length > 0 && args[0] == "task-cli")
        {
            return args.Skip(1).ToArray();
        }

        return args;
    }

    private static bool TryGetCommand(string commandName, out ICommand? command)
    {
        return s_commands!.TryGetValue(commandName, out command);
    }

    private static void ShowUnknownCommandError(string commandName)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Помилка: Невідома команда '{commandName}'");
        Console.WriteLine("Використовуйте 'task-cli help' або просто 'help' для перегляду команд.");
        Console.ResetColor();
    }

    private static int HandleException(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        switch (ex)
        {
            case FileLoadException:
                Console.WriteLine($"Помилка завантаження даних: {ex.Message}");
                return 2;

            case DirectoryNotFoundException:
                Console.WriteLine($"Помилка роботи з файлом: {ex.Message}");
                Console.WriteLine("Директорію не знайдено.");
                return 3;

            case IOException:
                Console.WriteLine($"Помилка роботи з файлом: {ex.Message}");
                return 3;

            case ArgumentException:
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Неправильні аргументи: {ex.Message}");
                return 4;

            case InvalidOperationException or NotSupportedException:
                Console.WriteLine($"Помилка операції: {ex.Message}");
                Console.WriteLine("Ця операція зараз недоступна.");
                return 5;

            default:
                Console.WriteLine("Сталася неочікувана помилка:");
                Console.WriteLine($"Тип: {ex.GetType().Name}");
                Console.WriteLine($"Повідомлення: {ex.Message}");
                return 99;
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
            s_commands["list"] = new ListCommand(fileService, helperService);
            s_commands["update"] = new UpdateCommand(fileService, helperService);
            s_commands["delete"] = new DeleteCommand(fileService, helperService);
            s_commands["mark-in-progress"] = new MarkInProgressCommand(fileService, helperService);
            s_commands["mark-done"] = new MarkDoneCommand(fileService, helperService);
            s_commands["help"] = new HelpCommand(s_commands);
            
            // Console.WriteLine($"Завантажено {s_commands.Count} команд");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Критична помилка ініціалізації: {ex.Message}");
            Console.ResetColor();
            
            // Створення мінімального набору команд P.s. щоб можна було викликати help
            s_commands = new Dictionary<string, ICommand>
            {
                ["help"] = new HelpCommand(new Dictionary<string, ICommand>())
            };
            
            throw new ApplicationException("Не вдалося ініціалізувати CLI. Використовуйте 'help' для базової допомоги.", ex);
        }
    }
}