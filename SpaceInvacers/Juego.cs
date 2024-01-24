using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Juego
	{
		public Juego() : this(60, 26) { }

		public Juego(int sizeX, int sizeY) 
		{
			Pantalla.SizeX = sizeX;
			Pantalla.SizeY = sizeY;
		}

		public void Lanzar()
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			Console.CursorVisible = false;
			Console.SetWindowSize(Pantalla.SizeX, Pantalla.SizeY);
			Console.SetBufferSize(Pantalla.SizeX, Pantalla.SizeY);
			Console.Title = "Space Invaders";

			Pantalla.DibujarMarco();

			Bienvenida bienvenida = new Bienvenida();
			bienvenida.Lanzar();

			if (!bienvenida.GetSalir()) {
				Partida partida = new Partida();
				partida.Lanzar();
			}
		}

		static void Main()
		{
			Juego juego = new Juego();
			juego.Lanzar();
		}
	}
}
