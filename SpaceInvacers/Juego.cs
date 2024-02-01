using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Juego
	{
		public Juego() : this(Pantalla.Ancho > -1 ? Pantalla.Ancho : 60) { }

		public Juego(int Ancho) : this(Ancho, (int)(Ancho / 2.3)) { }

		public Juego(int Ancho, int Alto) 
		{
			Pantalla.Ancho = Ancho;
			Pantalla.Alto = Alto;
			Configuracion.Guardar("Ancho", Ancho);
			Configuracion.Guardar("Alto", Alto);
		}

		public void Lanzar()
		{
			Console.OutputEncoding = System.Text.Encoding.Unicode;
			Console.CursorVisible = false;
			Console.SetWindowSize(Pantalla.Ancho, Pantalla.Alto);
			Console.SetBufferSize(Pantalla.Ancho, Pantalla.Alto);
			Console.Title = "Space Invaders";

			Pantalla.Limpiar();
			Pantalla.DibujarMarco();

			Bienvenida bienvenida = new Bienvenida();
			bienvenida.Lanzar();

			if (!bienvenida.GetSalir())
				new Partida().Lanzar();
		}

		static void Main()
		{
			Pantalla.Ancho = Configuracion.GetInt32("Ancho");
			Pantalla.Alto = Configuracion.GetInt32("Alto");
			if (Pantalla.Alto > -1)
				if (Pantalla.Alto > -1) new Juego(Pantalla.Ancho, Pantalla.Alto).Lanzar();
				else new Juego(Pantalla.Ancho).Lanzar();
			else
				new Juego().Lanzar();
		}
	}
}
