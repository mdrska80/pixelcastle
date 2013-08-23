using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using SdlDotNet;
using SdlDotNet.Graphics;

using Castles.Conf;

namespace Castles
{
	/// <summary>
	/// Summary description for ResourceManager.
	/// </summary>
	public class ResourceManager
	{
		public Dictionary<string, Surface> Cache_gfx {get;set;}
		public Dictionary<string, object> Cache_audio {get;set;}
		public Dictionary<string, SurfaceCollection> Cache_sprite {get;set;}
		public Dictionary<string, string> Texts { get; set; }

		public string gameDirectory {get;set;}

		public void Init(string gameDirectory)
		{
			Cache_gfx = new Dictionary<string, Surface>();
			Cache_audio  = new Dictionary<string, object>();
			Cache_sprite  = new Dictionary<string, SurfaceCollection>();
			Texts = new Dictionary<string, string>();

			this.gameDirectory = gameDirectory;

			// load all from config
			if (CastlesConfigurationReader.GetConfiguration().Gfxs  !=null)
			{
				foreach(Gfx pgfx in CastlesConfigurationReader.GetConfiguration().Gfxs)
				{
					Load_Gfx(pgfx.gfx);

				    if ((pgfx.theme != null)&&(!Game.I.themes.ContainsKey(pgfx.theme)))
				    {
                        // init theme
				        InitTheme(pgfx.theme);
				    }
				}
			}

			// texts
			Texts.Add("WINDOW_CAPTION", "test window caption");

			// animated sprites...
			Load_Sprite("TestAnim.png", "TestAnim", 32, 32, 0);

			// monsters
			Load_Sprite("TreeTough.png", "TreeTough", 61, 65, 0);
			Load_Sprite("TreeFeeding.png", "TreeFeeding", 32, 64, 0);
			Load_Sprite("TreeDead.png", "TreeDead", 61, 63, 0);
			Load_Sprite("Thuja.png", "Thuja", 29, 60, 0);
			Load_Sprite("Snowman.png", "Snowman", 34, 35, 0);
			Load_Sprite("BooobakMonster.png", "BooobakMonster", 32, 35, 0);

			// player sprites ... load them
			Load_Sprite("player.png", "player_walk_right_down", 48, 64, 2);
            Load_Sprite("player.png", "player_walk_right_up", 48, 64, 3);
            Load_Sprite("player.png", "player_walk_left_down", 48, 64, 0);
            Load_Sprite("player.png", "player_walk_left_up", 48, 64, 1);
		}

	    public void InitTheme(string theme)
	    {
            //get all platform relevant to themes
	        var x = (from i in CastlesConfigurationReader.GetConfiguration().Gfxs
	                 where i.theme == theme
	                 select i).ToList();

            Theme t = new Theme();
	        t.name = theme;

	        foreach (Gfx gfx in x)
	        {
                Surface s = Load_Gfx(gfx.gfx);


	            if (gfx.isPit)
	                t.PitPlatform = s;
                else if (gfx.isBlock)
                    t.BlockPlatform = s;
                else if (gfx.isTeleport)
                	t.TeleportPlatform = s;
                else if (gfx.isPressurePlate)
                	t.PressurePlatePlatform = s;
                else
                {
                    SP sp = new SP();
                    sp.surf = s;
                    sp.probability = gfx.probabilty;
                    t.platforms.Add(sp);
                }
	        }

            Game.I.themes.Add(t.name, t);

	    }

	    public Surface Load_Gfx(string fileName)
		{
			try
			{
				string fullFileName = Path.Combine(gameDirectory, fileName);

				if (File.Exists(fullFileName))
				{
					Surface s = new Surface(fullFileName);
					FileInfo fi = new FileInfo(fileName);
				    if (!Cache_gfx.ContainsKey(fi.Name))
				    {
				        Cache_gfx.Add(fi.Name, s);
				        Console.WriteLine("Added bitmap: " + fileName);
				    }

				    return s;
				}

                Console.WriteLine("Bitmap was not added (probably missing): " + fileName);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed to add bitmap: " + fileName);
				Console.WriteLine("ex: " + ex.Message.ToString());
			}

            return null;
		}

		public void Load_Audio(string fileName)
		{

		}

		public void Load_Sprite(string filename, string code, int dimensionX, int dimensionY, int row)
		{
			try
			{			
				Surface s = Cache_gfx[filename];

				// create surface
				SurfaceCollection sc = new SurfaceCollection();
				sc.Add(s, new Size(dimensionX, dimensionY), row);	

				// create animation
				//AnimationCollection ac = new AnimationCollection();
				//ac.Add(sc, 200);

				Cache_sprite.Add(code, sc);
				Console.WriteLine(string.Format("Added animated sprite: {0} with code: {1}", filename, code));
			}
			catch (Exception ex)
			{
				Console.WriteLine("Failed to add animated sprite: " + filename);
				Console.WriteLine("ex: " + ex.Message.ToString());
			}
		}

		public Surface GetGfx(string filename)
		{
			if ((!string.IsNullOrEmpty(filename))&&(Cache_gfx.ContainsKey(filename)))
				return Cache_gfx[filename];

			return null;
		}

		public object GetAudio(string filename)
		{
			if (Cache_audio.ContainsKey(filename))
				return Cache_audio[filename];

			return null;
		}	

		public SurfaceCollection GetSprite(string filename)
		{
			if (Cache_sprite.ContainsKey(filename))
				return Cache_sprite[filename];

			return null;
		}	
	}
}