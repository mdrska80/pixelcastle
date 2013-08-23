using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Castles.Tools;
using System.Collections.Generic;

namespace Castles
{
	public class Dimension
	{
		[XmlAttribute]
		public int X {get;set;}

		[XmlAttribute]
		public int Y {get;set;}
	}
}