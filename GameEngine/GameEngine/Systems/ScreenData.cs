using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    class ScreenData
    {
        #region Singleton
        private static readonly ScreenData INSTANCE = new ScreenData();
        public static ScreenData Get()
        {
            return INSTANCE;
        }
        #endregion

        int fullScreenWidth;
        int fullScreenHeight;
        int screenMultiplier;
        int lowResWidth;
        int lowResHeight;

        public ScreenData()
        {
            this.fullScreenWidth = 1920;
            this.fullScreenHeight = 1080;
            this.screenMultiplier = 6;
            this.lowResWidth = fullScreenWidth / screenMultiplier;
            this.lowResHeight = fullScreenHeight / screenMultiplier;
        }

        public int GetFullScreenWidth()
        {
            return this.fullScreenWidth;
        }

        public int GetFullScreenHeight()
        {
            return this.fullScreenHeight;
        }

        public int GetScreenMultiplier()
        {
            return this.screenMultiplier;
        }

        public int GetLowResWidth()
        {
            return this.lowResWidth;
        }

        public int GetLowResHeight()
        {
            return this.lowResHeight;
        }
    }
}
