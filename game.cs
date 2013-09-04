using System.Collections.Generic;
using Castles.Conf;
using System.Drawing;
using SdlDotNet.Core;

namespace Castles
{
    /// <summary>
    /// Summary description for Game.
    /// </summary>
    public class Game
    {
        public GameStatus gameStatus;
        public ResourceManager resourceManager;
        public ScreenManager screenManager;
        public EventManager eventManager;
        public Level level;
        public Player player;
        public Score highScore;

        public Point boardOrigin;

        // what screen we do want
        public Screens Screen = Screens.editor;
        public bool isPaused = false;

        /// <summary>
        /// Themes collection
        /// </summary>
        public Dictionary<string, Theme> themes { get; set; }

        #region editing_stuff
            //editor, why -57? ... :)
            //public int editingLayer = -57;
            public IGPos editingPlatform = null;
            // what am I currently editing?
            public EditorObjects editingObject = EditorObjects.platforms; 
        #endregion


        // statistics
        public int surfaces = 0;

        private static Game _i;
        public static Game I
        {
            get
            {
                if (_i==null)
                    _i=new Game();

                return _i;
            }
        }

        public void Init(ResourceManager resourceManager)
        {
            this.gameStatus = GameStatus.Preparing;
            this.resourceManager = resourceManager;
            this.screenManager = new ScreenManager();
            this.eventManager = new EventManager();
            InitExperienceTable();

            themes = new Dictionary<string, Theme>();

            resourceManager.Init(CastlesConfigurationReader.GetConfiguration().DataFolder);
            boardOrigin = new Point(500, 200);

            screenManager.StartNewGame();

            // set initial screen
            screenManager.StartEditor();
        }

        private void InitExperienceTable()
        {
            Experience.expTable.Add(1, 100);
            Experience.expTable.Add(2, 500);
            Experience.expTable.Add(3, 1500);
        }

        /// <summary>
        /// 
        /// </summary>
        private Game()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public void Start()
        {

            GameView gameView = new GameView(resourceManager);
            gameView.CreateView(this);

            //set up keyboard repeat... :)
            //SdlDotNet.Input.Keyboard.EnableKeyRepeat(100,30);
            
            //Event
            // Music music = new Music(Path.Combine(filePath, Path.Combine(dataDirectory, "fard-two.ogg")));

            // try
            // {
            //     music.Play(-1);
            // }
            // catch (DivideByZeroException)
            // {
            //     // Linux audio problem
            // }

            this.gameStatus = GameStatus.Started;
        }


        public void Pause()
        {
            isPaused = true;
            Screen = Screens.pause;
        }

        public void UnPause()
        {
            isPaused = false;

            //this shoud be previous screen
            Screen = Screens.game;
        }
    }


}