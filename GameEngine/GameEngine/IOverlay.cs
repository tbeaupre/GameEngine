using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public interface IOverlay : ISprite
    {
        int GetXOffset();

        int GetYOffset();

        void Update();
    }
}
