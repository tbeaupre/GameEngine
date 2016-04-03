using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public interface IObject: ISprite
    {
        bool GetMirrored();
        void Update();
        double GetVelX();
        double GetVelY();
        void SetVelocity(Nullable<double> x, Nullable<double> y);
        void Delete();
        void SetMirrored(bool val);
        void SetCurrentFrame(int frameNum);
        void ChangeVelocity(double x, double y);
        void ChangeWorldCoords(double x, double y, bool collisions);
    }
}
