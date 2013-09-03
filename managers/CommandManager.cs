using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castles.commands;

namespace Castles.managers
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

        public static ICommand RetrieveCommand(string command)
        {
            if (command == "test")
            {
                return new Command_test();
            }
        }
    }
}
