using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using SFML.Graphics;
using SFML.Utils;

namespace Castles
{
    public class Item
    {
        public string id = Guid.NewGuid().ToString();
        public bool isCursed { get; set; }
        public IGPos position {get;set;}

        public SpriteAnimatedEx Sprite { get; set; }

        public Item()
        {
            Game.I.eventManager.OnTurnEnd += eventManager_OnTurnEnd;
            InitGfx();
        }

        void eventManager_OnTurnEnd()
        {
            Update();
        }

        public virtual void Update()
        {
        }

        public virtual bool TryToPickup()
        {
            return false;
        }

        public virtual void InitGfx()
        {
            Sprite = new SpriteAnimatedEx();

        }

    }
}
