using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles.gameObjects
{
    public class HoneyCauldron : Item
    {
        public bool Filled { get; set; }

        public HoneyCauldron()
        {
            Filled = true;
        }

        public HoneyCauldron(IGPos pos)
        {
            position = pos;
        }
    }
}
