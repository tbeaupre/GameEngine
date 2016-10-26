using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class Character : Object
    {
        CharacterData data = new CharacterData();
        bool animate = true;
        int frameTimer = 0;
        int jumpNum = 0;
        Nullable<int> queuedFrame = null;
        bool airDodging = false;
        int airDodgeTimer = 0;

        public Character(double x, double y) :
            base(Allegiance.Ally, TextureLibrary.Get().GetTexture("Spaceman Just Body"), x, y, 1, 13)
        {
            this.AddOverlay(new ActiveOverlay(this,
                0, // startFrame
                -2, // xOffset
                0,
                new int[] { 0, 0, 0, 1, 0, 0, 0, 1, 2, 1, 0, 0, 1 }, // xOffsets
                new int[] { 4, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1, 1 }, // yOffsets
                3, "Spaceman Heads")); // Adds the Helmet
            this.AddOverlay(new PassiveOverlay(this,
                null,
                -2, // xOffset
                0,
                null,
                null,
                13, "Spaceman Legs")); // Adds the spaceman's legs
        }

        public override void Update()
        {
            IterateAirDodge();
            AnimationCheck();
            HandleKeys();
            JumpCheck();
            base.Update();
        }

        private void HandleKeys()
        {
            if (!airDodging)
            {
                KeyHandler keys = KeyHandler.Get();
                if (keys.IsKeyHeld(Buttons.Left))
                {
                    this.ChangeWorldCoords(-data.GetMoveSpeed(), 0, true);
                    SetMirrored(true);
                }
                if (keys.IsKeyHeld(Buttons.Right))
                {
                    this.ChangeWorldCoords(data.GetMoveSpeed(), 0, true);
                    SetMirrored(false);
                }
                if (keys.IsKeyPressed(Buttons.Jump) && jumpNum < data.GetNumJumps())
                {
                    jumpNum++;
                    this.SetVelocity(null, data.GetJumpHeight());
                }
                if (keys.IsKeyPressed(Buttons.Fire))
                {
                    SpriteLibrary.Get().AddSprite(Allegiance.Environment, new Explosion(GetWorldX(), GetWorldY(), 2, false));
                }
                if (keys.IsKeyPressed(Buttons.Dodge) && !CollisionDetector.MapCollisionDetect(this, 0, 1))
                {
                    AirDodge(keys);
                }
            }
        }

        public void AirDodge(KeyHandler keys)
        {
            base.SetCurrentFrame(5);
            this.SetVelocity(keys.GetNewPad().ThumbSticks.Left.X * data.GetAirDodgeLength(), keys.GetNewPad().ThumbSticks.Left.Y * data.GetAirDodgeLength());
            this.airDodging = true;
            this.airDodgeTimer = data.GetAirDodgeTimer();
            this.jumpNum = data.GetNumJumps() - 1;
            SetGravityFactor(0);
        }

        public void IterateAirDodge()
        {
            this.airDodgeTimer--;
            if (this.airDodgeTimer == data.GetAirDodgeTimer() / 4)
            {
                SetVelocity(0, 0);
            }
            if (this.airDodgeTimer == 0)
            {
                SetCurrentFrame(1);
                SetGravityFactor(1);
            }
        }

        public void JumpCheck()
        {
            if (!CollisionDetector.MapCollisionDetect(this,0,1) && jumpNum == 0)
            {
                jumpNum++;
            }
        }

        public override void HitFloor()
        {
            airDodging = false;
            jumpNum = 0;
            base.HitFloor();
        }

        // Since this sprite only changes frames every so often, there is an animation check to see if you can change the frame.
        public void AnimationCheck()
        {
            frameTimer++;
            if (frameTimer == data.GetAnimationTimer()) frameTimer = 0;
            if (frameTimer == 0)
            {
                animate = true;
                if (queuedFrame != null)
                {
                    SetCurrentFrame((int)queuedFrame);
                    queuedFrame = null;
                }
            }
            else animate = false;
        }

        public void IterateFrame()
        {
            SetCurrentFrame(GetCurrentFrame() + 1);
            if (GetCurrentFrame() == GetNumFrames())
            {
                SetCurrentFrame(0);
            }
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

        public override void SetCurrentFrame(int frameNum)
        {
            if (animate) base.SetCurrentFrame(frameNum);
            else queuedFrame = frameNum;
        }
    }
}
