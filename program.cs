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
            ResourceManager rm = new ResourceManager();

			Game.I.Init(rm);
            Game.I.Start();
        }
    }
}