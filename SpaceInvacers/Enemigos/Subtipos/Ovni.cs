using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
    class Ovni : Enemigo
    {
        private bool activo;
        private int direccion;
		private Random random;

		public Ovni(int x, int y, string imagen, ConsoleColor color) : base(x, y, imagen, color)
        {
			this.activo = false;
            this.direccion = 0;
            this.random = new Random();
        }
        
        public Ovni() : this(1, 4, "\u0F3A\u1d16\u0F3B", ConsoleColor.Red) { }

        public void Mover()
        {
            if (!this.activo && random.Next(1, 1000) <= 990)
                return;

			this.activo = true;

            if (this.direccion == 0) { // No se está moviendo
                int dir = random.Next(1, 100) <= 50 ? -1 : 1;
				this.direccion = dir;
                this.x = dir > 0 ? 1 : Pantalla.SizeX - this.Imagen.Length - 3; 
			}

            if (this.direccion > 0 && this.x == Pantalla.SizeX - this.Imagen.Length - 1
				 || this.direccion < 0 && this.x == 1) {
                this.Destruir();
                return;
            }

            if (direccion > 0)
                this.MoverDerecha();
            else
				this.MoverIzquierda();
		}

        public override void Destruir()
        {
            this.Borrar();
			this.activo = false;
			this.direccion = 0;
        }

        public bool GetActivo()
        {
            return activo;
        }
    }
}
