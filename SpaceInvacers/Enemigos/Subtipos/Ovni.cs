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

        public Ovni(int x, int y, string imagen, ConsoleColor color) : base(x, y, imagen, color)
        {
			this.activo = false;
        }

        public Ovni() : this(1, 4, "\u0F3A\u1d16\u0F3B", ConsoleColor.Red) { }

        public void Mover()
        {
            Random random = new Random();

            if (!this.activo && random.Next(1, 100) > 95)
                return;

			this.activo = true;

            bool puedeMoverDerecha = true;
            bool puedeMoverIzquierda = true;

            if (this.x == Console.BufferWidth - this.Imagen.Length)
                puedeMoverDerecha = false;

            if (this.x == 0)
                puedeMoverIzquierda = false;

            if (random.Next(1, 10) <= 5 && puedeMoverDerecha)
				this.MoverDerecha();
            else if (puedeMoverIzquierda)
				this.MoverIzquierda();
        }

        public new void Destruir()
        {
            this.Borrar();
			this.activo = false;
			this.x = 1;
        }

        public bool GetActivo()
        {
            return activo;
        }
    }
}
