using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castles.gameObjects;

namespace Castles
{
    public class Bees : Monster
    {
        public bool areBeesFed = false;
        public int howManyStepsLife = 10;

        public Bees()
        {
            type = EntityType.Bees;
            speed = (int)MonsterSpeed.Fast;

            // specific animation collection for this entity
            aColl.Add(Game.I.resourceManager.GetSprite("Bees"), 200);

            // add animation to sprite.
            sprite.Animations.Add("walk", aColl);
            sprite.CurrentAnimation = "walk";
            sprite.Animate = true;
        }

        public override void Update()
        {
            if (timeToUpdate)
            {
                // bees are alive only for a limited amount of time....
                if (howManyStepsLife <= steps)
                {
                    // look for cauldron
                    HoneyCauldron hc = Game.I.level.HoneyCauldron;

                    if ((hc != null) && (hc.Filled))
                    {
                        //do the bees smell the honey?
                        chasingTarget = hc.position;

                        if (position == hc.position)
                        {
                            hc.Filled = false;
                            areBeesFed = true;
                        }
                    }
                    else
                    {
                        // do as any other monster do, chase player
                        base.Update();
                    }
                }
            }
        }



    }
}
