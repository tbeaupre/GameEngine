using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public abstract class Sprite : ISprite
    {
        Allegiance allegiance;
        Texture2D texture;
        int currentFrame;
        int numFrames;
        double worldX = 0;
        double worldY = 0;

        public Sprite(Allegiance allegiance, Texture2D texture, double x, double y, int currentFrame, int numFrames)
        {
            this.allegiance = allegiance;
            this.texture = texture;
            this.worldX = x;
            this.worldY = y;
            this.currentFrame = currentFrame;
            this.numFrames = numFrames;
        }

        public virtual void ChangeWorldCoords(double x, double y)
        {
            worldX += x;
            worldY += y;
        }

        public void Delete()
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

        public void SetCurrentFrame(int frameNum)
        {
            this.currentFrame = frameNum;
        }

        public void SetTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        public void SetNumFrames(int i)
        {
            this.numFrames = i;
        }

        public virtual void Update()
        {

        }
    }
}
