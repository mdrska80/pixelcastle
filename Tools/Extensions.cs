using System;
using System.Globalization;
using System.Collections.Generic;
using SdlDotNet.Graphics;
using System.Drawing;

namespace Castles.Tools
{
	public static class Extensions
	{
		public static int ToInt(this string current)
		{
			int convertedValue;
			int.TryParse(current, out convertedValue);
			return convertedValue;
		}


		public static int ToInt(this object current)
		{
			int convertedValue;
			int.TryParse(current.ToString(), out convertedValue);
			return convertedValue;
		}
		
		public static DateTime ToDate(this object current)
		{
			if (current==null) return DateTime.MinValue;
			
			return DateTime.ParseExact(current.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
		}

		public static DateTime ToTime(this object current)
		{
			if (current==null) return DateTime.MinValue;
			
			return DateTime.ParseExact(current.ToString(), "HH:mm", CultureInfo.InvariantCulture);
		}

		public static bool ToBool(this object current)
		{
			bool convertedValue;
			bool.TryParse(current.ToString(), out convertedValue);
			return convertedValue;
		}

        public static Surface SetBrightness(this Surface current, int brightness)
	    {
	        Surface s = current;

	        for (int x = 0; x <= s.Width-1; x++)
	        {
	            for (int y = 0; y <= s.Height-1; y++)
	            {
	                Point p = new Point(x, y);
                    s.Lock();
	                Color c = s.GetPixel(p);

                    int R = c.R + brightness; 
                    int G = c.G + brightness; 
                    int B = c.B + brightness;

                    if (R > 255) R = 255;
                    if (G > 255) G = 255;
                    if (B > 255) B = 255;

	                Color cNew = Color.FromArgb(R, G, B);

	                Color[,] cc = {{cNew}};

	                s.SetPixels(p, cc);
                    s.Unlock();
	            }
	        }

	        return s;
	    }

//		public static List<string> ParsePath(this string path)
//		{
//			return PathParser.Parse(path);
//		}

	}
}

