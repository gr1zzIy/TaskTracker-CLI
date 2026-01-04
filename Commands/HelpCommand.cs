using System;
using System.Collections.Generic;
using TaskTrackerCLI.Utils;

namespace TaskTrackerCLI.Commands;

public class HelpCommand(Dictionary<string, ICommand> commands) : ICommand
{
    public string Name => "help";
    public string Description => "Показати довідку";
        
    public void Execute(string[] args)
    {
        GeneralHelper.ShowHelp();
            
        Console.WriteLine("\nДоступні команди:");
        foreach (var cmd in commands.Values)
        {
            if (cmd.Name != "help")
            {
                Console.WriteLine($"  {cmd.Name,-20} {cmd.Description}");
            }
        }
    }
}