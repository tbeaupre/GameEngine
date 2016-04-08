using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public sealed class PhysicsEngine
    {
        #region Singleton
        private static readonly PhysicsEngine INSTANCE = new PhysicsEngine();
        public static PhysicsEngine Get()
        {
            return INSTANCE;
        }
        #endregion

        double gravity = .5; //Pixels per update
        double terminalVel = 9; //Terminal Velocity for gravity updates.

        public PhysicsEngine() { }

        // Applies Physics to each sprite in the game
        public void ApplyPhysics()
        {
            foreach (IObject i in SpriteLibrary.Get().GetAllSprites())
            {
                i.ChangeVelocity(0, gravity * i.GetGravityFactor());
                if (i.GetVelY() > terminalVel)
                    i.SetVelocity(null, terminalVel);
            }
        }
    }
}
