using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Proyectil : Sprite, IDestructible
	{
		public bool Activo { get; set; } = false;

		public Proyectil() : base(0, 0, "\u2579", ConsoleColor.White) { }

		public void Destruir()
		{
			this.Borrar();
			this.Activo = false;
		}

		public void Disparar(int x, int y)
		{
			this.x = x;
			this.y = y;
			this.Activo = true;
		}

		public void Mover(bool positivo)
		{
			if (!positivo && this.y == 3 || positivo && y == Pantalla.Alto - 3) {
				this.Destruir();
				return;
			}

			base.Mover(this.x, this.y + (positivo ? 1 : -1));
			base.Dibujar();
		}
	}
}
