using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using SFML.Graphics;

using Castles.Conf;

namespace Castles
{
	/// <summary>
	/// Summary description for ResourceManager.
	/// </summary>
	public class ResourceManager
	{
		public Dictionary<string, Texture> Cache_texture {get;set;}
		public Dictionary<string, object> Cache_audio {get;set;}
		public Dictionary<string, string> Texts { get; set; }

		public string gameDirectory {get;set;}

		public void Init(string dir)
		{
			Cache_texture = new Dictionary<string, Texture>();
			Cache_audio  = new Dictionary<string, object>();
			Texts = new Dictionary<string, string>();

			this.gameDirectory = dir;

			// load all from config
			if (CastlesConfigurationReader.GetConfiguration().Gfxs  !=null)
			{
				foreach(Gfx pgfx in CastlesConfigurationReader.GetConfiguration().Gfxs)
				{
                    Load_Texture(pgfx);

				    if ((pgfx.theme != null)&&(!Game.I.themes.ContainsKey(pgfx.theme)))
				    {
                        // init theme
				        InitTheme(pgfx.theme);
				    }
				}
			}

			// texts
			Texts.Add("WINDOW_CAPTION", "test window caption");

            //// animated sprites...
            //Load_Sprite("TestAnim.png", "TestAnim", 32, 32, 0);

            //// monsters
            //Load_Sprite("TreeTough.png", "TreeTough", 61, 65, 0);
            //Load_Sprite("TreeFeeding.png", "TreeFeeding", 32, 64, 0);
            //Load_Sprite("TreeDead.png", "TreeDead", 61, 63, 0);
            //Load_Sprite("Thuja.png", "Thuja", 29, 60, 0);
            //Load_Sprite("Snowman.png", "Snowman", 34, 35, 0);
            //Load_Sprite("BooobakMonster.png", "BooobakMonster", 32, 35, 0);

            //// player sprites ... load them
            //Load_Sprite("player.png", "player_walk_right_down", 48, 64, 2);
            //Load_Sprite("player.png", "player_walk_right_up", 48, 64, 3);
            //Load_Sprite("player.png", "player_walk_left_down", 48, 64, 0);
            //Load_Sprite("player.png", "player_walk_left_up", 48, 64, 1);
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
                Texture tx = Load_Texture(gfx);


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

	    public Texture Load_Texture(Gfx g)
		{
			try
			{
				string fullFileName = Path.Combine(gameDirectory, g.gfx);

				if (File.Exists(fullFileName))
				{
                    Texture tx = new Texture(fullFileName);
                    FileInfo fi = new FileInfo(g.gfx);
				    if (!Cache_texture.ContainsKey(fi.Name))
				    {
				        Cache_texture.Add(g.code, tx);
                        Console.WriteLine("Added texture: " + g.code);
				    }

				    return tx;
				}

                Console.WriteLine("Texture was not added (probably missing): " + g.gfx);
			}
			catch (Exception ex)
			{
                Console.WriteLine("Failed to add texture: " + g.gfx);
				Console.WriteLine("ex: " + ex.Message.ToString());
			}

            return null;
		}

		public void Load_Audio(string fileName)
		{

		}

		public Texture GetGfx(string filename)
		{
			if ((!string.IsNullOrEmpty(filename))&&(Cache_texture.ContainsKey(filename)))
				return Cache_texture[filename];

			return null;
		}

		public object GetAudio(string filename)
		{
			if (Cache_audio.ContainsKey(filename))
				return Cache_audio[filename];

			return null;
		}	
	}
}