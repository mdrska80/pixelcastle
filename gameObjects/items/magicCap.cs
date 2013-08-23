using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles.gameObjects
{
	public class MagicCap : Item
	{
		public MagicCap()
		{
		}

		public override bool TryToPickup()
		{
			//make player invulnerable
			Game.I.player.MakeImmortal();

			return true;
		}         
	}
}
