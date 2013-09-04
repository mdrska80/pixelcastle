using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using SFML.Graphics;
using SdlDotNet;
using SdlDotNet.Audio;
using SdlDotNet.Graphics;

namespace Castles
{
    public class Gem : Item
    {
        public bool picked = false;
        public GemType type = GemType.normal;
        public int charges { get; set; }

        #region graphics
            private Sprite sMain;
        #endregion

        public override bool TryToPickup()
        {
            charges--;

            if (charges <= 0)
                picked = true;

            return picked;
        }

        /// <summary>
        /// GFX initialization
        /// </summary>
        public override void InitGfx()
        {
            sMain = new Sprite(Game.I.resourceManager.GetGfx("GEM"));
            base.InitGfx();
        }
    }

    public enum GemType
    {
        normal,

        /// <summary>
        /// decreasing points by crossing over
        /// </summary>
        inverse,
    }
}
