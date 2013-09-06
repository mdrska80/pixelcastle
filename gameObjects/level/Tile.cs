using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

//map from tiles and also local data.
namespace Castles
{
    public class Tile 
	{
        /// <summary>
        /// Id for animated ones
        /// </summary>
		[XmlAttribute]
		public string code {get;set;}

        /// <summary>
        /// Id for non animated ones, if gid is not filled it means it was replaced
        /// by code which point to animated sprite...so use that animated sprite
        /// insead of.
        /// </summary>
        [XmlAttribute]
        public int gid { get; set; }

        [XmlAttribute]
		public int x {get;set;}

		[XmlAttribute]
		public int y {get;set;}

		[XmlAttribute]
		public TileType type {get;set;}

	    /// <summary>
		/// What is on the platform, definition
		/// </summary>
		/// <value>The contains.</value>
		[XmlAttribute]
		public string contains {get;set;}

		[XmlIgnore]
		public Item item {get;set;}

		// monster can pass this point
		[XmlAttribute]
		public bool noMonsterPass {get;set;}
		
		[XmlAttribute]
		public bool noPlayerPass {get;set;}

        [XmlAttribute]
        public bool isPit { get; set; }

		public int layer { get; set; }

		public Elevator elevator {get;set;}
		public Moving moving {get;set;}
		public Generator generator {get;set;}
		public BaseAction action {get;set;}
        public Door door { get; set; }

        [XmlIgnore]
		public bool isHighLighted { get; set; }

		[XmlIgnore] 
		public bool handled = false;

		[XmlIgnore]
		public Dictionary<string, int> pathfindingValues = new Dictionary<string, int>();

		public IGPos IGPos
		{
			get
			{
				return new IGPos(x,y,layer);
			}
		}

		public void Update()
		{
			if (!handled)
			{
				// do updates
				if (elevator != null)
					UpdateElevator(elevator);

				if (moving != null)
					UpdateMoving(moving);

				if (generator != null)
					UpdateGenerator(generator);

				Game.I.level.handledPlaforms.Add(this);			

			}
		}

		private int brake = 3;
		private int brakeCurrent = 0;

		private void UpdateMoving(Moving moving)
		{
			switch(moving.Direction)
			{
				case Direction.RIGHT:
					{
						brakeCurrent++;
						if (brakeCurrent >= brake)
							brakeCurrent = 0;
						else
							return;

						moving.Current.X++;

					if (moving.Current.X >= moving.To.X)
						moving.Direction = Direction.LEFT;

					this.x++;

					//move platform to different layer
					//MoveToLayer(layerid, layerid+1);
					//layerid++;
					handled = true;					
					break;
				}

				case Direction.LEFT:
				{
					brakeCurrent++;
					if (brakeCurrent >= brake)
						brakeCurrent = 0;
					else
						return;

					moving.Current.X--;

					if (moving.Current.X <= moving.From.X)
						moving.Direction = Direction.RIGHT;

					this.x--;

					//move platform to different layer
					//MoveToLayer(layerid, layerid+1);
					//layerid++;
					handled = true;					
					break;					
				}
			}

		}

		private void UpdateGenerator(Generator generator)
		{
			
		}

		private void UpdateElevator(Elevator elevator)
		{
			switch (elevator.Direction)
			{
				case Direction.UP:
					{
						int elevatorPositionOld = elevator.Current++;

						if (elevator.Current >= elevator.High)
							elevator.Direction = Direction.DOWN;

					    if (Game.I.player != null)
					    {
					        if (Game.I.player.position.X == x &&
					            Game.I.player.position.Y == y &&
                                Game.I.player.position.Layer == layer + elevatorPositionOld)
					        {
                                Game.I.player.position.Layer = layer + elevator.Current;
					        }
					    }

					    //move platform to different layer
						//MoveToLayer(layerid, layerid+1);

						//entity present?



						//layerid++;
						handled = true;

						break;
					}

				case Direction.DOWN:
					{
						elevator.Current--;

						if (elevator.Current <= elevator.Low)
							elevator.Direction = Direction.UP;

						//entity present?
					    if (Game.I.player != null)
					    {
					        if (Game.I.player.position.X == x &&
					            Game.I.player.position.Y == y &&
					            Game.I.player.position.Layer == layer+(elevator.Current+1))
					        {
                                Game.I.player.position.Layer = layer + elevator.Current;
					        }
					    }

					    //move platform to different layer
						//MoveToLayer(layerid, layerid-1);



						handled = true;


						break;
					}
			}
		}

		public override string ToString()
		{
			return string.Format("X:{0}, Y:{1}",x,y);
		}

		public int GetPathFindingValue(string entity)
		{
			if (pathfindingValues.ContainsKey(entity))
				return pathfindingValues[entity];

			return -1;
		}
    }
}