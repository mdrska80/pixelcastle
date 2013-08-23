using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Castles.Tools;
using System.Collections.Generic;

namespace Castles
{
	public class EntityDef
	{
		[XmlAttribute]
		public int X { get; set; }

		[XmlAttribute]
		public int Y { get; set; }


        [XmlAttribute]
        public int Layer { get; set; }

		[XmlAttribute]
		public EntityType ET { get; set; }

		[XmlAttribute]
		public int ShiftX {get;set;}

		[XmlAttribute]
		public int ShiftY {get;set;}

		[XmlAttribute]
		public string Class {get;set;}

		public string Description {get;set;}

		public static EntityDef CreatePlayer(int x, int y, int layer)
		{
			EntityDef ed = new EntityDef();
			ed.X = x;
			ed.Y = y;
			ed.Layer = layer;
			ed.ET = EntityType.Player;
			ed.Class = "Castles.Player";

			return ed;
		}
	}
}