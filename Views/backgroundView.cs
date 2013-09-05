using SFML.Graphics;
using SFML.Window;

namespace Castles
{
    public class backgroundView : Drawable
    {
        private Vector2i origin { get; set; }

        public backgroundView(Vector2i origin)
        {
            this.origin = origin;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Clear(new Color(30, 32, 37));
        }
    }
}
