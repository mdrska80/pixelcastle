using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Castles.Tools;

namespace Castles
{
	public partial class Action
	{
		public void Execute_addLife(Platform p, Entity e)
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
