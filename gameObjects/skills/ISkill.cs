using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles
{
    public interface ISkill
    {
        string Name { get; set; }
        string Code { get; set; }
        string Description { get; set; }
        int Current { get; set; }
        int Max { get; set; }
        bool Hidden { get; set; }
    }
}
