using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using SdlDotNet.Graphics;

namespace Castles
{
    public class backgroundView : BaseView
    {
        public backgroundView(Point origin) : base(origin)
        {
        }

        public override void UpdateView(Surface surf)
        {
			surf.Fill(Color.FromArgb(30,32,37));
        }

    }
}
