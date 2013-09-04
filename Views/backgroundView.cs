using SFML.Graphics;
using SFML.Window;

namespace Castles
{
    public class backgroundView : BaseView
    {
        public backgroundView(Vector2f origin) : base(origin)
        {
        }

        public override void UpdateView(RenderWindow window)
        {
            window.Clear(new Color(30, 32, 37));
        }
    }
}
