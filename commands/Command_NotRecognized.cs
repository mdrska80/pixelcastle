﻿using System;

namespace Castles.commands
{
    public class Command_NotRecognized : ICommand
    {
        private string cmd { get; set; }

        public Command_NotRecognized(string command)
        {
            cmd = command;
        }

        public void Process()
        {
            Console.WriteLine("Command \"{0}\" was not recognized.", cmd);
        }
    }
}
