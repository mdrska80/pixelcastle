using System.Collections.Generic;
using SFML.Window;
using Castles.Conf;
using SFML.Graphics;

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
        public InputManager inputManager;

        public Level level;
        public Player player;

        public Vector2i boardOrigin;
        public RenderWindow window;

        // what screen we do want
        public Screens Screen = Screens.editor;
        public bool isPaused = false;

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

        public void Init()
        {
            this.gameStatus = GameStatus.Preparing;
            this.resourceManager = new ResourceManager();
            this.screenManager = new ScreenManager();
            this.eventManager = new EventManager();
            this.inputManager = new InputManager();
            InitExperienceTable();

            resourceManager.Init(CastlesConfigurationReader.GetConfiguration().DataFolder);
            boardOrigin = new Vector2i(500, 200);

            screenManager.StartNewGame();

            // set initial screen
            screenManager.StartEditor();
        }

        private void InitExperienceTable()
        {
            Experience.expTable = new Dictionary<int, long>();

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

            GameView gameView = new GameView();
            gameView.CreateView(this);
            
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