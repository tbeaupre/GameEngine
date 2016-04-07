using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GameEngine
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D sprite;
        RenderTarget2D lowRes;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = ScreenData.Get().GetFullScreenWidth();
            graphics.PreferredBackBufferHeight = ScreenData.Get().GetFullScreenHeight();
            //graphics.IsFullScreen = true;

            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            TextureLibrary.Get().InitTextureLib(this.Content);
            SpriteLibrary.Get().AddSprite(Allegiance.Ally, new Character(100, 100));
            SpriteLibrary.Get().AddSprite(Allegiance.Ally, new Ally(200, 110));
            Map.Get().InitMap();
            sprite = TextureLibrary.Get().GetTexture("Spaceman Body");
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            lowRes = new RenderTarget2D(graphics.GraphicsDevice, ScreenData.Get().GetLowResWidth(), ScreenData.Get().GetLowResHeight());

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyHandler.Get().UpdateKeyboardHandler();
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            foreach (IObject i in SpriteLibrary.Get().GetAllSprites())
            {
                i.Update();
            }
            PhysicsEngine.Get().ApplyPhysics();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            // Prep to draw lowRes sprites
            graphics.GraphicsDevice.SetRenderTarget(lowRes);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            GraphicsDevice.Clear(new Color(0, 0, 0, 0));
            // Draw the lowRes sprites
            DrawLowRes();
            // Finish up drawing lowRes sprites
            base.Draw(gameTime);
            spriteBatch.End();
            
            graphics.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            DrawHighResMap();
            spriteBatch.Draw(lowRes, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
            base.Draw(gameTime);
            spriteBatch.End();
        }

        public void DrawLowRes()
        {
            DrawLowResMap();
            DrawAllySprites();
        }

        public void DrawIObject(IObject sprite, float depth)
        {
            DrawISprite(sprite, depth);
            foreach (IOverlay o in sprite.GetOverlays())
            {
                DrawISprite(o, depth);
            }
        }

        public void DrawISprite(ISprite sprite, float depth)
        {
            SpriteEffects effect = SpriteEffects.None;
            if (sprite.GetMirrored()) effect = SpriteEffects.FlipHorizontally;
            spriteBatch.Draw(sprite.GetTexture(), sprite.GetDestRect().ToRectangle(), sprite.GetSourceRect().ToRectangle(), Color.White, 0.0f, new Vector2(0, 0), effect, depth);
        }

        public void DrawAllySprites()
        {
            foreach (IObject i in SpriteLibrary.Get().GetSpritesOfType(Allegiance.Ally))
            {
                DrawIObject(i, 0.5f);
            }
        }
        
        // Draws the background at specific fractional pixels so it must be done at high res
        public void DrawHighResMap()
        {
            Map map = Map.Get();
            spriteBatch.Draw(
                map.GetBackground(),
                new Rectangle(
                    (int)(map.GetX() * (6 / map.GetParallaxFactor()) * ((double)graphics.PreferredBackBufferWidth / (double)ScreenData.Get().GetFullScreenWidth())),
                    (int)(map.GetY() * (6 / map.GetParallaxFactor()) * ((double)graphics.PreferredBackBufferHeight / (double)ScreenData.Get().GetFullScreenHeight())),
                    (int)(map.GetBackground().Width * 6 * ((double)graphics.PreferredBackBufferWidth / (double)ScreenData.Get().GetFullScreenWidth())),
                    (int)(map.GetBackground().Height * 6 * ((double)graphics.PreferredBackBufferHeight / (double)ScreenData.Get().GetFullScreenHeight()))),
                null,
                Color.White);
        }

        // Draws the low res parts of the map such as hitbox and foreground
        public void DrawLowResMap()
        {
            Map map = Map.Get();
            spriteBatch.Draw(map.GetHitbox(), new Rectangle((int)map.GetX(), (int)map.GetY(), map.GetHitbox().Width, map.GetHitbox().Height), null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1f);
            if (map.GetForeground() != null)
            {
                spriteBatch.Draw(map.GetForeground(), new Rectangle((int)map.GetX(), (int)map.GetY(), map.GetForeground().Width, map.GetForeground().Height), null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.9f);
            }
        }
    }
}
