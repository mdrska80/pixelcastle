using Castles.Conf;
using Castles.gameObjects.skills;
using SFML.Graphics;
using SFML.Utils;

namespace Castles
{
	/// <summary>
	/// Summary description for Game.
	/// </summary>
	public class Player : Entity
	{
        public override void InitGfx()
        {
            base.InitGfx();

            SpriteAnimated sa = new SpriteAnimated(Game.I.resourceManager.GetTexture("PLAYER"), 48, 64, 10, Game.I.window, RenderStates.Default,0,3,true);
            Sprite.AddAnimatedSprite("MAIN", sa);
            Sprite.CurrentAnimation = "MAIN";
        }

        public override void AssignSkills()
        {
            //player can walk...
            Skills.Add(new Skill_Walk());
        }

		public override void Update()
		{
			// comment
			
			//no movement....
			//ClearHighlightedPath(highlightedPlatforms);
			//highlightedPlatforms = HighlightPath(position, new IGPos(0, 0, 0));
		}

		public override Tile Move(Direction dir)
		{
			// na jakou plaformu chci vkrocit?
			Tile p = base.Move(dir);

			// existuje takova plarforma?
			if (p != null)
			{
				Monster m = Level.GetMonsterOnPlatform(p.x, p.y, p.layer);

				//do i step on monster?
				if (m != null)
				{
					bool isKilled = m.TryToKill(this);

					if (!isKilled)
					{
						//death from monster
						Die();
					}
				}
			}

			Sprite.CurrentAnimation = "WALK_"+dir;

			return p;
		}

		public override void Die()
		{
		    if (!CastlesConfigurationReader.GetConfiguration().Immortal)
		    {
		        base.Die();

		        // play sound...

		        if (lives == 0)
		            Game.I.screenManager.GameOver();
		        else
		        {
		            Game.I.level.PutMonstersToOriginalPositions();
		        }
		    }
		}

		public override bool Pickup (Tile p)
		{
		    return true;
		}

		public void IncreaseLive()
		{
			++lives;

			// play sound...
		}
	}	
}