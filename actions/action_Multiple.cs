using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles.actions
{
    public class action_Multiple : BaseAction
    {
        public List<BaseAction> actions { get; set; }

        public override void Execute(Tile p, Entity e)
        {
            foreach (BaseAction ba in actions)
            {
                ba.Execute(p,e);
            }
        }
    }
}
