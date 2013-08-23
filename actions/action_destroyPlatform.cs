using Castles.Tools;

namespace Castles
{
	public partial class Action
	{
		public void Execute_destroyPlatform(Platform p, Entity e)
		{
			if (IsActive)
			{
				// target is in parameters
				// Param1 = x
				// Param2 = y
				// Param3 = layer

			    Platform px = Game.I.level.GetPlatform(Param1.ToInt(), Param2.ToInt(), Param3.ToInt());
                Game.I.level.RemovePlatform(px);

				if (IsOneTimeAction)
				{
					IsActive = false;
				}
			}
		}
	}
}
