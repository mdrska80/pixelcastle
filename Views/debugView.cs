using System.Collections.Generic;
using System.Drawing;

using SFML.Graphics;

namespace Castles
{
    public class debugView : Drawable
    {
        public void Draw(RenderTarget target, RenderStates states)
        {
            //fps, etc
            //throw new System.NotImplementedException();
            Game.I.player.Sprite.CurrentSprite.Update((int)Game.I.delta/1000);
        }
    }
}
