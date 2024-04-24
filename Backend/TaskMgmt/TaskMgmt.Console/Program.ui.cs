// See https://aka.ms/new-console-template for more information

using System;
using System.IO;
using TaskMgmt.Console;

class Program2
{
    static List<string> ? ConsoleReadInput()
    {
        string ? input = Console.ReadLine();
        if (input == null || input.Length == 0 || input == " ") 
            return null;

        //Accounting for quotes
        List<string> output = new List<string>();

        string[] args = input.Split('\'', '"').ToArray();
        foreach(var arg in args)
        {
            if (arg == "" || arg == " ") continue;

            arg.Split(' ').ToList().ForEach( e => {
                if (e != "")
                    output.Add(e);
            });
        }
        return output;
    }

    const string COMMAND_OPTIONS = 
    @$"
        COMMANDS
        --------
            /create <projectName> <projectDescription> <groupId>        := create a project
        
    ";
    static void Main2()
    {
        ConsoleProject cproj = new ConsoleProject();

        bool isRunning = true;
        while (isRunning)
        {
            Console.Write("> ");
            List<string> args = ConsoleReadInput();
            if (args == null) continue;

            switch(args[0].ToLower())
            {
                case "/create":
                    if (args.Count() != 4)
                    {
                        Console.WriteLine("Insufficient arguments!, expected 4");
                        continue;
                    }

                    string projName = args[1];
                    string projDes = args[2];
                    if (!int.TryParse(args[3], out int groupId))
                    {
                        Console.WriteLine("groupId should be a number, passed 3rd argument is a string");
                        continue;
                    }

                    cproj.CreateNewProject(projName, projDes, groupId);
                break;
                case "/help":
                    System.Console.WriteLine(COMMAND_OPTIONS);
                break;
                case "/exit":
                    isRunning = false;
                break;
                default:
                    Console.WriteLine("Invalid command!");
                break;
            }
        }
    }
}
