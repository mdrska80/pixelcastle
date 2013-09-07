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
		/// <summary>
		/// Every item hs its own unique id
		/// </summary>
        public string id = Guid.NewGuid().ToString();

		/// <summary>
		/// Is item cursed?
		/// - for all item types:
		///   - cannot be removed
		///   - it has some problematic stats
		/// - for armors:
		///   - lower defense
		/// - for weapons
		///   - lower attack
		///   - slower
		/// </summary>
		/// <value><c>true</c> if is cursed; otherwise, <c>false</c>.</value>
        public bool isCursed { get; set; }

		/// <summary>
		/// Every object in game is unidentified. There must exist "known item list" because plyer is not n idiot.
		/// If player will find another item it will be automatically identified. For example: Player will find
		/// "Red potion" and by reading identify scroll or by tasting or by identify in library this potion will
		/// become "Healing potion". Color red is for each game random. Rogue-like scenario. 
		/// </summary>
		/// <value><c>true</c> if is identified; otherwise, <c>false</c>.</value>
		public bool isIdentified {get;set;}
        
		/// <summary>
		/// Position of the object in the realm. Every entity, object, ... has its own position in world,
		/// even objects in players inventory. So when player dies objects will be dropped on the ground.
		/// </summary>
		/// <value>The position.</value>
		public IGPos position {get;set;}

		/// <summary>
		/// Main animated sprite for object. Of course not every object have to be animated but it is better
		/// option if no object animation. 
		/// </summary>
		/// <value>The sprite.</value>
        public SpriteAnimatedEx Sprite { get; set; }

		/// <summary>
		/// ctor
		/// </summary>
        public Item()
        {
            Game.I.eventManager.OnTurnEnd += eventManager_OnTurnEnd;
            InitGfx();
        }

		/// <summary>
		/// On every turn end(which is different if this object is held by monster nd different
		/// end turn if held by player) there can be an action. For example:
		/// - Torch: burning out
		/// - Charm: one charge off
		/// </summary>
        void eventManager_OnTurnEnd()
        {
            Update();
        }

        public virtual void Update()
        {
        }

		/// <summary>
		/// Method is called when someone tries to pick this up.
		/// </summary>
		/// <returns><c>true</c>, if to pickup was tryed, <c>false</c> otherwise.</returns>
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
