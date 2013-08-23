using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Castles.Conf;
using Castles.Tools;
using Castles.gameObjects;

namespace Castles
{
	/// <summary>
	/// Summary description for Game.
	/// </summary>
	public class Score
	{
		/// <summary>
		/// Scores on all boards
		/// </summary>
		public List<Scrn> screens {get;set;}

        public string profile { get; set; }

	    private Score()
	    {
	    }

	    public Score(string profil)
	    {
	        profile = profil;
	    }

	    public void Save()
		{
			string path = Path.Combine(Game.I.resourceManager.gameDirectory, string.Format("score_{0}.xml", profile));
			Serializer<Score>.Serialize(path, this);			
		}

		public static Score Load(string profile)
		{
            string path = Path.Combine(Game.I.resourceManager.gameDirectory, string.Format("score_{0}.xml", profile));
			Score s = Serializer<Score>.Deserialize(path);
		    s.profile = profile;

			return s;		
		}

	    public static void ActualizeHighScore()
	    {
            //read all possible scores and create global superone names highscore.
	    }

	    public static Score LoadHighScore()
		{
			string path = Path.Combine(Game.I.resourceManager.gameDirectory, "highscore.xml");
			Score s = Serializer<Score>.Deserialize(path);	

            if (s==null) 
            {
                s = new Score("high");
                s.screens = new List<Scrn>();
            }

			return s;	
		}

		public void SaveHighScore()
		{
			string path = Path.Combine(Game.I.resourceManager.gameDirectory, "highscore.xml");
			Serializer<Score>.Serialize(path, this);	
		}

		public Scrn GetScrn(int level)
		{
			var x = (from i in screens
						where i.level == level
						select i).FirstOrDefault();

			if (x==null)
			{
				x = new Scrn();
				screens.Add(x);
			}

			return x;

		}

	}	

	public class Scrn
	{

		/// <summary>
		/// Amount
		/// </summary>
		public string author {get;set;}


		/// <summary>
		/// Amount
		/// </summary>
		[XmlAttribute]
		public int score {get;set;}

		/// <summary>
		/// Best amount for this profile
		/// </summary>
		[XmlAttribute]
		public int bestScore {get;set;}

		/// <summary>
		/// Level
		/// </summary>
		[XmlAttribute]
		public int level {get;set;}

		/// <summary>
		/// When
		/// </summary>
		[XmlAttribute]
		public DateTime dt {get;set;}
	}
}