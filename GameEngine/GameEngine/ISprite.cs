using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public interface ISprite
    {
        bool GetMirrored();
        Texture2D GetTexture();
        DRectangle GetSourceRect();
        DRectangle GetDestRect();
        int GetSpriteWidth();
        int GetSpriteHeight();
        int GetCurrentFrame();
        int GetNumFrames();
    }
}
