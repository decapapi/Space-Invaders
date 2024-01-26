using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Juego
	{
		public Juego() : this(60) { }

		public Juego(int sizeX) : this(sizeX, (int)(sizeX / 2.3)) { }

		public Juego(int sizeX, int sizeY) 
		{
			Pantalla.SizeX = sizeX;
			Pantalla.SizeY = sizeY;
			Configuracion.Guardar("sizeX", sizeX);
			Configuracion.Guardar("sizeY", sizeY);
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
			int sizeX = Configuracion.GetInt32("sizeX");
			int sizeY = Configuracion.GetInt32("sizeY");
			if (sizeX > -1)
				if (sizeY > -1) new Juego(sizeX, sizeY).Lanzar();
				else new Juego(sizeX).Lanzar();
			else
				new Juego().Lanzar();
		}
	}
}
