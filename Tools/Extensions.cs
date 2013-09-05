using System;
using System.Globalization;
using System.Collections.Generic;
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

	}
}

