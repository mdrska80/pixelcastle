using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Castles.Conf;

namespace Castles
{
    /// <summary>
    /// Summary description for ScreenManager. Tahle trida slouzi ke zpracovani udalosti, ktere se dejou mezi prechody mezi obrazy.
    /// </summary>
    public class ScreenManager
    {
        public void GameOver()
        {
            // do game over stuff
            Game.I.Screen = Screens.gameOver;
            
            // Game.I.player
            // SaveScore...
        }

        public void ShowBonus()
        {
            Game.I.Screen = Screens.bonus;
        }
        
        public void StartNewGame()
        {
            Game.I.Screen = Screens.loading;

            Game.I.highScore = Score.LoadHighScore();

            //clear
        }

        public void StartLevel(int level)
        {
            Game.I.Screen = Screens.level;
            Game.I.level = Level.Load(level); ;
        }

        public void InitGame()
        {
            Game.I.Screen = Screens.menu;
        }

        public void ShowLevelStatistics()
        {
            Game.I.Screen = Screens.statistics;
        }

        public void StartEditor()
        {
            Game.I.Screen = Screens.editor;

            Game.I.editingPlatform = new IGPos(1, 1, 0);
            Game.I.level = Level.Load(CastlesConfigurationReader.GetConfiguration().Editor.Level); ;
        }
    }
}