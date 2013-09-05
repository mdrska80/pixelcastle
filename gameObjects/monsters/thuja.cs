using SFML.Graphics;
using SFML.Utils;

namespace Castles
{
    public class Thuja : Monster
    {
        public Thuja() : base()
        {
            type = EntityType.Thuja;
            speed = (int)MonsterSpeed.SuperFast;

            // specific animation collection for this entity
            SpriteAnimated sa = new SpriteAnimated(Game.I.resourceManager.GetTexture("THUJA"), 32, 32, 32, Game.I.window, RenderStates.Default);
            Sprite.AddAnimatedSprite("MAIN", sa);
            Sprite.CurrentAnimation = "MAIN";
        }
    }
}
