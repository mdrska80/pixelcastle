using System.Collections.Generic;

using SFML.Graphics;
using SFML.Window;

namespace Castles
{
    public class Board : Drawable
    {
        private Vector2f origin { get; set; }
        private List<string> data { get; set; }

        public Board(Vector2f o, List<string> d)
        {
            origin = o;
            data = d;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Sprite s = new Sprite(Game.I.resourceManager.GetTexture("Interface_Box2.png"));
            s.Position = new Vector2f(origin.X, origin.Y);
            target.Draw(s);

            //int index = 1;
            //foreach (string s in data)
            //{
            //    Common.Text(new Point(origin.X + 10, origin.Y + 20 * index), s, font, surf, Color.FromArgb(253, 222, 125));
            //    //m_FontSurface = font.Render(s, Color.FromArgb(218, 212, 94));
            //    //surf.Blit(m_FontSurface, new Point(origin.X + 10, origin.Y + 20*index));
            //    index++;
            //}
        }
    }
}
