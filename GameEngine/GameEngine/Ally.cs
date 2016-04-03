using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine
{
    public class Ally : Object
    {
        public Ally (double x, double y) :
            base(Allegiance.Ally, TextureLibrary.Get().GetTexture("Spaceman Heads"), x, y, 1, 3)
        {

        }
    }
}
