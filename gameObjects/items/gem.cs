using SFML.Graphics;
using SFML.Utils;

namespace Castles
{
    public class Gem : Item
    {
        public bool picked = false;
        public int charges { get; set; }

        public override bool TryToPickup()
        {
            charges--;

            if (charges <= 0)
                picked = true;

            return picked;
        }

        /// <summary>
        /// GFX initialization
        /// </summary>
        public override void InitGfx()
        {
            SpriteAnimated sa = new SpriteAnimated(Game.I.resourceManager.GetTexture("GEM"), 32,32,32, Game.I.window, RenderStates.Default );
            Sprite.AddAnimatedSprite("MAIN",  sa);
        }
    }
}
