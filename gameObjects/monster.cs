using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Castles.gameObjects;
using SdlDotNet.Graphics.Sprites;

namespace Castles
{
	public class Monster : Entity
	{
		public override void Update()
		{
			// default monster behavior is to chase a player.
            if (Game.I.player!=null)
			    chasingTarget = Game.I.player.position;

			base.Update();

			if (timeToUpdate)
			{
				if (highlightedPlatforms!=null && highlightedPlatforms.Count > 1)
				{
					Platform px = highlightedPlatforms[highlightedPlatforms.Count - 2];

					// px je kterym smerem od mista kde stojim?
					Direction dir = GetDirection(this.position, px);
					Move(dir);
				}
			}
		}

	    public override Platform Move(Direction dir)
	    {
	        Platform p = base.Move(dir);
	        Player pl = Game.I.player;

	        if (p != null)
	        {
                //is there a player on this platform?
	            if (pl.position.X == p.x &&
	                pl.position.Y == p.y &&
	                pl.position.Layer == p.layer)
	            {
                    //ok there is
                    Game.I.player.Die();
	            }
	        }

	        return p;
	    }

        public override void Die()
        {
            base.Die();

            if (lives == 0)
                Game.I.level.Monsters.Remove(this);
        }

        public virtual bool TryToKill(Entity e)
        {
        	// all monsters are by default immortal.
        	return false;
        }



	}
}
