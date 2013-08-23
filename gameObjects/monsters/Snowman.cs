using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles
{
    public class Snowman : Monster
    {
        public bool areBeesFed = false;

        public Snowman()
        {
            type = EntityType.Snowman;
            speed = (int)MonsterSpeed.Slow;

            // specific animation collection for this entity
            aColl.Add(Game.I.resourceManager.GetSprite("Snowman"), 200);

            // add animation to sprite.
            sprite.Animations.Add("walk", aColl);
            sprite.CurrentAnimation = "walk";
            sprite.Animate = true;
        }
    }
}
