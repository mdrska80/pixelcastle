using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace Castles.Views
{
    public class gameoverView : BaseView
    {
        private Board b = new Board();

        public Surface sPoint_done {get;set;}
        public Surface sPoint_current {get;set;}
        public Surface sPoint_locked {get;set;}

        // map: something like this:
        // http://mangust-art.deviantart.com/art/Interface-Global-map-266839499


        public gameoverView(Point origin) : base(origin)
        {
            //sIsGamePaused = resourceManager.GetGfx("IsGamePaused.png");
        }

        public override void UpdateView(Surface surf)
        {
            List<string> l = new List<string>();
            l.Add("Game over");
            
            b.Update(surf, new Point(10, 10), l);
        }
    }
}
