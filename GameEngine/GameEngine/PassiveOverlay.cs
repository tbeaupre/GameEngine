using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class PassiveOverlay : Overlay
    {
        int[] frameData; // Each frame of the master associates with a certain overlay frame
        
        public PassiveOverlay(IObject master, int[] frameData, int[] xOffsets, int[] yOffsets, int numFrames, String texture)
            : base (master, xOffsets, yOffsets, numFrames, texture)
        {
            this.frameData = frameData;
        }

        public PassiveOverlay(IObject master, int[] frameData, int xOffset, int yOffset, int[] xOffsets, int[] yOffsets, int numFrames, String texture)
            : base(master, xOffset, yOffset, xOffsets, yOffsets, numFrames, texture)
        {
            this.frameData = frameData;
        }

        public PassiveOverlay(IObject master, int[] xOffsets, int[] yOffsets, int numFrames, String texture)
            : base(master, xOffsets, yOffsets, numFrames, texture)
        {
            this.frameData = null;
        }
        
        public PassiveOverlay(IObject master, int xOffset, int yOffset, int[] xOffsets, int[] yOffsets, int numFrames, String texture)
            : base(master, xOffset, yOffset, xOffsets, yOffsets, numFrames, texture)
        {
            this.frameData = null;
        }

        // Returns either the hardset current frame or the frame associated with the current master frame if no special frame is set.
        public override int GetCurrentFrame()
        {
            if (frameData == null) return this.GetMaster().GetCurrentFrame();
            else return this.frameData[GetMaster().GetCurrentFrame()];
        }
    }
}
