using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles.actions
{
    public partial class Action
    {
        /// <summary>
        /// What direction we want to jump?
        /// </summary>
        public Direction direction { get; set; }

        public void Execute_jump(Platform p, Entity e)
        {
            //jump = < SPACE >
            if (p != null)
            {
                //calculate target platform

                //copy coordinates from target plaform to player location.
            }

        }
    }
}
