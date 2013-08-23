using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace Castles
{
    public class Theme
    {

        public Theme()
        {
            platforms = new List<SP>();
            r = new Random();
        }

        /// <summary>
        /// Surfaces we can use in theme
        /// </summary>
        public List<SP> platforms { get; set; }

        /// <summary>
        /// Theme name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Tile for pit platform
        /// </summary>
        public Surface PitPlatform { get; set; }

        /// <summary>
        /// Tile for pit platform
        /// </summary>
        public Surface TeleportPlatform { get; set; }  

        /// <summary>
        /// Pressure platform
        /// </summary>
        public Surface PressurePlatePlatform {get;set;}      

        /// <summary>
        /// Platforma pres kterou nelze prejit.
        /// </summary>
        public Surface BlockPlatform { get; set; }

        private Random r { get; set; }

        public Surface GetRandomPlatform()
        {
            int value = r.Next(0, 100);
            foreach (SP platform in platforms)
            {
                if (platform.probability >= value)
                    return platform.surf;
            }

            return platforms[platforms.Count - 1].surf;
        }
    }

    public class SP
    {
        public Surface surf { get; set; }
        public int probability { get; set; }
    }

}
