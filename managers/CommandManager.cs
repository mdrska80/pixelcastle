using System;
using Castles.commands;

namespace Castles
{
    public class CommandManager
    {
        public static void HandleCommand(string command)
        {
            ICommand comm = RetrieveCommand(command);

            try
            {
                comm.Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing command: {0}, {1}", command, ex);
            }
        }

        private static ICommand RetrieveCommand(string command)
        {
            //here is actually list of all possible commands
            if (command == "test")
            {
                return new Command_test();
            }

            return new Command_NotRecognized(command);
        }
    }
}
