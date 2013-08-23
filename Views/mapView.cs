using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace Castles
{
    public class mapView : BaseView
    {
        private Board b = new Board();

        public Surface sPoint_done {get;set;}
        public Surface sPoint_current {get;set;}
        public Surface sPoint_locked {get;set;}

        // map: something like this:
        // http://mangust-art.deviantart.com/art/Interface-Global-map-266839499


        public mapView(Point origin) : base(origin)
        {
            //sIsGamePaused = resourceManager.GetGfx("IsGamePaused.png");
        }

        public override void UpdateView(Surface surf)
        {
            if (Game.I.isPaused)
            {
             //   surf.Blit(sIsGamePaused, new Point(100,100));
            }

            List<string> l = new List<string>();
            l.Add(string.Format("Top score: {0}", Game.I.highScore.GetScrn(Game.I.level.level).score));
            l.Add(string.Format("Score: {0}", Game.I.player.score.GetScrn(Game.I.level.level).score));
            
            b.Update(surf, new Point(10, 10), l);
        }
    }
}
