using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public static class CollisionDetector
    {
        // for rectangular collisions between two sprites.
        public static bool RectCollisionDetect(ISprite sprite1, ISprite sprite2)
        {
            return RectCollisionDetect(sprite1.GetDestRect(), sprite2.GetDestRect());
        }

        private static bool RectCollisionDetect(DRectangle rect1, DRectangle rect2)
        {
            return (rect2.X + rect2.Width > rect1.X && // sprite2's right side overlaps sprite1's left side
                rect2.X + rect2.Width < rect1.X + rect1.Width &&  // sprite2's right side isn't past sprite1's right side
                rect2.Y + rect2.Height > rect1.Y && // sprite2's bottom side overlaps sprite1's top side
                rect2.Y + rect2.Height < rect1.Y + rect1.Height  // sprite2's bottom side isn't past sprite1's bottom side
                );
        }

        public static bool MapCollisionDetect(ISprite sprite, double xOffset, double yOffset)
        {
            Map map = Map.Get();
            Rectangle rect = sprite.GetDestRect().ToRectangle();
            rect.X += (int)xOffset;
            rect.Y += (int)yOffset;
            rect.X -= (int)map.GetX();
            rect.Y -= (int)map.GetY();
            Color[] pixels;

            pixels = new Color[sprite.GetSpriteWidth() * sprite.GetSpriteHeight()];

            // Check to see if rectangle is outside of map.
            if (rect.X < 0
                || rect.Y < 0
                || rect.X + sprite.GetSpriteWidth() > map.GetHitbox().Width
                || rect.Y + sprite.GetSpriteHeight() > map.GetHitbox().Height) return false;

            map.GetHitbox().GetData<Color>(
                0, rect, pixels, 0, sprite.GetSpriteWidth() * sprite.GetSpriteHeight()
                );
            for (int y = 0; y < sprite.GetSpriteHeight(); y++)
            {
                for (int x = 0; x < sprite.GetSpriteWidth(); x++)
                {
                    Color colorA = pixels[y * sprite.GetSpriteWidth() + x];
                    if (colorA.A != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool MapCollisionDetect(ISprite sprite)
        {
            return MapCollisionDetect(sprite, 0.0, 0.0);
        }
    }
}
