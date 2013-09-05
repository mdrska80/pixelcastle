using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace SFML.Utils
{
    public class SpriteAnimatedEx : Drawable
    {
        public SpriteAnimated CurrentSprite { get; set; }

        private string ca = "";
        public string CurrentAnimation
        {
            get { return ca; }
            set 
            {
                if (Animations.ContainsKey(value))
                {
                    //reset current animation.
                    if (CurrentSprite!=null)
                        CurrentSprite.Reset();

                    //set new animation
                    ca = value;
                    CurrentSprite = Animations[value];
                }
            }
        }
        public Dictionary<string, SpriteAnimated> Animations { get; set; }

        public SpriteAnimatedEx()
        {
            Animations = new Dictionary<string, SpriteAnimated>();
        }

        public void AddAnimatedSprite(string code, SpriteAnimated s)
        {
            if (s != null)
            {
                if (!Animations.ContainsKey(code))
                {
                    Animations.Add(code, s);
                }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (CurrentSprite!=null)
                target.Draw(CurrentSprite);
        }
    }
}
