using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles
{
    public class CrystalBall : Monster
    {

        public CrystalBall() : base()
        {
            type = EntityType.CrystalBall;
            speed = (int)MonsterSpeed.Normal;

            // specific animation collection for this entity
            aColl.Add(Game.I.resourceManager.GetSprite("CrystalBall"), 200);

            // add animation to sprite.
            sprite.Animations.Add("walk", aColl);
            sprite.CurrentAnimation = "walk";
            sprite.Animate = true;
        }
    }
}
