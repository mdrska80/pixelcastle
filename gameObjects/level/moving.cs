using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Castles.Tools;
using System.Collections.Generic;

namespace Castles
{
	public class Moving
	{
		public Direction Direction {get;set;}
		public IGPos From {get;set;}
		public IGPos To {get;set;}
		public IGPos Current { get; set; }
	}
}