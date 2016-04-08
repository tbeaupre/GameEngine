using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public class Explosion : Object
    {
        int frameOffset = 5;
        int frameTimer = 0;
        int size = 0;
        bool gravity = false;
        Random rnd;
        int spread = 3;

        public Explosion(double x, double y, int size, bool gravity)
            :base(Allegiance.Environment, TextureLibrary.Get().GetTexture("Explosion"), x, y, 0, 7)
        {
            this.size = size;
            this.gravity = gravity;
            SetGravityFactor(.5);
            this.rnd = new Random();
            spread = 4 + (int)Math.Pow(size, 3);
        }

        public Explosion(double x, double y, int size, bool gravity, Random rnd)
            : base(Allegiance.Environment, TextureLibrary.Get().GetTexture("Explosion"), x, y, 0, 7)
        {
            this.rnd = rnd;
            this.size = size;
            this.gravity = gravity;
            SetGravityFactor(.5);
            this.rnd = new Random();
            spread = 4 + (int)Math.Pow(size, 3);
        }

        public void HandleSize()
        {
            if (size > 0)
            {
                if (GetCurrentFrame() == 1)
                {
                    size--;
                    CreateExplosionPlus(size);
                }
            }
        }

        public void CreateExplosionPlus(int size)
        {
                CreateExplosion(GetSpriteWidth() + rnd.Next(-spread, spread), rnd.Next(-spread, spread));
                CreateExplosion(-GetSpriteWidth() + rnd.Next(-spread, spread), rnd.Next(-spread, spread));
                CreateExplosion(rnd.Next(-spread, spread), GetSpriteHeight() + rnd.Next(-spread, spread));
                CreateExplosion(rnd.Next(-spread, spread), -GetSpriteHeight() + rnd.Next(-spread, spread));
        }

        public void CreateExplosion(double xOffset, double yOffset)
        {
            SpriteLibrary.Get().AddSprite(Allegiance.Environment,
                new Explosion(GetWorldX() + xOffset,
                            GetWorldY() + yOffset,
                            this.size, this.gravity, this.rnd));
        }

        public void IterateFrame()
        {
            IterateFrameTimer();
            if (frameTimer == 0)
            {
                if (GetCurrentFrame() < (GetNumFrames() - 1))
                    SetCurrentFrame(GetCurrentFrame() + 1);
                else
                    this.Delete();
            }
        }

        public void IterateFrameTimer()
        {
            this.frameTimer++;
            if (frameTimer == frameOffset) this.frameTimer = 0;
        }

        public override void Update()
        {
            IterateFrame();
            HandleSize();
            if (gravity) base.Update();
        }
    }
}
