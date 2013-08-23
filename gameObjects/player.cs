using System;
using System.Collections.Generic;
using Castles.Conf;
using Castles.gameObjects;
using SdlDotNet.Graphics.Sprites;

namespace Castles
{
	/// <summary>
	/// Summary description for Game.
	/// </summary>
	public class Player : Entity
	{
		public Score score { get; set; }

		public DateTime dtImmortalStart {get;set;}

		public bool isImmortal
		{
			get
			{
				return (DateTime.Now-dtImmortalStart).TotalSeconds <= 5;
			}
		}

		public void MakeImmortal()
		{
			dtImmortalStart = DateTime.Now;
		}
		
		// just count how many extra lives has been given.
		private int extraLivesGiven = 0;

		public Player()
			: base()
		{
			// by default we will have 3 lives
			lives = 3;
			score = new Score(CastlesConfigurationReader.GetConfiguration().ActiveProfile);
            score.screens = new List<Scrn>();
			pickedGems = 0;

			AnimationCollection aCollUp = new AnimationCollection();
			aCollUp.Add(Game.I.resourceManager.Cache_sprite["player_walk_right_up"], 150);

			AnimationCollection aCollDown = new AnimationCollection();
			aCollDown.Add(Game.I.resourceManager.Cache_sprite["player_walk_left_down"], 150);

			AnimationCollection aCollRight = new AnimationCollection();
			aCollRight.Add(Game.I.resourceManager.Cache_sprite["player_walk_right_down"], 150);

			AnimationCollection aCollLeft = new AnimationCollection();
			aCollLeft.Add(Game.I.resourceManager.Cache_sprite["player_walk_left_up"], 150);

			// add animation to sprite.
			sprite.Animations.Add("StandingStill_up", aCollUp);
			sprite.Animations.Add("StandingStill_down", aCollDown);
			sprite.Animations.Add("StandingStill_right", aCollRight);
			sprite.Animations.Add("StandingStill_left", aCollLeft);

			sprite.CurrentAnimation = "StandingStill_up";
			sprite.Animate = true;            
		}

		

		public override void Update()
		{
			// comment
			
			//no movement....
			//ClearHighlightedPath(highlightedPlatforms);
			//highlightedPlatforms = HighlightPath(position, new IGPos(0, 0, 0));
		}

		public override Platform Move(Direction dir)
		{
			// na jakou plaformu chci vkrocit?
			Platform p = base.Move(dir);

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

			sprite.CurrentAnimation = "StandingStill_"+dir.ToString();

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

		public override bool Pickup (Platform p)
		{
			// At the beginning of every maze, gems are worth 1 point each; this value increases by 1 
			// for every gem Bentley picks up, to a maximum of 99. Each maze also randomly includes a 
			// hat or honey pot, which serve the dual purpose of awarding points and providing Bentley 
			// with the ability to defeat specific enemies. 

			int pgOrig = pickedGems;

			//handle cauldron
			if (p.item is HoneyCauldron)
			{
				//The honey pot is worth 1,000 points, and picking 
				// it up can delay the landing of a swarm of bees.
				if (p.item.TryToPickup())
				{
					IncreaseScore(CastlesConfigurationReader.GetConfiguration().Bonuses.cauldron);
					Game.I.level.HoneyCauldron = null;
					p.item = null;
				}
			}  

			// handle magic cap
			if (p.item is MagicCap)
			{
				// The hat is worth 500 points and will make 
				// Bentley invulnerable for a few seconds and allow him to eliminate Berthilda the witch, 
				// who appears in the last maze of each level.
				if (p.item.TryToPickup())
				{
					IncreaseScore(CastlesConfigurationReader.GetConfiguration().Bonuses.magichat);
					Game.I.level.HoneyCauldron = null;
					p.item = null;
				}
			}
			
			if (p.item is Booobak_item)
			{
				if (p.item.TryToPickup())
				{
					if (!Game.I.level.Booobak.isUltra)
						IncreaseScore(CastlesConfigurationReader.GetConfiguration().Bonuses.booobak);
					else
						IncreaseScore(CastlesConfigurationReader.GetConfiguration().Bonuses.booobakUltra);

					Game.I.level.Booobak = null;
					p.item = null;
				}
			}

			//freeze item....for several seconds...
//            if (p.item is Freeze_item)
  //          {
	//            IncreaseScore(CastlesConfigurationReader.GetConfiguration().Bonuses.booobak);
	  //          p.item.TryToPickup();
		//        Game.I.level.Booobak = null;
		  //      p.item = null;
			//}

			bool lastgem = base.Pickup(p);

			//play sound according to level player is on right now

			//calculate score
			if (pgOrig!=pickedGems)
				IncreaseScore(GetGemScore());

			if (lastgem)
			{
				int bonus = Common.CalculateLastGemBonus(Game.I.level.level);
				IncreaseScore(bonus);

				Game.I.screenManager.ShowLevelStatistics();
			}

			return lastgem;
		}

		public void IncreaseLive()
		{
			++lives;

			// play sound...
		}

		public int GetGemScore()
		{
			// At the beginning of every maze, gems are worth 1 point each; this value increases by 1 
			// for every gem Bentley picks up, to a maximum of 99.
			int s = pickedGems;
			if (s>=99) s = 99;
			return s;
		}

		public void IncreaseScore(int plusScore)
		{
			// FXL has told me how the time bonus in the game is figured. 
			// For every four seconds of game play, you lose 1000 points. 
			// The amount of time bonus starts at 200,000, so you can figure
			// that a 5 minute game will have a time bonus of 125,000. 
			// Also, for every life you lose, you lose at least 1000 time bonus points, 
			// so even though you can make up for your life as far as the life bonus, 
			// you still lose in time bonus.
			int extraLifein = CastlesConfigurationReader.GetConfiguration().Bonuses.extraLifein;
            Scrn s = score.GetScrn(Game.I.level.level);


			//every 60.000 give extra live...
			int howManyExtraLivesShouldPLayerHaveBefore = s.score % extraLifein;

			s.score += plusScore;

            int howManyExtraLivesShouldPLayerHaveAfter = s.score % extraLifein;

			if (howManyExtraLivesShouldPLayerHaveBefore!=howManyExtraLivesShouldPLayerHaveAfter)
			{
				//Game.I.screenManager.ShowBonus();

				IncreaseLive();
				extraLivesGiven++;

			}
		}
	}	
}