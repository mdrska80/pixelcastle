using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles.gameObjects.items
{
    public class lever : WallItem
    {
        public bool LeverUp { get; set; }

        public override void Toogle()
        {
            LeverUp = !LeverUp;
        }
    }


}
