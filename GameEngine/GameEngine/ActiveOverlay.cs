using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class ActiveOverlay : Overlay
    {
        int currentFrame; // Allows for an override to simply following the master's frame

        public ActiveOverlay(IObject master, int currentFrame, int[] xOffsets, int[] yOffsets, int numFrames, String texture)
            : base(master, xOffsets, yOffsets, numFrames, texture)
        {
            this.currentFrame = currentFrame;
        }

        public ActiveOverlay(IObject master, int currentFrame, int xOffset, int yOffset, int[] xOffsets, int[] yOffsets, int numFrames, String texture)
            : base(master, xOffset, yOffset, xOffsets, yOffsets, numFrames, texture)
        {
            this.currentFrame = currentFrame;
        }

        // Returns either the hardset current frame or the frame associated with the current master frame if no special frame is set.
        public override int GetCurrentFrame()
        {
            return currentFrame;
        }

        public void SetCurrentFrame(int frame)
        {
            this.currentFrame = frame;
        }
    }
}
