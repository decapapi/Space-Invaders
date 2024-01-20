using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class PiezaTorre : Sprite, IDestructible
	{
		private bool activo = true;

		public PiezaTorre(int x, int y) : base(x, y, "\u2592", ConsoleColor.Green) { }

		public bool Activo
		{
			get { return this.activo; }
			set { this.activo = value; }
		}

		public void Destruir()
		{
			this.activo = false;
			this.Borrar();
		}
	}
}
