using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Castles.Tools;

namespace Castles
{
	public class Action_AddLife : BaseAction
	{
		public override void Execute(Platform p, Entity e)
		{
			if (IsActive)
			{
				e.lives++;

				if (IsOneTimeAction)
				{
					IsActive = false;
				}
			}
		}
	}
}
