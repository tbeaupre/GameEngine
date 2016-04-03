using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class DRectangle
    {
        public int Height;
        public int Width;
        public double X;
        public double Y;

        public DRectangle(double X, double Y, int Width, int Height)
        {
            this.X = X;
            this.Y = Y;
            this.Width = Width;
            this.Height = Height;
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle((int)X, (int)Y, Width, Height);
        }
    }
}
