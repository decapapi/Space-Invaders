using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
    class Calamar : Enemigo
    {
        public Calamar(int x, int y) : base(x, y, "\u15A7\u15A8", ConsoleColor.Magenta) { }
    }
}
