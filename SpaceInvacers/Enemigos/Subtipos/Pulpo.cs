﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
    class Pulpo : Enemigo
    {
        public Pulpo(int x, int y) : base(x, y, "\u1578\u157A", ConsoleColor.Yellow) { }
    }
}
