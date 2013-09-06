using System;
using SFML.Graphics;
using SFML.Window;

using Castles.gameObjects;
using Castles.gameObjects.items;


namespace Castles
{
	/// <summary>
	/// Derived class
	/// </summary>
	public class InputManager
	{
	    private RenderWindow _rw { get; set; }

	    public void InitWindow(RenderWindow window)
	    {
	        _rw = window;

            window.KeyPressed += OnKeyPressed;
	    }


        /// <summary>
        /// Function called when a key is pressed
        /// </summary>
        void OnKeyPressed(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;
            if (e.Code == Keyboard.Key.Escape)
                window.Close();

            switch (Game.I.Screen)
            {
                case Screens.editor:
                case Screens.game:
                    {
                        //process keys for game screen
                        if (e.Code == Keyboard.Key.Up)
                        {
                            Game.I.player.Move(Direction.UP);
                            //move player up 
                        }
                        else if (e.Code == Keyboard.Key.Down)
                        {
                            //move player down
                            Game.I.player.Move(Direction.DOWN);

                        }
                        else if (e.Code == Keyboard.Key.Left)
                        {
                            //move player Left
                            Game.I.player.Move(Direction.LEFT);

                        }
                        else if (e.Code == Keyboard.Key.Right)
                        {
                            //move player right
                            Game.I.player.Move(Direction.RIGHT);

                        }

                        else if (e.Code == Keyboard.Key.D)
                        {
                            Game.I.gameView.mapView.X += 10;
                        }
                        else if (e.Code == Keyboard.Key.A)
                        {
                            Game.I.gameView.mapView.X -= 10;
                        }
                        else if (e.Code == Keyboard.Key.W)
                        {
                            Game.I.gameView.mapView.Y -= 10;
                        }
                        else if (e.Code == Keyboard.Key.S)
                        {
                            Game.I.gameView.mapView.Y += 10;
                        }

                        else if (e.Code == Keyboard.Key.Q)
                        {
                            Vector2i v = Mouse.GetPosition(Game.I.window);
                            Vector2i vv = Game.I.gameView.mapView.renderer.GetTile_Absolute(new Vector2f(v.X, v.Y));
                        }


                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
	}
}