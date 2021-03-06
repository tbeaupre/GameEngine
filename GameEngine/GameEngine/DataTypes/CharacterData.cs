﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    class CharacterData
    {
        double jumpHeight;
        int numJumps;
        double moveSpeed;
        int animationTimer;
        double airDodgeVel;
        int airDodgeTimer;
        
        public CharacterData()
        {
            // Default values
            jumpHeight = -7;
            numJumps = 2;
            moveSpeed = 3;
            animationTimer = 20;
            airDodgeVel = 5;
            airDodgeTimer = 10;
        }

        public double GetJumpHeight()
        {
            return this.jumpHeight;
        }

        public double GetMoveSpeed()
        {
            return this.moveSpeed;
        }

        public int GetNumJumps()
        {
            return this.numJumps;
        }

        public int GetAnimationTimer()
        {
            return this.animationTimer;
        }

        public double GetAirDodgeLength()
        {
            return this.airDodgeVel;
        }

        public int GetAirDodgeTimer()
        {
            return this.airDodgeTimer;
        }
    }
}
