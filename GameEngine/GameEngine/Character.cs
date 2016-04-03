using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class Character : Object
    {
        CharacterData data = new CharacterData();

        int jumpNum = 0;

        public Character(double x, double y) :
            base(Allegiance.Ally, TextureLibrary.Get().GetTexture("Spaceman Body"), x, y, 1, 13)
        {
        }

        public override void Update()
        {
            HandleKeys();
            base.Update();
        }

        private void HandleKeys()
        {
            KeyboardState newKeys = KeyHandler.Get().GetNewKeys();
            KeyboardState oldKeys = KeyHandler.Get().GetOldKeys();
            if (newKeys.IsKeyDown(Keys.Left))
            {
                this.ChangeWorldCoords(-data.GetMoveSpeed(), 0, true);
                SetMirrored(true);
            }
            if (newKeys.IsKeyDown(Keys.Right))
            {
                this.ChangeWorldCoords(data.GetMoveSpeed(), 0, true);
                SetMirrored(false);
            }
            if (newKeys.IsKeyDown(Keys.Z) && oldKeys.IsKeyUp(Keys.Z) && jumpNum < data.GetNumJumps())
            {
                jumpNum++;
                this.SetVelocity(null, data.GetJumpHeight());
            }
        }

        public override void HitFloor()
        {
            jumpNum = 0;
            base.HitFloor();
        }

        public override void ChangeWorldCoords(double x, double y, bool collision)
        {
            if (collision)
            {
                for (int i = 0; i < Math.Abs(x); i++)
                {
                    if (!CollisionDetector.MapCollisionDetect(this, Math.Sign(x), 0))
                    {
                        Map.Get().ChangeX(-Math.Sign(x));
                        base.ChangeWorldCoords(Math.Sign(x), 0, false);
                    }
                    else break;
                }
                for (int i = 0; i < Math.Abs(y); i++)
                {
                    if (!CollisionDetector.MapCollisionDetect(this, 0, Math.Sign(y)))
                    {
                        Map.Get().ChangeY(-Math.Sign(y));
                        base.ChangeWorldCoords(0, Math.Sign(y), false);
                    }
                    else
                    {
                        if (Math.Sign(y) > 0) HitFloor();
                        else HitCeiling();
                        break;
                    }
                }
            }
            else
            {
                base.ChangeWorldCoords(x, y, false);
            }
        }
    }
}
