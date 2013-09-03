using System;
using System.IO;
using System.Drawing;
using System.Collections;

// SFML
using SFML;
using SFML.Window;
using SFML.Graphics;

// Castle references
using Castles.Conf;
using Castles.Views;


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
		//Surface surf;

		/// <summary>
		/// constructor
		/// </summary>
		public GameView(ResourceManager resourceManager)
		{
			this.resourceManager = resourceManager;

			//Events.Tick += new EventHandler<TickEventArgs>(this.Tick);
			//Events.Quit += new EventHandler<QuitEventArgs>(this.Quit);
		}

		/// <summary>
		/// 
		/// </summary>
		public void CreateView(Game g)
		{
			try
			{
				game = g;

                #region SFML initialization
                // Request a 32-bits depth buffer when creating the window
                ContextSettings contextSettings = new ContextSettings();
                contextSettings.DepthBits = 32;

                // Create the main window
                Window window = new Window(new VideoMode(1024, 768), "SFML window with OpenGL", Styles.Default, contextSettings);
                window.SetTitle(resourceManager.Texts["WINDOW_CAPTION"]);

                // Make it the active window for OpenGL calls
                window.SetActive();

                // Setup event handlers
                window.Closed += OnClosed;
                window.KeyPressed += OnKeyPressed;
                window.LostFocus += window_LostFocus;
                window.GainedFocus += window_GainedFocus;
                window.TextEntered += new EventHandler<TextEventArgs>(window_TextEntered);

                #endregion

                #region Views initializations
                //interfaceView = new interfaceView(new Point(0, 0));
                //levelView = new levelView(Game.I.boardOrigin);

                //backgroundView = new backgroundView(new Point(0, 0));
                //debugView = new debugView(new Point(0, 0));
                //editorView = new editorView(new Point(0, 0));
                //gameoverView = new gameoverView(new Point(0, 0));

                #endregion

                // Start the game loop
                while (window.IsOpen())
                {
                    // Process events
                    window.DispatchEvents();

                    // tohle je trochu blby...to by se melo obnovovat jen parkrat za cas...
                    UpdateGameObjects();
                    
                    //redraw whatever we need
                    UpdateViews();

                    // Finally, display the rendered frame on screen
                    window.Display();
                }

				//screen = Video.SetVideoMode(1024, 768);
				//Video.WindowIcon();
				//Video.WindowCaption = resourceManager.Texts["WINDOW_CAPTION"];
				//Video.SetVideoMode(1024,768);//, false, false, false, true, true);

				//this.surf = Video.Screen.CreateCompatibleSurface();

				//fill the surface with black
				//this.surf.Fill(new Rectangle(new Point(0, 0), surf.Size), Color.Red);
				//Mouse.ShowCursor = false;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

	    private string buffer = "";
        void window_TextEntered(object sender, TextEventArgs e)
        {
            // did we hit enter?
            if (e.Unicode == "\r")
            {
                //process command
                Console.WriteLine("Processing command: {0}", buffer);
                buffer = string.Empty;                                  // clean the buffer
                return;
            }

            buffer += e.Unicode;
        }

        void window_GainedFocus(object sender, EventArgs e)
        {
            Game.I.UnPause();
        }

        void window_LostFocus(object sender, EventArgs e)
        {
            Game.I.Pause();
        }  
		
		/// <summary>
		/// 
		/// </summary>
		public void UpdateViews()
		{
			//this.surf.Fill(new Rectangle(new Point(0, 0), surf.Size), Color.Black);

			Game.I.surfaces = 0;

            //if (surf == null) return; //nothing to update

            //switch (Game.I.Screen)
            //{
            //    case Screens.menu:
            //        {
            //            backgroundView.UpdateView(surf);
            //            break;
            //        }
            //    case Screens.game:
            //        {
            //            backgroundView.UpdateView(surf);
            //            levelView.UpdateView(surf);
            //            interfaceView.UpdateView(surf);
            //            debugView.UpdateView(surf);
            //            break;
            //        }
            //    case Screens.editor:
            //        {
            //            backgroundView.UpdateView(surf);

            //            interfaceView.UpdateView(surf);
            //            levelView.UpdateView(surf);
            //            debugView.UpdateView(surf);
            //            editorView.UpdateView(surf);
            //            break;
            //        }
            //    case Screens.statistics:
            //        {
            //            backgroundView.UpdateView(surf);
            //            debugView.UpdateView(surf);
            //            break;
            //        }
            //        case Screens.gameOver:
            //        {
            //            backgroundView.UpdateView(surf);
            //            debugView.UpdateView(surf);
            //            gameoverView.UpdateView(surf);
            //            break;
            //        }
            //    default:
            //        {
            //            backgroundView.UpdateView(surf);
            //            debugView.UpdateView(surf);

            //            break;
            //        }

            //}

		    // blit
//			Video.Screen.Blit(surf);
	//		Video.Screen.Update();
		}

	    //private int i = 1;

        //float lastTime = 0;
        //private void Tick(object sender, TickEventArgs e)
        //{
        //    if (game != null)
        //    {
        //        float time = Timer.TicksElapsed;
        //        if (time - lastTime > 50) //every second, update game objects
        //        {
        //            //thread?
        //            UpdateGameObjects();
        //            lastTime = Timer.TicksElapsed;
        //        }

        //        UpdateView();
        //    }
        //}

        private void UpdateGameObjects()
        {
            if (!Game.I.isPaused)
            {
                if (Game.I.player != null)
                    Game.I.player.Update();

                if (Game.I.level != null)
                {
                    Game.I.level.ClearPathFindingInfo();

                    if (Game.I.level.Monsters != null)
                    {
                        // Update monster positions
                        foreach (Monster m in Game.I.level.Monsters)
                        {
                            if (m != null)
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

        /// <summary>
        /// Function called when the window is closed
        /// </summary>
        static void OnClosed(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }

        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;
            if (e.Code == Keyboard.Key.Escape)
                window.Close();
        }
	}
}
