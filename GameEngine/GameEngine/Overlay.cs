using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public abstract class Overlay : IOverlay
    {
        IObject master;
        int xOffset = 0; // The overall X offset of the overlay
        int yOffset = 0; // The overall Y offset of the overlay
        int[] xFrameOffsets; // Each frame of the master has a certain offset from the master's X
        int[] yFrameOffsets; // Each frame of the master has a certain offset from the master's Y
        int numFrames;
        Texture2D texture;

        public Overlay(IObject master, int[] xOffsets, int[] yOffsets, int numFrames, String texture)
        {
            this.master = master;
            this.xFrameOffsets = xOffsets;
            this.yFrameOffsets = yOffsets;
            this.numFrames = numFrames;
            this.texture = TextureLibrary.Get().GetTexture(texture);
        }

        public Overlay(IObject master, int xOffset, int yOffset, int[] xOffsets, int[] yOffsets, int numFrames, String texture)
        {
            this.master = master;
            this.xOffset = xOffset;
            this.yOffset = yOffset;
            this.xFrameOffsets = xOffsets;
            this.yFrameOffsets = yOffsets;
            this.numFrames = numFrames;
            this.texture = TextureLibrary.Get().GetTexture(texture);
        }

        // Returns either the hardset current frame or the frame associated with the current master frame if no special frame is set.
        public virtual int GetCurrentFrame()
        {
            return 0;
        }

        public int GetXOffset()
        {
            if (xFrameOffsets == null)
            {
                if (GetMirrored())
                    return 0;
                else
                    return xOffset;
            }
            else
            {
                if (GetMirrored())
                    return -(xFrameOffsets[master.GetCurrentFrame()]);
                else
                    return xFrameOffsets[master.GetCurrentFrame()] + xOffset;
            }
        }

        public int GetYOffset()
        {
            if (yFrameOffsets == null) return yOffset;
            else
            {
                return yFrameOffsets[master.GetCurrentFrame()] + yOffset;
            }
        }

        public DRectangle GetDestRect()
        {
            DRectangle masterDest = master.GetDestRect();
            return new DRectangle(masterDest.X + GetXOffset(), masterDest.Y + GetYOffset(), GetSpriteWidth(), GetSpriteHeight());
        }

        public int GetNumFrames()
        {
            return this.numFrames;
        }

        public DRectangle GetSourceRect()
        {
            return new DRectangle(GetSpriteWidth() * GetCurrentFrame(), 0, GetSpriteWidth(), texture.Height);
        }

        public int GetSpriteHeight()
        {
            return this.texture.Height;
        }

        public int GetSpriteWidth()
        {
            return this.texture.Width / numFrames;
        }

        public Texture2D GetTexture()
        {
            return this.texture;
        }

        public virtual void Update()
        {

        }

        public bool GetMirrored()
        {
            return this.master.GetMirrored();
        }

        public IObject GetMaster()
        {
            return this.master;
        }
    }
}
