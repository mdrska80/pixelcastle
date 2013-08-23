using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics.Sprites;

namespace Castles
{
    public class TestMob : Monster
    {
        public TestMob() : base()
        {
            type = EntityType.TestMob;
            speed = (int)MonsterSpeed.Normal;

            // specific animation collection for this entity
            aColl.Add(Game.I.resourceManager.GetSprite("TestAnim"), 200);

            // add animation to sprite.
            sprite.Animations.Add("walk", aColl);
            sprite.CurrentAnimation = "walk";
            sprite.Animate = true;                
        }
    }
}
