using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameEngine
{
    public sealed class TextureLibrary
    {
        #region Singleton
        private static readonly TextureLibrary INSTANCE = new TextureLibrary();
        public static TextureLibrary Get()
        {
            return INSTANCE;
        }
        #endregion

        private Dictionary<String, Texture2D> textureLib = new Dictionary<string, Texture2D>();

        public TextureLibrary() { }

        // Initializes the Texture library to contain all relevant Texture2Ds for the game.
        public void InitTextureLib(ContentManager content)
        {
            AddTexture("Spaceman Body", content);
            AddTexture("Spaceman Heads", content);
            AddTexture("Background1", content);
            AddTexture("Foreground1", content);
            AddTexture("Hitbox1", content);
        }

        // Makes it a bit simpler to add textures who are not in any folders
        public void AddTexture(String key, ContentManager content)
        {
            AddTexture(key, content.Load<Texture2D>(key));
        }

        // Adds a texture to the Texture Library so it can be accessed by any other sprite
        public void AddTexture(String key, Texture2D texture)
        {
            textureLib.Add(key, texture);
        }

        // Retrieves the texture associated to a key or returns null
        public Texture2D GetTexture(String key)
        {
            Texture2D result = null;
            textureLib.TryGetValue(key, out result);
            return result;
        }

    }
}
