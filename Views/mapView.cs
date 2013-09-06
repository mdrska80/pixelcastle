using SFML.Graphics;
using SFML.Utils.TileEngine;
using SFML.Window;

namespace Castles
{
    public class mapView : Drawable
    {
        public MapRenderer renderer = new MapRenderer(null, MainTileProviders.TileProvider, 64);
        public int X = 10;
        public int Y = 10;


        public void Draw(RenderTarget target, RenderStates states)
        {
            View v = new View(new Vector2f(X, Y), new Vector2f(1024,768));
            target.SetView(v);
            //renderer.Refresh(1,1);
            target.Draw(renderer);

            target.SetView(Game.I.window.DefaultView);
        }
    }
}
