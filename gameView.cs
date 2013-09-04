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
                   
                    //redraw whatever we need
                    UpdateViews();

                    // Finally, display the rendered frame on screen
                    window.Display();
                }
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

	    private string buffer = "";
        void window_TextEntered(object sender, TextEventArgs e)
        {
            bool handled = false;

            // did we hit enter?
            if (e.Unicode == "\r")
            {
                if (buffer[0] == '/')
                {
                    //process command, always starts with slash
                    Console.WriteLine("Processing command: {0}", buffer);

                    CommandManager.HandleCommand(buffer.Substring(1, buffer.Length-1));

                    buffer = string.Empty; // clean the buffer
                    handled = true;
                }
            }

            //ctrl + i, tab
            if (e.Unicode == "\t")
            {
                Console.WriteLine("CTRL I PRESSED");
                handled = true;
            }

            //backspace
            if (e.Unicode == "\b")
            {
                buffer = buffer.Substring(0, buffer.Length - 1);
                handled = true;
            }

            if (!handled)
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
