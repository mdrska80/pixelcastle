using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles
{
    public class WallItem : Item, IToogable
	{
        public WallItemPosition wipos { get; set; }

        public virtual void Toogle()
        {
            
        }
    }
}
