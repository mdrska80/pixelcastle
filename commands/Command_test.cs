using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles.commands
{
    public class Command_test : ICommand
    {
        public void Process()
        {
            Console.WriteLine("Test command response.");
        }
    }
}
