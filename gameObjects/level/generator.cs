using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Castles.Tools;
using System.Collections.Generic;

namespace Castles
{
	public class Generator
	{
		[XmlAttribute]
		public bool isGenerator { get; set; }        

		[XmlAttribute]
		public int Interval {get;set;}

		[XmlAttribute]
		public EntityType EntityType = EntityType.TreeFeeding;
	}
}