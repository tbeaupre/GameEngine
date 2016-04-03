using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public interface IObject
    {
        void Update();
        Texture2D GetTexture();
        DRectangle GetSourceRect();
        DRectangle GetDestRect();
        int GetSpriteWidth();
        int GetSpriteHeight();
        int GetCurrentFrame();
        int GetNumFrames();
        double GetVelX();
        double GetVelY();
        void SetVelocity(Nullable<double> x, Nullable<double> y);
        void Delete();
        void SetCurrentFrame(int frameNum);
        void ChangeVelocity(double x, double y);
        void ChangeWorldCoords(double x, double y, bool collisions);
    }
}
