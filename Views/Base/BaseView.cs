using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace Castles
{
    public interface IView
    {
        void UpdateView(Surface surf);
    }

    public class BaseView : IView
    {
        protected Point origin { get; set; }

        public BaseView(Point origin)
        {
            this.origin = origin;
        }

        public virtual void UpdateView(Surface surf)
        {
        }

        protected Point OP(int X, int Y)
        {
            return new Point(X+origin.X, Y+origin.Y);
        }

    }
}
