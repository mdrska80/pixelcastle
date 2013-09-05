using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Castles
{
    public static class FPS
    {
        public static int Value { get; set; }

        /// <summary>
        /// rozdil casu v mikrosekundach
        /// </summary>
        public static double Delta { get; set; }

        /// <summary>
        /// rozdil casu v milisekundach
        /// </summary>
        public static double SemiDelta
        {
            get { return Delta/1000; }
        }

    }
}
