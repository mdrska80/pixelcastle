using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Castles.Tools;

namespace Castles
{
    public class Action_CreatePlatform : BaseAction
    {
        public override void Execute(Tile p, Entity e)
        {
            if (IsActive)
            {
                // target is in parameters
                // Param1 = x
                // Param2 = y
                // Param3 = layer

                // does the platform already exists?
                Tile px = Game.I.level.GetPlatform(Param1.ToInt(), Param2.ToInt(), Param3.ToInt());


                if (px!=null)
                {
                    //no so we can create it
                    //Tile ppp = Game.I.level.CreatePlatform(Param1.ToInt(), Param2.ToInt(), Param3.ToInt());

					// add to level and sort
					//Game.I.level.Platforms.Add(ppp);
					//Game.I.level.Sort();
                }

                if (IsOneTimeAction)
                {
                    IsActive = false;
                }
            }
        }
    }
}
