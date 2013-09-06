using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace SFML.Utils.TileEngine
{
    public class MainTileProviders
    {
        public static void ColorTileProvider(int x, int y, int layer, out Color color, out IntRect rec)
        {
            if (x==1 && y == 1)
                color = Color.Red;
                
            else
            {
                color = new Color((byte)(x * 15), (byte)(y * 15), (byte)(255 - (byte)(y * 15)));
            }

            //texture coordinates....
            rec = new IntRect(0,0,32,32);
        }

        public static void TestSpriteProvider(int x, int y, int layer, out SpriteAnimated sprite)
        {
            sprite = null;
        }
    }
}
