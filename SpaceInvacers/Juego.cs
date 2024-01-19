using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Juego
	{
		private int sizeX;
		private int sizeY;

		public Juego() : this(60, 26) { }

		public Juego(int sizeX, int sizeY) 
		{
			this.sizeX = sizeX;
			this.sizeY = sizeY;
		}

		public void Lanzar()
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			Console.CursorVisible = false;
			Console.SetWindowSize(this.sizeX, this.sizeY);
			Console.SetBufferSize(this.sizeX, this.sizeY);
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
