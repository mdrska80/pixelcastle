namespace Castles
{
    /// <summary>
    /// 
    /// </summary>
    public enum GameStatus
    {
        /// <summary>
        /// 
        /// </summary>
        Preparing,
        /// <summary>
        /// 
        /// </summary>
        Started,
        /// <summary>
        /// 
        /// </summary>
        Running,
        /// <summary>
        /// 
        /// </summary>
        Paused,
        /// <summary>
        /// 
        /// </summary>
        Stopped
    }

    public enum Screens
    {
        menu,           // main menu, where u can choose various options.
        game,           // main game screen, most of the time will be spent here.
        score,          // high score screen
        editor,         // editor screen, some debug info is visible.
        gameOver,       // game over screen
        bonus,          // bonus during gme
        statistics,      // level statistics.
        level,
        loading,
        pause           // game is paused
    }

    public enum EditorObjects
    {
        platforms,
        items,
        monsters,
        gems,
        box,
        cauldron,
		barrel,
        bbk
    }

    public enum Direction
    {
        UP = 1,
        DOWN = 2,
        RIGHT = 3,
        LEFT = 4
    }

    public enum EntityType
    {
        Player,
        TreeFeeding,
        TreeTough,
        TreeDead,
        Bees,
        Skeleton,
        Berthilda,
        CrystalBall,
        TestMob,
        Thuja,
        Snowman,
        Booobak

        // posible new monsters
    }    

    public enum TileType
    {
        // Column, from point where detected - down
        Column,

        // No other additional platforms
        Platform,

        // Custom GFX, for now equivalent to platform...
        Custom
    }

    public enum MonsterSpeed
    {
        SuperFast = 5,
        Fast = 10,
        Normal = 15,
        Slow = 25,
        VerySlow = 75,
        Static = 999
    }

    public enum WallItemPosition
    {
        left  = 1,
        up = 2
    }
}