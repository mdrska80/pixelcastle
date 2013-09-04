using SFML.Graphics;
using SFML.Window;

namespace Castles
{
    public interface IView
    {
        void UpdateView(RenderWindow surf);
    }

    public class BaseView : IView
    {
        //protected Point origin { get; set; }
        protected Vector2i origin { get; set; }

        public BaseView(Vector2i origin)
        {
            this.origin = origin;
        }

        public virtual void UpdateView(RenderWindow window)
        {
        }

        /// <summary>
        /// Shift out new vector relative to origin
        /// </summary>
        /// <param name="X">X</param>
        /// <param name="Y">Y</param>
        /// <returns>Shifted vector</returns>
        protected Vector2i OP(int X, int Y)
        {
            return new Vector2i(X+origin.X, Y+origin.Y);
        }
    }
}
