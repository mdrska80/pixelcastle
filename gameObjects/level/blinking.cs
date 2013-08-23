using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Castles.Tools;
using System.Collections.Generic;

namespace Castles
{
	public class Blinking
	{
		[XmlAttribute]
		public bool isBlinking { get; set; }

		[XmlAttribute]
		public int Interval {get;set;}

		[XmlAttribute]
		public bool Visible {get;set;}
	}
}