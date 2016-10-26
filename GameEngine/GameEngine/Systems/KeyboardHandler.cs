using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class KeyHandler
    {
        #region Singleton
        private static readonly KeyHandler INSTANCE = new KeyHandler();
        public static KeyHandler Get()
        {
            return INSTANCE;
        }
        #endregion

        bool gamePad;
        Microsoft.Xna.Framework.PlayerIndex index = Microsoft.Xna.Framework.PlayerIndex.One;

        KeyboardState oldKeys;
        KeyboardState newKeys;

        GamePadState oldPad;
        GamePadState newPad;

        public KeyHandler()
        {
            gamePad = true;
            if (GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One).IsConnected)
                this.index = Microsoft.Xna.Framework.PlayerIndex.One;
            else if (GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Two).IsConnected)
                this.index = Microsoft.Xna.Framework.PlayerIndex.Two;
            else if (GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Three).IsConnected)
                this.index = Microsoft.Xna.Framework.PlayerIndex.Three;
            else if (GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.Four).IsConnected)
                this.index = Microsoft.Xna.Framework.PlayerIndex.Four;
            else
                gamePad = false;
        }

        public void UpdateKeyboardHandler()
        {
            this.oldKeys = newKeys;
            this.oldPad = newPad;
            this.newKeys = Keyboard.GetState();
            if (GamePad.GetState(index).IsConnected)
            {
                this.newPad = GamePad.GetState(index);
                gamePad = true;
            }
            else
                gamePad = false;
        }

        public KeyboardState GetOldKeys()
        {
            return this.oldKeys;
        }

        public KeyboardState GetNewKeys()
        {
            return this.newKeys;
        }

        public GamePadState GetNewPad()
        {
            return this.newPad;
        }

        public bool IsKeyPressed(Buttons button)
        {
            return (IsKeyDown(button, false) && !IsKeyDown(button, true));
        }

        public bool IsKeyHeld(Buttons button)
        {
            return IsKeyDown(button, false);
        }

        public bool IsKeyDown(Buttons button, bool old)
        {
            GamePadState pad;
            KeyboardState keys;
            if (old)
            {
                pad = oldPad;
                keys = oldKeys;
            }
            else
            {
                pad = newPad;
                keys = newKeys;
            }
            if (gamePad)
            {
                switch (button) {
                    case Buttons.Down:
                        return (pad.ThumbSticks.Left.Y < -0.3);
                    case Buttons.Up:
                        return (pad.ThumbSticks.Left.Y > 0.3);
                    case Buttons.Left:
                        return (pad.ThumbSticks.Left.X < -0.3);
                    case Buttons.Right:
                        return (pad.ThumbSticks.Left.X > 0.3);
                    case Buttons.Fire:
                        return (pad.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.A));
                    case Buttons.Jump:
                        return (pad.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.X) || pad.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Y));
                    case Buttons.Special:
                        return (pad.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.B));
                    case Buttons.Dodge:
                        return (pad.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.LeftTrigger) || pad.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.RightTrigger));
                    default:
                        return false;
                }
            }
            else
            {
                switch (button)
                {
                    case Buttons.Down:
                        return (keys.IsKeyDown(Keys.Down));
                    case Buttons.Up:
                        return (keys.IsKeyDown(Keys.Up));
                    case Buttons.Left:
                        return (keys.IsKeyDown(Keys.Left));
                    case Buttons.Right:
                        return (keys.IsKeyDown(Keys.Right));
                    case Buttons.Fire:
                        return (keys.IsKeyDown(Keys.X));
                    case Buttons.Jump:
                        return (keys.IsKeyDown(Keys.Z));
                    case Buttons.Special:
                        return (keys.IsKeyDown(Keys.C));
                    default:
                        return false;
                }
            }
        }
    }
}
