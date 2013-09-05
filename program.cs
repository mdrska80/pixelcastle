using System;
using System.Drawing;
using System.IO;

namespace Castles
{
    public class HelloWorld
    {
        [STAThread]
        public static void Main()
        {
            Game.I.Start();
            //Game.I.Init();
        }
    }
}