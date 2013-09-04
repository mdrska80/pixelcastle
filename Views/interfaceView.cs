using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace Castles
{
    public class interfaceView : BaseView
    {
        private Board b = new Board();

        //surfaces required for this View
        public Surface sIsGamePaused {get;set;}


        public interfaceView(Point origin) : base(origin)
        {
            sIsGamePaused = Game.I.resourceManager.GetGfx("IsGamePaused.png");
        }

        public override void UpdateView(SFML.Graphics.RenderWindow window)
        {
            base.UpdateView(window);
        }

        public override void UpdateView(Surface surf)
        {
            if (Game.I.isPaused)
            {
                surf.Blit(sIsGamePaused, new Point(100,100));
            }

            List<string> l = new List<string>();
            l.Add(string.Format("Top score: {0}", Game.I.highScore.GetScrn(Game.I.level.level).score));

            if (Game.I.player!=null)
                l.Add(string.Format("Score: {0}", Game.I.player.score.GetScrn(Game.I.level.level).score));
            else
            {
                l.Add(string.Format("Player not defined"));
            }
            
            b.Update(surf, new Point(10, 10), l);
        }
    }
}
