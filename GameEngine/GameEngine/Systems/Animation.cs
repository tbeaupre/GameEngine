using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Systems
{
    class Animation
    {
        public AnimationData data { get; set; }
        public bool loop { get; set; }
        public int currentFrameIndex { get; set; }
        private int frameTimer;
        public bool done { get; set; }

        public Animation (String animation = "default", bool loop = false, int start = 0)
        {
            this.data = AnimationLibrary.Get().GetAnimation(animation);
            this.loop = loop;
            this.currentFrameIndex = start;
            this.frameTimer = 0;
            this.done = false;
        }

        int GetCurrentFrame()
        {
            return data.animation[currentFrameIndex];
        }

        void UpdateAnimation()
        {
            if (!done)
            {
                frameTimer++;
                if (frameTimer == data.frameTimer)
                {
                    frameTimer = 0;
                    currentFrameIndex++;
                    if (currentFrameIndex == data.animation.GetUpperBound(0))
                    {
                        currentFrameIndex = 0;
                        if (!loop)
                        {
                            done = true;
                        }
                    }
                }
            }
        }
    }
}
