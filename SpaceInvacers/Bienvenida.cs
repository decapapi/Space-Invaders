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
			Pantalla.CrearFondo();

			Timer timer = new Timer(1000);

			ConsoleKey tecla = ConsoleKey.None;
			do {
				if (Console.KeyAvailable)
					tecla = Console.ReadKey(true).Key;

				if (timer.GetTicked()) {
					Pantalla.ActualizarFondo();
					Pantalla.TextoCentrado("S P A C E   I N V A D E R S", 10);
					Pantalla.TextoCentrado("Pulsa Intro para jugar o ESC para salir", 12);
				}
				timer.Actualizar();
			} while (tecla == ConsoleKey.None || (tecla != ConsoleKey.Escape && tecla != ConsoleKey.Enter));

			Pantalla.Limpiar();
			this.salir = tecla == ConsoleKey.Escape;
		}

		public bool GetSalir()
		{
			return this.salir;
		}
	}
}
