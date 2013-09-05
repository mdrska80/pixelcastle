using System.Collections.Generic;
using SFML.Graphics;
using SFML.Utils;
using SFML.Window;

namespace Castles
{
    public class interfaceView : Drawable
    {
        private SpriteAnimatedEx Sprite { get; set; }
        private Text txtFPS { get; set; }

        public void init()
        {
            SpriteAnimated saCursor = new SpriteAnimated(Game.I.resourceManager.GetTexture("CURSOR"), 96, 96, 10 , Game.I.window, RenderStates.Default,0,14,true);
            Sprite = new SpriteAnimatedEx();
            Sprite.AddAnimatedSprite("MAIN", saCursor);
            Sprite.CurrentAnimation = "MAIN";

            txtFPS = new Text();
            txtFPS.Font = new Font("data/sansation.ttf");
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Vector2i v2i = Mouse.GetPosition((RenderWindow)target);

            if (Sprite==null)
                init();

            Sprite.CurrentSprite.Position = new Vector2f(v2i.X, v2i.Y);
            txtFPS.DisplayedString = string.Format("FPS:  "+FPS.Value.ToString());
            txtFPS.Position = new Vector2f(100,100);
            txtFPS.Color = Color.Black;

            target.Draw(txtFPS);

            Sprite.CurrentSprite.Update(FPS.SemiDelta);

        }
    }
}
