using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace Castles
{
    public class Board
    {
        public Surface sBox { get; set; }
        private Surface m_FontSurface;

        private SdlDotNet.Graphics.Font font;


        public Board()
        {
            sBox = Game.I.resourceManager.GetGfx("Interface_Box2.png");
            font = new SdlDotNet.Graphics.Font(@"Arial.ttf", 16);
        }

        public void Update(Surface surf, Point origin, List<string> data)
        {
            Game.I.surfaces++;
            surf.Blit(sBox, origin);

            int index = 1;
            foreach (string s in data)
            {
                Common.Text(new Point(origin.X + 10, origin.Y + 20 * index), s, font, surf, Color.FromArgb(253, 222, 125));
                //m_FontSurface = font.Render(s, Color.FromArgb(218, 212, 94));
                //surf.Blit(m_FontSurface, new Point(origin.X + 10, origin.Y + 20*index));
                index++;
            }

        }


    }
}
