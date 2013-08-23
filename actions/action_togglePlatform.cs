using Castles.Tools;

namespace Castles
{
    public partial class Action
    {
        public void Execute_togglePlatform(Platform p, Entity e)
        {
            if (IsActive)
            {
                // target is in parameters
                // Param1 = x
                // Param2 = y
                // Param3 = layer

                // does the platform already exists?
                Platform px = Game.I.level.GetPlatform(Param1.ToInt(), Param2.ToInt(), Param3.ToInt());

                if (px != null)
                {
                    Execute_createPlatform(p, e);
                }
                else
                {
                    Execute_destroyPlatform(p,e);
                }
            }
        }
    }
}
