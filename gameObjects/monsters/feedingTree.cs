using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics.Sprites;

namespace Castles
{
    public class FeedingTree : Monster
    {
        public bool isFeeding
		{
			get
			{
				return (DateTime.Now-dtFeedingStart).TotalSeconds <= 5;
			}
		}

		public DateTime dtFeedingStart {get;set;}

        public FeedingTree() : base()
        {
            type = EntityType.TreeFeeding;
            speed = (int)MonsterSpeed.Fast;

            // specific animation collection for this entity
            aColl.Add(Game.I.resourceManager.GetSprite("TreeFeeding"), 200);

            // add animation to sprite.
            sprite.Animations.Add("walk", aColl);
            sprite.Animations.Add("feed", aColl);

            sprite.CurrentAnimation = "walk";
            sprite.Animate = true;                
        }

		public override bool Pickup (Platform p)
		{
			bool lastgem = base.Pickup (p);

			//put to vulnerable mode...
			dtFeedingStart = DateTime.Now;
			sprite.CurrentAnimation = "feed";//feed

		    return lastgem;
		}
		
		public override void Update()
		{
            base.Update();

			if ((!isFeeding)&&(sprite.CurrentAnimation=="feed"))
				sprite.CurrentAnimation = "walk";
		}

		public override bool TryToKill(Entity e)
		{
			if (isFeeding)
			{
				Die();
				return true;
			}
			else
				return false;
		}

    }
}
