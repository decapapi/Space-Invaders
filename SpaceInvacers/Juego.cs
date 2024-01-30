using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Juego
	{
		public Juego() : this(Pantalla.SizeX) { }

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
			Pantalla.SizeX = Configuracion.GetInt32("sizeX");
			Pantalla.SizeY = Configuracion.GetInt32("sizeY");
			if (Pantalla.SizeY > -1)
				if (Pantalla.SizeY > -1) new Juego(Pantalla.SizeX, Pantalla.SizeY).Lanzar();
				else new Juego(Pantalla.SizeX).Lanzar();
			else
				new Juego().Lanzar();
		}
	}
}
