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
        double x = 0;
        double y = 0;

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
            return this.x;
        }

        public double GetY()
        {
            return this.y;
        }

        public void ChangeX(double x)
        {
            this.x += x;
        }

        public void ChangeY(double y)
        {
            this.y += y;
        }
    }
}
