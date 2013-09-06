using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace SFML.Utils.TileEngine
{
    public class MainTileProviders
    {
        public static void TileProvider(int x, int y, int layer, out Color color, out IntRect rec)
        {
            if (x==1 && y == 1)
                color = Color.Red;
                
            else
            {
                color = new Color((byte)(x * 10), (byte)(y * 10), (byte)(255 - (byte)(y * 10)));
            }

            rec = new IntRect(0,0,1,1);
        }
    }
}
