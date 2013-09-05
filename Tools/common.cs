using System.Drawing;
using System.Xml.Serialization;

namespace Castles
{
    public class Common
    {
        public static IGPos GetDesiredPosition(Direction dir, IGPos position)
        {
            switch(dir)
            {
            case Direction.UP:
                {
                    return new IGPos(position.X+1, position.Y-1, position.Layer);
                }
            case Direction.DOWN:
                {
                    return new IGPos(position.X-1, position.Y + 1, position.Layer);
                }
            case Direction.LEFT:
                {
                    return new IGPos(position.X - 1, position.Y, position.Layer);
                }
            case Direction.RIGHT:
                {
                    return new IGPos(position.X + 1, position.Y, position.Layer);
                }
            }

            return new IGPos(-1,-1,-1);

        }
        
        public static int CalculateLastGemBonus(int level)
        {
            return 1000 + ((level*4) - 1);
            //return 1000 + ((((level * 4) - 1) - (4 - board)) * 100);
        }

        public const int halfwidth = 24;
        public const int halfheight = 12;
        public const int stepheight = 7;

        public static Point GetPoint(int x, int y, int layer, Point origin)
        {
            Point p = new Point(origin.X + x * halfwidth,
                            origin.Y + x * halfheight + y * halfwidth - layer * (stepheight));

            return p;
        }

        public static Point GetPlatformXYFromMouseXY(int editingLayer, ref Point m_CursorPosition)
        {
            Point coord = new Point();
            int X = m_CursorPosition.X;
            int Y = m_CursorPosition.Y;

            coord.X = (X - Game.I.boardOrigin.X) / halfwidth;
            coord.Y = (Y - Game.I.boardOrigin.Y - coord.X * halfheight + editingLayer * stepheight) / halfwidth;

            Game.I.editingPlatform.X = coord.X;
            Game.I.editingPlatform.Y = coord.Y;

            return coord;
        }
    }

    public class IGPos
    {
        [XmlAttribute]
        public int X {get;set;}
        
        [XmlAttribute]
        public int Y {get;set;}
        
        [XmlAttribute]
        public int Layer {get;set;}

        public IGPos()
        {
        }

        public IGPos(int x, int y, int layer)
        {
            X = x;
            Y = y;
            Layer = layer;
        }

        public override string ToString()
        {
            return string.Format("x:{0}, y:{1}, layer:{2}",X,Y,Layer);
        }
    }
}
