using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class PiezaTorre : Sprite, IDestructible
	{
		public bool Activo { get; set; } = true;

		public PiezaTorre(int x, int y) : base(x, y, "\u2592", ConsoleColor.Green) { }

		public void Destruir()
		{
			this.Activo = false;
			this.Borrar();
		}
	}
}
