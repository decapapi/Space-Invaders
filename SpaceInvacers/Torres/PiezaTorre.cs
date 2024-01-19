using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class PiezaTorre : Sprite
	{
		private bool activo = true;

		public PiezaTorre(int x, int y) : base(x, y, "\u2635", ConsoleColor.Green) { }

		public bool GetActivo() { return this.activo; }

		public void Destruir()
		{
			this.activo = false;
			this.Borrar();
		}
	}
}
