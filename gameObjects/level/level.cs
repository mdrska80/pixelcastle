using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using System.Linq;
using Castles.Conf;
using Castles.Tools;
using System.Collections.Generic;
using Castles.gameObjects;

namespace Castles
{
	public partial class Level
	{
		public Level()
		{
			// default behavior
			level = 1;

			handledPlaforms= new List<Tile>();
			Monsters = new List<Monster>();

            Game.I.eventManager.OnTurnEnd += eventManager_OnTurnEnd;
		}

        void eventManager_OnTurnEnd()
        {
            Update();

            foreach (var handledPlaform in Game.I.level.handledPlaforms)
            {
                handledPlaform.handled = false;
            }

            Game.I.level.handledPlaforms.Clear();
        }

        /// <summary>
        /// AllGems
        /// NoMonsters
        /// </summary>
		[XmlAttribute]
		public string goals {get;set;}

        /// <summary>
        /// Level name, which will be visible on the game board.
        /// </summary>
		[XmlAttribute]
		public string name {get;set;}

        public List<Tile> Platforms{ get; set; }

		public List<EntityDef> EntityDefs { get; set; }

		// this will move around game arena so it is separe....
		[XmlIgnore]
		public List<Monster> Monsters { get; set; }

    	[XmlIgnore]
		public List<Tile> platformsToRemove = new List<Tile>();
		
		[XmlIgnore]
		public List<Tile> handledPlaforms { get; set; }

		[XmlIgnore]
		public int activeGems { get; set; }

		[XmlIgnore]
		public int level { get; set; }

		public Tile GetPlatform(int x, int y, int layer, bool excludeElevators = false)
		{
		    if (excludeElevators)
		        return (from i in Platforms
		                where i.x == x && i.y == y && i.layer == layer && i.elevator == null 
                        select i).FirstOrDefault();

			return (from i in Platforms
						where i.x == x && i.y == y && i.layer == layer
						select i).FirstOrDefault();
		}

        public List<Tile> GetElevators(int x, int y)
        {
            return (from i in Platforms
                    where i.x == x && i.y == y && i.elevator!=null
                    select i).ToList();
        }


		public Tile GetPlatform(IGPos pos)
		{
			return GetPlatform(pos.X, pos.Y, pos.Layer);
		}
        
		public static Level Load(int level)
		{
			string path = Path.Combine(Game.I.resourceManager.gameDirectory, string.Format("Levels/level_{0:00}.xml", level));
			Level l = Serializer<Level>.Deserialize(path);

            if (l == null)
            {  
                l = new Level();
                l.Platforms = new List<Tile>();
            }


			l.level = level;

            CreateMonsters(l);
            CreateItems(l);
            
            return l;
		}

		
	    private static void CreateMonsters(Level l)
	    {
            if (l.EntityDefs != null && l.EntityDefs.Count > 0)
            {
                foreach (var entityDef in l.EntityDefs)
                {
                    Entity m = Entity.CreateEntity(entityDef);

                    if (m is Player)
                        Game.I.player = (Player)m;
                    else
                        l.Monsters.Add((Monster)m);
                }
            }
	    }

	    private static void CreateItems(Level l)
	    {
            foreach (var platform in l.Platforms)
            {
                if (platform.contains == null) platform.contains = string.Empty;

                if (platform.contains.Contains("gem"))
                {
                    l.activeGems++;
                    platform.item = new Gem();
                }
            }
	    }

	    public void Save()
		{
			string path = Path.Combine(Game.I.resourceManager.gameDirectory, string.Format("Levels/level_{0:00}.xml", level));

			Sort();
			Serializer<Level>.Serialize(path, this);

	        ClearXml(path);


		}

	    public void ClearXml(string path)
	    {
	        string s = File.ReadAllText(path);
	        s = s.Replace("ColumnHeight=\"0\" ", "");
	        s = s.Replace("ShiftX=\"0\" ", "");
            s = s.Replace("ShiftY=\"0\" ", "");
            s = s.Replace("contains=\"\" ","");

            s = s.Replace("monsterCannotPass=\"false\" ","");
            s = s.Replace("playerCannotPass=\"false\" ","");
            s = s.Replace("isPit=\"false\" ","");
            s = s.Replace("isPressurePlate=\"false\" ","");


            //add xml comments
	        string comments = @"<!--

Level
=====
goals = not defined at this moment. This will be used to trigger alternative level ending
name = Level name, which will be visible on the game board.
theme = Level name, which will be visible on the game board.

Tile
========
gfx = for custom GFX on specific platform
x = x
y = y
type = platform type (Column, Custom)
ColumnHeight = if u do not want column up do total down. So how many platforms from up will be drawn.
ShiftX = gfx shiftX
ShiftY = gfx shiftY
contains = what can be placed on platform, most common will be gem
monsterCannotPass = block platform for Monsters
playerCannotPass = block platform for player
isPit = pit
layer = layer
-->"+Environment.NewLine+Environment.NewLine;

            

            File.WriteAllText(path, comments+s);
	    }

