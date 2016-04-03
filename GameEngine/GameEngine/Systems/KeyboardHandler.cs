using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        KeyboardState oldKeys;
        KeyboardState newKeys;

        public KeyHandler() { }

        public void UpdateKeyboardHandler()
        {
            this.oldKeys = newKeys;
            this.newKeys = Keyboard.GetState();
        }

        public KeyboardState GetOldKeys()
        {
            return this.oldKeys;
        }

        public KeyboardState GetNewKeys()
        {
            return this.newKeys;
        }
    }
}
