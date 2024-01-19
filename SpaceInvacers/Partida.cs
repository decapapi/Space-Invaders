using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
    class Partida
	{
		public void Lanzar()
		{
			Marcador marcador = new Marcador();
			BloqueDeEnemigos bloqueDeEnemigos = new BloqueDeEnemigos();
			BloqueDeTorres bloqueDeTorres = new BloqueDeTorres();
			Ovni ovni = new Ovni();
			Nave nave = new Nave();

			Proyectil proyectil = new Proyectil();

			List<Timer> timers = new List<Timer>();

			Timer timerBloque = new Timer(800);
			Timer timerMoverOvni = new Timer(1000);
			Timer timerDisparoEnemigo = new Timer(1500);
			Timer timerActualizarDisparoEnemigo = new Timer(40);

			timers.Add(timerBloque);
			timers.Add(timerMoverOvni);
			timers.Add(timerDisparoEnemigo);
			timers.Add(timerActualizarDisparoEnemigo);

			Timer timerActualizarDisparo = new Timer(60);

			ConsoleKey tecla;
			do {
				tecla = ConsoleKey.None;

				foreach (Timer timer in timers)
					timer.Actualizar();

				if (proyectil.GetActivo()) {
					timerActualizarDisparo.Actualizar();
					if (timerActualizarDisparo.GetTicked())
						proyectil.Mover(false);
				}

				bloqueDeEnemigos.Dibujar();
				bloqueDeTorres.Dibujar();
				nave.Dibujar();

				if (ovni.GetActivo())
					ovni.Dibujar();

				if (Console.KeyAvailable)
					tecla = Console.ReadKey(true).Key;

				switch (tecla) {
					case ConsoleKey.LeftArrow: nave.MoverIzquierda(); break;
					case ConsoleKey.Spacebar:
						if (!proyectil.GetActivo())
							proyectil.Disparar(nave.GetX(), nave.GetY());
						break;
					case ConsoleKey.RightArrow: nave.MoverDerecha(); break;
				}

				if (timerBloque.GetTicked())
					bloqueDeEnemigos.Mover();

				if (timerMoverOvni.GetTicked())
					ovni.Mover();

				if (timerActualizarDisparoEnemigo.GetTicked())
					bloqueDeEnemigos.MoverProyectil();

				if (timerDisparoEnemigo.GetTicked())
					bloqueDeEnemigos.Disparar();

				if (bloqueDeEnemigos.ComprobarColisionProyectil(nave))
					marcador.ConsumirVidar();

				if (proyectil.GetActivo()) {
					if (bloqueDeEnemigos.ComprobarColisiones(proyectil))
						marcador.IncrementarPuntuacion(10);

					if (ovni.GetActivo() && ovni.Colisiona(proyectil)) {
						ovni.Destruir();
						proyectil.Destruir();
						marcador.IncrementarPuntuacion(50);
					}

				}

			} while (tecla != ConsoleKey.Escape && marcador.GetVidas() >= 0);

			MostrarGameOver();
		}

		private void MostrarGameOver()
		{
			Pantalla.Limpiar();
			Pantalla.DibujarMarco();
			Pantalla.TextoCentrado("G A M E   O V E R", 8, ConsoleColor.Red);
			Pantalla.TextoCentrado("Pulsa Intro para volver o ESC para salir", 10);

			ConsoleKey tecla;
			do {
				tecla = Console.ReadKey(true).Key;
			} while (tecla != ConsoleKey.Escape && tecla != ConsoleKey.Enter);

			if (tecla == ConsoleKey.Enter)
			{
				Pantalla.Limpiar();
				Juego juego = new Juego();
				juego.Lanzar();
			}
		}
	}
}
