using System;
using System.Drawing;
using System.IO;
using SdlDotNet.Core;
using SdlDotNet.Graphics;

namespace Castles
{
    public class HelloWorld
    {
        [STAThread]
        public static void Main()
        {
            ResourceManager rm = new ResourceManager();
            InputController inputController = new InputController();

			Game.I.Init(rm);
            Game.I.Start();
            inputController.Go();
        }
    }
}