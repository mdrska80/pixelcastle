using System;
using System.IO;
using System.Drawing;
using System.Collections;
using Castles.Conf;
using Castles.Views;
using SdlDotNet.Graphics;
using SdlDotNet.Input;
using SdlDotNet.Audio;
using SdlDotNet.Core;

namespace Castles
{
	/// <summary>
	/// Summary description for GameView.
	/// </summary>
	public class GameView
	{
		private ResourceManager resourceManager;
		private Game game { get; set; }

		// Views
		private IView interfaceView { get; set; }
		private IView levelView { get; set; }
		private IView backgroundView {get;set;}
		private IView debugView {get;set;}
		private IView editorView { get; set; }
        private IView gameoverView { get; set; }


		//Todo views
		private IView scoreView {get;set;}
		private IView mainmenuView {get;set;}


		//surfaces required for this View
		// main surface
		Surface surf;

		/// <summary>
		/// constructor
		/// </summary>
		public GameView(ResourceManager resourceManager)
		{
			this.resourceManager = resourceManager;

			Events.Tick += new EventHandler<TickEventArgs>(this.Tick);
			Events.Quit += new EventHandler<QuitEventArgs>(this.Quit);
		}

		/// <summary>
		/// 
		/// </summary>
		public void CreateView(Game g)
		{
			try
			{
				game = g;
				//screen = Video.SetVideoMode(1024, 768);
				Video.WindowIcon();
				Video.WindowCaption = resourceManager.Texts["WINDOW_CAPTION"];
				Video.SetVideoMode(1024,768);//, false, false, false, true, true);

				this.surf = Video.Screen.CreateCompatibleSurface();

				//fill the surface with black
				//this.surf.Fill(new Rectangle(new Point(0, 0), surf.Size), Color.Red);
				Mouse.ShowCursor = false;

				interfaceView = new interfaceView(new Point(0,0));
				levelView = new levelView(Game.I.boardOrigin);

				backgroundView = new backgroundView(new Point(0,0));
				debugView = new debugView(new Point(0, 0));
				editorView = new editorView(new Point(0, 0));

	            gameoverView = new gameoverView(new Point(0,0));
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
			}
		}  
		
		/// <summary>
		/// 
		/// </summary>
		public void UpdateView()
		{
			//this.surf.Fill(new Rectangle(new Point(0, 0), surf.Size), Color.Black);

			Game.I.surfaces = 0;

			if (surf == null) return; //nothing to update

		    switch (Game.I.Screen)
		    {
                case Screens.menu:
		            {
                        backgroundView.UpdateView(surf);
                        break;
		            }
                case Screens.game:
		            {
                        backgroundView.UpdateView(surf);
                        levelView.UpdateView(surf);
                        interfaceView.UpdateView(surf);
                        debugView.UpdateView(surf);
                        break;
		            }
                case Screens.editor:
		            {
                        backgroundView.UpdateView(surf);

                        interfaceView.UpdateView(surf);
                        levelView.UpdateView(surf);
                        debugView.UpdateView(surf);
                        editorView.UpdateView(surf);
                        break;
		            }
                case Screens.statistics:
		            {
                        backgroundView.UpdateView(surf);
                        debugView.UpdateView(surf);
		                break;
		            }
                    case Screens.gameOver:
		            {
                        backgroundView.UpdateView(surf);
                        debugView.UpdateView(surf);
		                gameoverView.UpdateView(surf);
                        break;
		            }
		        default:
		            {
                        backgroundView.UpdateView(surf);
                        debugView.UpdateView(surf);

                        break;
		            }

		    }




            //if (i == 1)
            //{
            //    DirectoryInfo di = new DirectoryInfo(Path.Combine(CastlesConfigurationReader.GetConfiguration().DataFolder, "Screenshots"));

            //    if (di.Exists)
            //    {
            //        FileInfo[] fi = di.GetFiles();
            //        surf.SaveBmp(Path.Combine(CastlesConfigurationReader.GetConfiguration().DataFolder,
            //                                  string.Format("Screenshots/shot{0}.bmp", fi.GetLength(0))));
            //    }

            //    i = 2;
            //}

		    // blit
			Video.Screen.Blit(surf);
			Video.Screen.Update();
		}

	    //private int i = 1;

		float lastTime = 0;
		private void Tick(object sender, TickEventArgs e)
		{
			if (game != null)
			{
				float time = Timer.TicksElapsed;
				if (time - lastTime > 50) //every second, update game objects
				{
					//thread?
					UpdateGameObjects();
					lastTime = Timer.TicksElapsed;
				}

				UpdateView();
			}
		}

		private void UpdateGameObjects()
		{
			if (!Game.I.isPaused)
			{
                if (Game.I.player!=null)
                    Game.I.player.Update();

                if (Game.I.level != null)
                {
                    Game.I.level.ClearPathFindingInfo();

                    if (Game.I.level.Monsters != null)
                    {
                        // Update monster positions
                        foreach (Monster m in Game.I.level.Monsters)
                        {
							if (m!=null)
                            	m.Update();
                        }
                    }

                    // update ingame items

                    // is there a cauldron?
                    if (Game.I.level.HoneyCauldron != null)
                        Game.I.level.HoneyCauldron.Update();

                    // update platforms, move them, etc...
                    Game.I.level.Update();

                    //clear handled platforms
                    foreach (var handledPlaform in Game.I.level.handledPlaforms)
                    {
                        handledPlaform.handled = false;
                    }

                    Game.I.level.handledPlaforms.Clear();
                }

			}
		}

		private void Quit(object sender, QuitEventArgs e)
		{
			Events.QuitApplication();
		}
	}
}
