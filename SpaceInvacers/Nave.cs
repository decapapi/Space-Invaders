﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Nave : Sprite
	{
		public Nave(int x, int y) : base(x, y, "\u15B9\u15BA", ConsoleColor.Green) { }

		public Nave() : this(29, Console.BufferHeight-3) { }

		public void MoverDerecha()
		{
			base.Mover(this.x + 1, this.y);
		}

		public void MoverIzquierda()
		{
			base.Mover(this.x - 1, this.y);
		}
	}
}
