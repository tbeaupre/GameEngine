using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class Map
    {
        #region Singleton
        private static readonly Map INSTANCE = new Map();
        public static Map Get()
        {
            return INSTANCE;
        }
        #endregion

        Texture2D background;
        Texture2D hitbox;
        Texture2D foreground;
        int parallaxFactor = 5;
        int x = 0;
        int y = 0;
        int offsetX = 0;
        int offsetY = 0;

        public Map() { }

        public void InitMap()
        {
            this.background = TextureLibrary.Get().GetTexture("Background1");
            this.hitbox = TextureLibrary.Get().GetTexture("Hitbox1");
            this.foreground = TextureLibrary.Get().GetTexture("Foreground1");
        }

        public Texture2D GetBackground()
        {
            return this.background;
        }

        public Texture2D GetHitbox()
        {
            return this.hitbox;
        }

        public Texture2D GetForeground()
        {
            return this.foreground;
        }

        public int GetParallaxFactor()
        {
            return this.parallaxFactor;
        }

        public double GetX()
        {
            return this.x - offsetX;
        }

        public double GetY()
        {
            return this.y - offsetY;
        }

        public double GetOffsetX()
        {
            return this.offsetX;
        }

        public double GetOffsetY()
        {
            return this.offsetY;
        }

        public void ChangeX(int deltaX)
        {
            this.x += deltaX;
            if (this.x >= 0)
            {
                offsetX = this.x;
            }
            else if (this.x + hitbox.Width <= ScreenData.Get().GetLowResWidth())
            {
                offsetX = this.x + hitbox.Width - ScreenData.Get().GetLowResWidth();
            }
        }

        public void ChangeY(int y)
        {
            this.y += y;
            if (this.y >= 0)
            {
                offsetY = this.y;
            }
            else if (this.y + hitbox.Height <= ScreenData.Get().GetLowResHeight())
            {
                offsetY = this.y + hitbox.Height - ScreenData.Get().GetLowResHeight();
            }
        }
    }
}
