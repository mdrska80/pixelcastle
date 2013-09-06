using System;
using System.IO;
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
        /// <summary>
        /// Rotate board by 90 degrees
        /// </summary>
		public void Rotate()
		{
            foreach (Tile p in Platforms)
            {
                int i, j;

                i = 20 - p.y - 1;
                j = p.x;

                p.x = i;
                p.y = j;
            } 
		}

		public void ShiftRight()
		{

		}

		public void ShiftLeft()
		{

		}

		public void ShiftUp()
		{

		}

		public void ShiftDown()
		{
			
		}

        /// <summary>
        /// Move whole visible board one step UP
        /// </summary>
	    public void MoveUp()
	    {
            foreach (Tile p in Platforms)
            {
                p.y--;
            }

	        Save();
	    }

	    public void MoveDown()
	    {
            foreach (Tile p in Platforms)
            {
                p.y++;
            }

            Save();
        }

	    public void MoveRight()
	    {
            foreach (Tile p in Platforms)
            {
                p.x++;
            }
        
            Save();
        }

	    public void MoveLeft()
	    {
            foreach (Tile p in Platforms)
            {
                p.x--;
            }

            Save();
        }




	}
}