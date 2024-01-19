using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Pantalla
	{
		public static void TextoCentrado(string texto, int y, ConsoleColor color = ConsoleColor.White)
		{
			int consoleWidth = Console.BufferWidth;
			int textLength = texto.Length;

			int x = (consoleWidth - textLength) / 2;

			x = Math.Max(0, Math.Min(x, consoleWidth - textLength));

			Texto(texto, x, y, color);
		}

		public static void Texto(string texto, int x, int y, ConsoleColor color = ConsoleColor.White)
		{
			if (x >= 0 && x < Console.WindowWidth && y >= 0 && y < Console.WindowHeight) {
				Console.SetCursorPosition(x, y);
				Console.ForegroundColor = color;
				Console.Write(texto);
				Console.ResetColor();
			}
		}

		public static void DibujarMarco()
		{
			int ancho = Console.BufferWidth;
			int alto = Console.BufferHeight - 1;
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
			for (int i = 1; i < Console.BufferHeight - 2; i++) {
				Console.SetCursorPosition(1, i);
				Console.Write(new string(' ', Console.BufferWidth - 2));
			}
			Console.SetCursorPosition(0, Console.BufferHeight - 1);
			Console.Write(new string(' ', Console.BufferWidth - 2));
		}
	}
}
