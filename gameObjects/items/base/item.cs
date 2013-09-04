using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Diagnostics;

using SdlDotNet;
using SdlDotNet.Audio;
using SdlDotNet.Graphics;

namespace Castles
{
    public class Item
    {
        public string id = Guid.NewGuid().ToString();
        public IGPos position {get;set;}

        public Item()
        {
            Game.I.eventManager.OnTurnEnd += eventManager_OnTurnEnd;
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
    }
}
