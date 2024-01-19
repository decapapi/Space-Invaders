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
            activo = false;
        }

        public Ovni() : this(1, 4, "<^>", ConsoleColor.Red) { }

        public void Mover()
        {
            Random random = new Random();

            if (!activo && random.Next(1, 100) > 95)
                return;

            activo = true;

            bool puedeMoverDerecha = true;
            bool puedeMoverIzquierda = true;

            if (x == Console.BufferWidth - Imagen.Length)
                puedeMoverDerecha = false;

            if (x == 0)
                puedeMoverIzquierda = false;

            if (random.Next(1, 10) <= 5 && puedeMoverDerecha)
                MoverDerecha();
            else if (puedeMoverIzquierda)
                MoverIzquierda();
        }

        public void Destruir()
        {
            Borrar();
            activo = false;
            x = 1;
        }

        public bool GetActivo()
        {
            return activo;
        }
    }
}
