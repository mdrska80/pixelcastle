using System;
using System.Drawing;
using System.IO;
using System.Xml.Serialization;
using System.Linq;
using Castles.Tools;
using System.Collections.Generic;

namespace Castles
{
	public class Elevator : IToogable
	{
		/// <summary>
		/// Lowest layer where the elevator is going
		/// </summary>
		[XmlAttribute]
		public int Low {get;set;}

		/// <summary>
		/// Highest layer where the elevator is going
		/// </summary>
		[XmlAttribute]
		public int High {get;set;}

		/// <summary>
		/// Where the real platform is.
		/// </summary>
		[XmlAttribute]
		public int Current {get;set;}

		[XmlAttribute]
		public Direction Direction {get;set;}

        /// <summary>
        /// Je vytah funkcni?
        /// </summary>
        [XmlAttribute]
        public bool IsStopped { get; set; }

        public void Toogle()
        {
            // if elevator was running then it will be stopped and vice versa
            IsStopped = !IsStopped;
        }
    }
}