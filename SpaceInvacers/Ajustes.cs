using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Ajustes
	{
		private static int seleccion = 0;
		private static Dictionary<int, (int, int)> screenSizes = new Dictionary<int, (int, int)>
		{
			{ 0, (60, 26) },
			{ 1, (80, 30) },
			{ 2, (100, 38) }
		};

		public static void Mostrar()
		{
			Pantalla.Limpiar();
			ConsoleKey tecla;
			do {
				Pantalla.TextoCentrado("* TAMAÑO DE LA PANTALLA *", Pantalla.PosTextoY(-2));
				Pantalla.TextoCentrado($"{(seleccion == 0 ? '\u23FA' : ' ')}  60x26 ", Pantalla.PosTextoY());
				Pantalla.TextoCentrado($"{(seleccion == 1 ? '\u23FA' : ' ')}  80x30 ", Pantalla.PosTextoY(2));
				Pantalla.TextoCentrado($"{(seleccion == 2 ? '\u23FA' : ' ')}  100x38", Pantalla.PosTextoY(4));
				Pantalla.TextoCentrado("Pulsa ESC para volver", Pantalla.PosTextoY(6));

				tecla = Console.ReadKey(true).Key;

				switch (tecla) {
					case ConsoleKey.Enter: CambiarAjustes(); break;
					case ConsoleKey.UpArrow:
						if (seleccion > 0)
							seleccion--;
						break;
					case ConsoleKey.DownArrow:
						if (seleccion < 2)
							seleccion++;
						break;
				}
			} while (tecla != ConsoleKey.Escape);
		}

		private static void CambiarAjustes()
		{
			if (Pantalla.Confirmacion("¿Seguro que quieres aplicar los cambios?")
				&& seleccion >= 0 && seleccion <= 2)
			{
				var (sizeX, sizeY) = screenSizes[seleccion];
				Pantalla.SizeX = sizeX;
				Pantalla.SizeY = sizeY;
				Configuracion.Guardar("sizeX", Pantalla.SizeX);
				Configuracion.Guardar("sizeY", Pantalla.SizeY);
				new Juego().Lanzar();
			}
		}
	}
}
