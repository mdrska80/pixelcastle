using Castles.Tools;

namespace Castles
{
    public class Action_TogglePlatform : BaseAction
    {
        public override void Execute(Tile p, Entity e)
        {
            if (IsActive)
            {
                // target is in parameters
                // Param1 = x
                // Param2 = y
                // Param3 = layer

                // does the platform already exists?
                Tile px = Game.I.level.GetPlatform(Param1.ToInt(), Param2.ToInt(), Param3.ToInt());

                if (px != null)
                {
                    new Action_CreatePlatform().Execute(p, e);
                }
                else
                {
                    new Action_DestroyPlatform().Execute(p, e);
                }
            }
        }
    }
}
