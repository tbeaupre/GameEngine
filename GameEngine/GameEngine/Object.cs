using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public abstract class Object : IObject
    {
        List<IOverlay> overlays = new List<IOverlay>();
        Allegiance allegiance;
        Texture2D texture;
        int currentFrame;
        int numFrames;
        double worldX = 0;
        double worldY = 0;
        double velX = 0;
        double velY = 0;
        bool mirrored = false;

        public Object(Allegiance allegiance, Texture2D texture, double x, double y, int currentFrame, int numFrames)
        {
            this.allegiance = allegiance;
            this.texture = texture;
            this.worldX = x;
            this.worldY = y;
            this.currentFrame = currentFrame;
            this.numFrames = numFrames;
        }

        public void AddOverlay(IOverlay overlay)
        {
            this.overlays.Add(overlay);
        }

        public List<IOverlay> GetOverlays()
        {
            return this.overlays;
        }

        public virtual void ChangeVelocity(double x, double y)
        {
            velX += x;
            velY += y;
        }

        public void SetVelocity(Nullable<double> x, Nullable<double> y)
        {
            if (x != null)
            {
                velX = (double)x;
            }
            if (y != null)
            {
                velY = (double)y;
            }
        }

        public virtual void ChangeWorldCoords(double x, double y, bool collisions)
        {
            if (collisions)
            {
                for (int i = 0; i < Math.Abs(x); i++)
                {
                    if (!CollisionDetector.MapCollisionDetect(this, Math.Sign(x), 0))
                    {
                        worldX += Math.Sign(x);
                    }
                    else break;
                }
                for (int i = 0; i < Math.Abs(y); i++)
                {
                    if (!CollisionDetector.MapCollisionDetect(this, 0, Math.Sign(y)))
                    {
                        worldY += Math.Sign(y);
                    }
                    else break;
                }
            }
            else
            {
                worldX += x;
                worldY += y;
            }
        }

        public virtual void Delete()
        {
            SpriteLibrary.Get().DeleteSprite(allegiance, this);
        }

        public int GetCurrentFrame()
        {
            return currentFrame;
        }

        public DRectangle GetDestRect()
        {
            return new DRectangle(worldX + Map.Get().GetX(), worldY + Map.Get().GetY(), texture.Width / numFrames, texture.Height);
        }

        public bool GetMirrored()
        {
            return this.mirrored;
        }

        public int GetNumFrames()
        {
            return numFrames;
        }

        public DRectangle GetSourceRect()
        {
            return new DRectangle(GetSpriteWidth() * currentFrame, 0, GetSpriteWidth(), texture.Height);
        }

        public int GetSpriteHeight()
        {
            return texture.Height;
        }

        public int GetSpriteWidth()
        {
            return texture.Width / numFrames;
        }

        public Texture2D GetTexture()
        {
            return this.texture;
        }

        public void SetAllegiance(Allegiance allegiance)
        {
            this.allegiance = allegiance;
        }

        public virtual void SetCurrentFrame(int frameNum)
        {
            this.currentFrame = frameNum;
        }

        public void SetMirrored(bool val)
        {
            this.mirrored = val;
        }

        public void SetNumFrames(int i)
        {
            this.numFrames = i;
        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public virtual void Update()
        {
            CeilingCheck();
            ChangeWorldCoords(velX, velY, true); // Update the current x and y positions based on velocity.
        }

        public void CeilingCheck()
        {
            if (CollisionDetector.MapCollisionDetect(this, 0, -1) && GetVelY() < 0)
            {
                HitCeiling();
            }
        }

        public virtual void HitCeiling()
        {
            SetVelocity(null, 0);
        }

        public virtual void HitFloor()
        {
            SetVelocity(null, 0);
        }

        public double GetVelX()
        {
            return velX;
        }

        public double GetVelY()
        {
            return velY;
        }
    }
}
