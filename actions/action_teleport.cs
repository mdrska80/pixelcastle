using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Castles.Tools;

namespace Castles
{
	public class Action_Teleport : BaseAction
	{
		public override void Execute(Tile p, Entity e)
		{
			if (IsActive)
			{
				// target is in parameters
				// Param1 = x
				// Param2 = y
				// Param3 = layer

				e.position.X = Param1.ToInt();
				e.position.Y = Param2.ToInt();
				e.position.Layer = Param3.ToInt();

				if (IsOneTimeAction)
				{
					IsActive = false;
				}
			}
		}
	}
}
