using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SdlDotNet.Core;
using SdlDotNet.Graphics;
using SdlDotNet.Input;

namespace Castles
{
    public class debugView : BaseView
    {
        private Board b = new Board();

        public debugView(Point origin) : base(origin)
        {
        }

        public override void UpdateView(Surface surf)
        {
            //calculate data
            string fps = Events.Fps.ToString();

            List<string> l = new List<string>();
            l.Add(string.Format("FPS: {0}", fps));
            l.Add(string.Format("Surfaces: {0}, {1}", Game.I.surfaces, Game.I.Screen));

            b.Update(surf, new Point(10, 150),l);
        }
    }
}
