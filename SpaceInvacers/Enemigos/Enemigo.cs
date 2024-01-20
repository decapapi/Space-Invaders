using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Enemigo : Sprite, IDestructible
	{
		private bool activo = true;

		public Enemigo(int x, int y, string imagen, ConsoleColor color) : base(x, y, imagen, color) { }

		public void MoverDerecha() { base.Mover(x+1, y); }
		public void MoverIzquierda() { base.Mover(x-1, y); }

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
