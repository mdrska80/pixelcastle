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

        #region update
        public int speed { get; set; }
        public int sspeeed { get; set; }        

        public bool timeToUpdate
        {
            get
            {
                sspeeed++;
                bool toReturn = (sspeeed >= speed);

                if (toReturn)
                    sspeeed = 0;

                return toReturn;
            }
        }
        #endregion


        public virtual void Update()
        {
            //ClearHighlightedPath(highlightedPlatforms);
            //highlightedPlatforms = HighlightPath(position, chasingTarget);
        }

        public virtual bool TryToPickup()
        {
            return false;
        }        
    }
}
