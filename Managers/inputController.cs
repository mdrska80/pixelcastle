using System;
using System.Collections.Generic;
using Castles.gameObjects;
using Castles.gameObjects.items;
using SdlDotNet.Core;
using SdlDotNet.Input;

namespace Castles
{
	/// <summary>
	/// Derived class
	/// </summary>
	public class InputController
	{
		/// <summary>
		/// constructor
		/// </summary>
		public InputController()
		{
		}

		
		/// <summary>
		/// 
		/// </summary>
		public void Go()
		{
			Events.KeyboardDown += new EventHandler<KeyboardEventArgs>(this.KeyboardDown);
			Events.MouseButtonDown += new EventHandler<MouseButtonEventArgs>(Events_MouseButtonDown);

			Events.Run();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void KeyboardDown(object sender, KeyboardEventArgs e)
		{
			if (Game.I.Screen == Screens.game)
			{
				//process keys for game screen
				if (e.Key == Key.UpArrow)
				{
					Game.I.player.Move(Direction.up);
					//move player up 
				}
				else if (e.Key == Key.DownArrow)
				{
					//move player down
					Game.I.player.Move(Direction.down);

				}
				else if (e.Key == Key.LeftArrow)
				{
					//move player Left
					Game.I.player.Move(Direction.left);

				}
				else if (e.Key == Key.RightArrow)
				{
					//move player right
					Game.I.player.Move(Direction.right);

				}
			}

			if (Game.I.Screen == Screens.editor)
			{
				//process keys for editor screen
				//process keys for game screen
				if (e.Key == Key.UpArrow)
				{
					Game.I.player.Move(Direction.up);
					//move player up 
				}
				else if (e.Key == Key.DownArrow)
				{
					//move player down
					Game.I.player.Move(Direction.down);

				}
				else if (e.Key == Key.LeftArrow)
				{
					//move player Left
					Game.I.player.Move(Direction.left);

				}
				else if (e.Key == Key.RightArrow)
				{
					//move player right
					Game.I.player.Move(Direction.right);

				}

				// ctrl up ... move board up


				if (e.Key == Key.M)
				{
					//process keys for editor screen

				}

				if (e.Key == Key.B)
				{
					//put items on the screen
					Game.I.editingObject = EditorObjects.box;
				}				

				if (e.Key == Key.C)
				{
					//put items on the screen
					Game.I.editingObject = EditorObjects.cauldron;
				}

				if (e.Key == Key.G)
				{
					//put gems on the screen
					Game.I.editingObject = EditorObjects.gems;

				}

				if (e.Key == Key.P)
				{
					//put plaforms on the screen
					Game.I.editingObject = EditorObjects.platforms;
				}

                // move board
                if (e.Key == Key.R)
                {
                    //process keys for editor screen
                    Game.I.level.Rotate();
                    Game.I.level.Rotate();
                }

                if (e.Key == Key.W)
                    Game.I.level.MoveUp();

                if (e.Key == Key.S)
                    Game.I.level.MoveDown();

                if (e.Key == Key.A)
                    Game.I.level.MoveLeft();

                if (e.Key == Key.D)
                    Game.I.level.MoveRight();




			}

			// global keys...

			if (e.Key == Key.Escape || e.Key == Key.Q)
			{
				Events.QuitApplication();
			}
			else if (e.Key == Key.U)
			{
				if (Game.I.level!=null)
					Game.I.level = Game.I.level.Reload();
			}
		}

		void Events_MouseButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (Game.I.Screen == Screens.menu)
			{
				//scroll thru options...
			}


			if (Game.I.Screen == Screens.game)
			{

			}

			if (Game.I.Screen == Screens.editor)
			{
				if (e.Button == MouseButton.WheelDown)
					Game.I.editingPlatform.Layer--;

				if (e.Button == MouseButton.WheelUp)
					Game.I.editingPlatform.Layer++;

				if (e.Button == MouseButton.PrimaryButton)
				{
					int x, y;
                    int layer = Game.I.editingPlatform.Layer;
					Platform p = null;
					GetLAndP(out p, out x, out y);

					if (p!=null)
					{
						if (Game.I.editingObject == EditorObjects.platforms)
							Game.I.level.RemovePlatform(p);

						if (Game.I.editingObject == EditorObjects.gems)
						{
							CheckAndAddEntity(p, "gem");
							p.item = new Gem();
							Game.I.level.activeGems++;
						}

						if (Game.I.editingObject == EditorObjects.cauldron)
						{
							CheckAndAddEntity(p, "cauldron");
							p.item = new HoneyCauldron();
						}

						if (Game.I.editingObject == EditorObjects.barrel)
						{
							CheckAndAddEntity(p, "barrel");
							p.item = new Barrel();
						}

						if (Game.I.editingObject == EditorObjects.box)
						{
							CheckAndAddEntity(p, "box");
							p.item = new Box();
						}
					}
					else
					{
                        Platform px = Game.I.level.CreatePlatform(x, y, layer);
                        Game.I.level.Platforms.Add(px);
					}

                    Game.I.level.Save();
				}
			}
		}

		public void CheckAndAddEntity(Platform p, string key)
		{
			if (p.contains.Contains(key))
				p.contains = p.contains.Replace(key, "");
			else
				p.contains += ","+key;
		}

		public void GetLAndP(out Platform platform, out int x, out int y)
		{
			//real pos
			x = Game.I.editingPlatform.X;
			y = Game.I.editingPlatform.Y;
			int layer = Game.I.editingPlatform.Layer;

			platform = Game.I.level.GetPlatform(x, y, layer);			
		}
	}
}