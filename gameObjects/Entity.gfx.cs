using SFML.Graphics;
using SFML.Utils;

namespace Castles
{
    public partial class Entity
    {
        public SpriteAnimatedEx Sprite { get; set; }

        public virtual void InitGfx()
        {
            Sprite = new SpriteAnimatedEx();

        }
    }
}
