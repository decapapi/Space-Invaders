using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Enemigo : Sprite, IDestructible
	{
		public bool Activo { get; set; } = true;

		public Enemigo(int x, int y, string imagen, ConsoleColor color) : base(x, y, imagen, color) { }

		public void MoverDerecha() { base.Mover(x + 1, y); }
		public void MoverIzquierda() { base.Mover(x - 1, y); }
		public void Subir() { base.Mover(x, y - 1); }
		public void Bajar() { base.Mover(x, y + 1); }

		public virtual void Destruir()
		{
			this.Activo = false;
			this.Borrar();
		}
	}
}