	    /// <summary>
        /// we will draw one column at a time
        /// 
        /// 
        /// x
        /// layer
        /// last increase will be y
        /// </summary>
		public void Sort()
		{
            Platforms = Platforms.OrderByDescending(a => a.layer).ToList();
            Platforms = Platforms.OrderBy(a => a.x).ToList();

            // y form 0..1..2..etc
            Platforms = Platforms.OrderBy(a => a.y).ToList();
        }

		public Level Reload()
		{
			Level l = Load(level);

			if (l == null) l = this;
			return l;
		}

		public static Level LoadNextLevel()
		{
			return null;
		}

		public void Update()
		{
			//update all platforms and other level items

			foreach (Tile p in Platforms)
			{
				p.Update();
			}

			foreach (var platform in platformsToRemove)
			{
                Game.I.level.RemovePlatform(platform);
			}

			platformsToRemove.Clear();

			//we were rearanging platfornms...
			Sort();
		}

        public void RemovePlatform(Tile p)
        {
            var x = (from i in Platforms
                     where i.x == p.x && i.y == p.y && p.layer == i.layer
                     select i
                    ).FirstOrDefault();

            if (x != null)
                Platforms.Remove(x);
        }

	    public int GetMaxLayer()
	    {
            if (Platforms.Count > 0)
	            return Platforms.Max(x => x.layer);

	        return 30;
	    }

		/// <summary>
		/// Metoda vrati monstum, ktere na danem policku aktualne stoji.
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="layer"></param>
		/// <returns></returns>
		public static Monster GetMonsterOnPlatform(int x, int y, int layer)
		{
			return (from i in Game.I.level.Monsters where 
						 i.position.X == x &&
						 i.position.Y == y &&
						 i.position.Layer == layer
						 select i).FirstOrDefault();
		}

	    public static Tile CheckPlatform(IGPos desiredPosition, IGPos position)
		{

			//search for platform on same layer
			Tile pTile = Game.I.level.GetPlatform(desiredPosition.X, desiredPosition.Y, position.Layer, true);

			if (pTile == null)
			{
				// search for platform on higher layer
                pTile = Game.I.level.GetPlatform(desiredPosition.X, desiredPosition.Y, position.Layer + 1, true);

				if (pTile == null)
				{
					// search for platform on lower layer
                    pTile = Game.I.level.GetPlatform(desiredPosition.X, desiredPosition.Y, position.Layer - 1, true);
				}
			}

	        if (pTile == null)
	        {
                //we can have multiple elevators....
	            List<Tile> pElevatorPlatforms = Game.I.level.GetElevators(desiredPosition.X, desiredPosition.Y);

	            if ((pElevatorPlatforms != null) && (pElevatorPlatforms.Count > 0))
	            {
                    //je to dostatecne blizko tomu co stojim?
	                Tile ppp = pElevatorPlatforms[0];
	                if (ppp.elevator.Current == position.Layer) pTile = ppp;
                    if (ppp.elevator.Current + 1 == position.Layer) pTile = ppp;
                    if (ppp.elevator.Current - 1 == position.Layer) pTile = ppp;
	            }
	        }

	        // if no platform we cannot simply go there
			if (pTile == null) return null;

			//we have platform...

			// is it elevator? Can we get on?
			if (pTile.elevator != null)
			{

				if (pTile.elevator.Current == position.Layer) return pTile;
				if (pTile.elevator.Current == position.Layer - 1) return pTile;
				if (pTile.elevator.Current == position.Layer + 1) return pTile;
			}

			// is platform moving? Can we get on?
			if (pTile.moving != null)
			{
				if (!(pTile.moving.Current.X == desiredPosition.X &&
					pTile.moving.Current.Y == desiredPosition.Y))
				{
					//platform is somewhere else...we cannot get on.
					return null;
				}
			}

			// if we are here no other universal obstackle is in place
			return pTile;
		}

		public void ClearPathFindingInfo()
		{
			foreach (var platform in Platforms)
			{
				platform.pathfindingValues.Clear();
			}
		}

		/// <summary>
		/// reset monster positions. Tohle je dobre kdyz hrac prijde o zivot a monstra by se mela
		/// rozmistit do pozic, ve kterych zacinala
		/// </summary>
        public void PutMonstersToOriginalPositions()
        {
            List<Monster> ms = Game.I.level.Monsters;

            foreach (Monster m in ms)
            {
                m.position.X = m.positionOriginal.X;
                m.position.Y = m.positionOriginal.Y;
                m.position.Layer = m.positionOriginal.Layer;
            }
        }
    }
}