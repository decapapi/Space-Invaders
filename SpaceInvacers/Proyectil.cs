using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Proyectil : Sprite, IDestructible
	{
		private bool activo;

		public Proyectil() : base(0, 0, "\u2579", ConsoleColor.White)
		{
			this.activo = false;
		}

		public bool Activo
		{
			get { return this.activo; }
			set { this.activo = value; }
		}

		public void Destruir()
		{
			this.Borrar();
			this.activo = false;
		}

		public void Disparar(int x, int y)
		{
			this.x = x;
			this.y = y;
			this.activo = true;
		}

		public void Mover(bool positivo)
		{
			if (!positivo && this.y == 3 || positivo && y == Pantalla.SizeY - 2) {
				this.Destruir();
				return;
			}

			base.Mover(this.x, this.y + (positivo ? 1 : -1));
			base.Dibujar();
		}
	}
}
