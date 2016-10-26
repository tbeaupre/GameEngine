using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    class AnimationLibrary
    {
        #region Singleton
        private static readonly AnimationLibrary INSTANCE = new AnimationLibrary();
        public static AnimationLibrary Get()
        {
            return INSTANCE;
        }
        #endregion

        private Dictionary<String, AnimationData> animationLib = new Dictionary<string, AnimationData>();

        public AnimationLibrary()
        {
            AddAnimation("default", new AnimationData(new int[] { 0 }));
        }

        // Adds an animation to the Animation Library
        public void AddAnimation(String key, AnimationData data)
        {
            animationLib.Add(key, data);
        }

        // Retrieves the animation associated to a key or returns null;
        public AnimationData GetAnimation(String key)
        {
            AnimationData result = null;
            animationLib.TryGetValue(key, out result);
            return result;
        }
    }
}
