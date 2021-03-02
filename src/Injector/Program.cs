using Reloaded.Injector;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        WriteColored("Press any key to inject", ConsoleColor.Green);
        Console.ReadKey();

        var processes = Process.GetProcessesByName("valheim");
        if (processes.Any() == false)
        {
            WriteColored("could not find valheim process. Is the game running?", ConsoleColor.Red);
            Console.ReadKey();
            return;
        }

        if (processes.Length > 1)
        {
            WriteColored("multiple valheim processes found. Run the game only one", ConsoleColor.Red);
            Console.ReadKey();
            return;
        }

        var process = processes.Single();
        using (var injector = new Injector(process))
        {
            var moduleFile = new FileInfo("ValheimNetworkFix.dll");

            if (moduleFile.Exists == false)
            {
                WriteColored("could not find ValheimNetworkFix.dll", ConsoleColor.Red);
                Console.ReadKey();
                return;
            }

            var result = injector.Inject(moduleFile.FullName);
            injector.CallFunction(moduleFile.FullName, "Initialize", 0);
        }
    }

    private static void WriteColored(string text, ConsoleColor color)
    {
        var previousColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = previousColor;
    }
}
