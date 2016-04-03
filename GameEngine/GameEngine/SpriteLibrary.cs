using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public sealed class SpriteLibrary
    {
        #region Singleton
        private static readonly SpriteLibrary INSTANCE = new SpriteLibrary();
        public static SpriteLibrary Get()
        {
            return INSTANCE;
        }
        #endregion
        
        private List<ISprite> allySprites = new List<ISprite>();
        private List<ISprite> enemySprites = new List<ISprite>();
        private List<ISprite> neutralSprites = new List<ISprite>();
        private List<ISprite> environmentSprites = new List<ISprite>();

        public SpriteLibrary() { }

        // Adds a sprite to the list associated with its group
        public void AddSprite(Allegiance allegiance, ISprite sprite)
        {
            switch (allegiance)
            {
                case Allegiance.Ally:
                    allySprites.Add(sprite);
                    break;
                case Allegiance.Enemy:
                    enemySprites.Add(sprite);
                    break;
                case Allegiance.Environment:
                    neutralSprites.Add(sprite);
                    break;
                case Allegiance.Neutral:
                    environmentSprites.Add(sprite);
                    break;
            }
        }

        // Removes a Sprite from its group's list
        public void DeleteSprite(Allegiance allegiance, ISprite sprite)
        {
            switch (allegiance)
            {
                case Allegiance.Ally:
                    allySprites.Remove(sprite);
                    break;
                case Allegiance.Enemy:
                    enemySprites.Remove(sprite);
                    break;
                case Allegiance.Environment:
                    neutralSprites.Remove(sprite);
                    break;
                case Allegiance.Neutral:
                    environmentSprites.Remove(sprite);
                    break;
            }
        }

        public List<ISprite> GetSpritesOfType(Allegiance allegiance)
        {
            switch (allegiance)
            {
                case Allegiance.Ally:
                    return allySprites;
                case Allegiance.Enemy:
                    return enemySprites;
                case Allegiance.Environment:
                    return neutralSprites;
                case Allegiance.Neutral:
                    return environmentSprites;
                default:
                    return allySprites;
            }
        }

        public List<ISprite> GetAllSprites()
        {
            List<ISprite> result = new List<ISprite>();
            result.AddRange(allySprites);
            result.AddRange(enemySprites);
            result.AddRange(neutralSprites);
            result.AddRange(environmentSprites);

            return result;
        }

        public ISprite GetCharacter()
        {
            return allySprites[0];
        }
    }
}
