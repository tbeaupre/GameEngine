using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class Character : Sprite
    {
        double moveSpeed = 5;
        public Character(double x, double y) :
            base(Allegiance.Ally, TextureLibrary.Get().GetTexture("Spaceman Body"), x, y, 1, 13)
        {
        }

        public override void Update()
        {
            HandleKeys();
        }

        private void HandleKeys()
        {
            if (KeyHandler.Get().GetNewKeys().IsKeyDown(Keys.Left))
            {
                this.ChangeWorldCoords(-moveSpeed, 0);
            }
            if (KeyHandler.Get().GetNewKeys().IsKeyDown(Keys.Right))
            {
                this.ChangeWorldCoords(moveSpeed, 0);
            }
        }
        
        public override void ChangeWorldCoords(double x, double y)
        {
            for (int i = 0; i < Math.Abs(x); i++)
            {
                if (!CollisionDetector.MapCollisionDetect(this, Math.Sign(x), 0))
                {
                    Map.Get().ChangeX(-Math.Sign(x));
                    base.ChangeWorldCoords(Math.Sign(x), 0);
                }
                else break;
            }
            for (int i = 0; i < Math.Abs(y); i++)
            {
                if (!CollisionDetector.MapCollisionDetect(this, 0, Math.Sign(y)))
                {
                    Map.Get().ChangeY(-Math.Sign(y));
                    base.ChangeWorldCoords(0, Math.Sign(y));
                }
                else break;
            }
        }
    }
}
