using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    class AnimationData
    {
        public int[] animation { get; }
        public int frameTimer { get; }

        public AnimationData(int[] animation, int frameTimer = 20)
        {
            this.animation = animation;
            this.frameTimer = frameTimer;
        }
    }
}
