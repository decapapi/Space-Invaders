using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Bienvenida
	{
		private bool salir;

		public void Lanzar()
		{
			Pantalla.TextoCentrado("S P A C E   I N V A D E R S", 10);
			Pantalla.TextoCentrado("Pulsa Intro para jugar o ESC para salir", 12);

			ConsoleKey tecla;
			do {
				tecla = Console.ReadKey(true).Key;
			} while (tecla != ConsoleKey.Escape && tecla != ConsoleKey.Enter);

			Pantalla.Limpiar();
			this.salir = tecla == ConsoleKey.Escape;
		}

		public bool GetSalir()
		{
			return this.salir;
		}
	}
}
