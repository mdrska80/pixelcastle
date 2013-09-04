using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles
{
    public class Experience
    {
        public static Dictionary<int, long> expTable { get; set; }

        public int Current { get; set; }

        public int Level
        {
            get
            {
                foreach (KeyValuePair<int, long> keyValuePair in expTable)
                {
                    if (keyValuePair.Value > Current) return (keyValuePair.Key-1);
                }

                return 0;
            }
        }

        public override string ToString()
        {
            return string.Format("Exp: {0:### ### ### ###} -> Level: {1}",Current, Level);
        }
    }
}
