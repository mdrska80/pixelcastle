using System.Collections.Generic;
using System.Drawing;

using SFML.Graphics;

namespace Castles
{
    public class debugView : Drawable
    {
        public void Draw(RenderTarget target, RenderStates states)
        {
            Game.I.player.Sprite.CurrentSprite.Update(FPS.SemiDelta);
        }
    }
}
