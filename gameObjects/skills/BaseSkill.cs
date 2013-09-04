using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles
{
    public class BaseSkill : ISkill
    {
        /// <summary>
        /// Skill name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Code which is used in code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Description of the skill - visible ingame
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Current points for this particular skill
        /// </summary>
        public int Current { get; set; }

        /// <summary>
        /// Maximum points which player can have for now. Current can be lowered by something...
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Skill is not displayed in skill list
        /// </summary>
        public bool Hidden { get; set; }
    }
}
