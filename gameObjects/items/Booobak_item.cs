using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles.gameObjects
{
    public class Booobak_item : Item
    {
        public bool isUltra {get;set;}

        public Booobak_item()
        {
        }

        public Booobak_item(IGPos pos)
        {
            position = pos;
        }

        public override bool TryToPickup()
        {
            // Create new monster
            Booobak b = new Booobak(isUltra);

            // Add it to game
            Game.I.level.Monsters.Add(b);

            return true;
        }   
    }
}
