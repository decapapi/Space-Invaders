
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Pantalla
	{
		private struct Caracter {
			public char imagen;
			public bool haCambiado;
		}

		private static Caracter[,] fondo;

		private static char[] estrellitas = { '\u2605', '\u2734' };

		public static int Ancho { get; set; }
		public static int Alto { get; set; }

		public static void TextoCentrado(string texto, int y, ConsoleColor color = ConsoleColor.White)
		{
			int consoleWidth = Ancho;
			int textLength = texto.Length;

			int x = (consoleWidth - textLength) / 2;

			x = Math.Max(0, Math.Min(x, consoleWidth - textLength));

			Texto(texto, x, y, color);
		}

		public static void Texto(string texto, int x, int y, ConsoleColor color = ConsoleColor.White)
		{
			if (x >= 0 && x < Ancho && y >= 0 && y < Alto) {
				Console.SetCursorPosition(x, y);
				Console.ForegroundColor = color;
				Console.Write(texto);
				Console.ResetColor();
			}
		}

		public static void CrearFondo()
		{
			fondo = new Caracter[Alto - 2, Ancho - 2];

			Random random = new Random();

			for (int i = 1; i < fondo.GetLength(0); i++)
				for (int j = 1; j < fondo.GetLength(1); j++) {
					if (random.Next(1, 100) > 90)
						if (random.Next(1, 100) > 90)
							fondo[i, j].imagen = estrellitas[random.Next(0, estrellitas.Length - 1)];
						else
							fondo[i, j].imagen = '.';
					else
						fondo[i, j].imagen = ' ';
				}
		}

		public static void ActualizarFondo()
		{
			Random random = new Random();

			for (int i = 1; i < fondo.GetLength(0); i++)
				for (int j = 1; j < fondo.GetLength(1); j++) {
					if (random.Next(1, 100) > 95) {
						char imagenAntigua = fondo[i, j].imagen;
						if (random.Next(1, 100) > 90)
							if (random.Next(1, 100) > 90)
								fondo[i ,j].imagen = estrellitas[random.Next(0, estrellitas.Length - 1)];
							else
								fondo[i, j].imagen = '.';
						else
							fondo[i, j].imagen = ' ';

						fondo[i, j].haCambiado = imagenAntigua != fondo[i, j].imagen;
					}
				}
			DibujarFondo();
		}

		public static void DibujarFondo()
		{
			Console.ForegroundColor = ConsoleColor.DarkGray;
			for (int i = 1; i < fondo.GetLength(0); i++)
				for (int j = 1; j < fondo.GetLength(1); j++) {
					if (!fondo[i, j].haCambiado)
						continue;
					Console.SetCursorPosition(j, i);
					Console.Write(fondo[i ,j].imagen);
				}
			Console.ResetColor();
		}

		public static void DibujarMarco()
		{
			int ancho = Ancho;
			int alto = Alto - 1;
			Console.SetCursorPosition(0, 0);
			Console.Write("\u250F"); // Esquina superior izquierda

			for (int i = 1; i <= ancho - 2; i++)
				Console.Write("\u2501"); // Línea horizontal superior

			Console.WriteLine("\u2513"); // Esquina superior derecha

			for (int i = 1; i <= alto - 2; i++)	{ // Líneas verticales izquierdas y derechas
				Console.SetCursorPosition(0, i);
				Console.Write("\u2503"); 
				Console.SetCursorPosition(ancho - 1, i);
				Console.Write("\u2503");
			}

			Console.Write("\u2517"); // Esquina inferior izquierda

			for (int i = 1; i <= ancho - 2; i++)
				Console.Write("\u2501"); // Línea horizontal inferior

			Console.WriteLine("\u251B"); // Esquina inferior derecha
		}

		public static void Limpiar()
		{
			for (int i = 1; i < Alto - 2; i++) {
				Console.SetCursorPosition(1, i);
				Console.Write(new string(' ', Ancho - 2));
			}
			Console.SetCursorPosition(0, Alto - 1);
			Console.Write(new string(' ', Ancho));
		}

		public static bool Confirmacion(string mensaje)
		{
			Limpiar();

			TextoCentrado(mensaje, PosTextoY());
			TextoCentrado("Pulsa Enter para confirmar o ESC para rechazar", PosTextoY(1));

			ConsoleKey tecla;
			do {
				tecla = Console.ReadKey(true).Key;
			} while (tecla != ConsoleKey.Enter && tecla != ConsoleKey.Escape);

			Limpiar();

			return tecla == ConsoleKey.Enter;
		}

		public static int PosTextoY(int numLinea = 0)
		{
			return (int)(Alto / 2.6) + (1 * numLinea);
		}
	}
}
