using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
    class Partida
	{
		private bool pausa;
		private bool nivelGanado;
		private int nivel;
		private static Marcador marcador;

		public Partida() : this(true, 1) { }

		public Partida(bool nuevaPartida, int nivel)
		{
			this.nivel = nivel;
			this.pausa = false;
			this.nivelGanado = false;
			if (nuevaPartida) marcador = new Marcador();
			else marcador.Actuallizar();
		}

		public void Lanzar()
		{
			BloqueDeEnemigos bloqueDeEnemigos = new BloqueDeEnemigos();
			BloqueDeTorres bloqueDeTorres = new BloqueDeTorres();
			Ovni ovni = new Ovni();
			Nave nave = new Nave();

			Proyectil proyectil = new Proyectil();

			List<Timer> timers = new List<Timer>();

			Timer timerBloque = new Timer(800 / this.nivel);
			Timer timerMoverOvni = new Timer(150 / this.nivel);
			Timer timerDisparoEnemigo = new Timer(1500 / this.nivel);
			Timer timerActualizarDisparoEnemigo = new Timer(40);

			timers.Add(timerBloque);
			timers.Add(timerMoverOvni);
			timers.Add(timerDisparoEnemigo);
			timers.Add(timerActualizarDisparoEnemigo);

			Timer timerActualizarDisparo = new Timer(60);

			ConsoleKey tecla;
			do {
				tecla = ConsoleKey.None;

				if (Console.KeyAvailable)
					tecla = Console.ReadKey(true).Key;

				if (tecla == ConsoleKey.F10) {
					this.pausa = !this.pausa;
					Pantalla.Limpiar();
					if (this.pausa) MostrarPausa();
					else marcador.Actuallizar();
				}

				if (this.pausa)
					continue;

				switch (tecla) {
					case ConsoleKey.LeftArrow: nave.MoverIzquierda(); break;
					case ConsoleKey.RightArrow: nave.MoverDerecha(); break;
					case ConsoleKey.Spacebar:
						if (!proyectil.Activo)
							proyectil.Disparar(nave.GetX(), nave.GetY());
						break;
				}

				if (bloqueDeEnemigos.GetEnemigosRestantes() <= 0)
					this.nivelGanado = true;

				foreach (Timer timer in timers)
					timer.Actualizar();

				if (proyectil.Activo) {
					timerActualizarDisparo.Actualizar();
					if (timerActualizarDisparo.GetTicked())
						proyectil.Mover(false);
				}

				bloqueDeEnemigos.Dibujar();
				bloqueDeTorres.Dibujar();
				nave.Dibujar();

				if (ovni.GetActivo())
					ovni.Dibujar();

				if (timerBloque.GetTicked())
					bloqueDeEnemigos.Mover();

				if (timerMoverOvni.GetTicked())
					ovni.Mover();

				if (timerActualizarDisparoEnemigo.GetTicked())
					bloqueDeEnemigos.MoverProyectil();

				if (timerDisparoEnemigo.GetTicked())
					bloqueDeEnemigos.Disparar();

				if (bloqueDeEnemigos.ProyectilColisionaCon(nave))
					marcador.ConsumirVidar();

				if (bloqueDeEnemigos.ColisionaCon(bloqueDeTorres))
					marcador.ActualizarPuntuacion(-20);

				if (bloqueDeEnemigos.ColisionaCon(nave))
					break;

				if (bloqueDeTorres.Colisionan(proyectil))
					proyectil.Destruir();

				if (bloqueDeTorres.Colisionan(bloqueDeEnemigos.Proyectil))
					bloqueDeEnemigos.Proyectil.Destruir();

				if (proyectil.Activo) {
					if (bloqueDeEnemigos.ColisionaCon(proyectil, out Type tipo)) {
						if (tipo == typeof(Calamar))
							marcador.ActualizarPuntuacion(30);
						if (tipo == typeof(Cangrejo))
							marcador.ActualizarPuntuacion(20);
						if (tipo == typeof(Pulpo))
							marcador.ActualizarPuntuacion(10);
					}

					if (ovni.GetActivo() && ovni.Colisiona(proyectil)) {
						ovni.Destruir();
						proyectil.Destruir();
						marcador.ActualizarPuntuacion(50);
					}
				}

			} while (!this.nivelGanado && tecla != ConsoleKey.Escape && marcador.GetVidas() > 0);

			Pantalla.Limpiar();

			if (nivelGanado) {
				Partida partida = new Partida(false, this.nivel + 1);
				partida.Lanzar();
				return;
			}

			if (marcador.GetVidas() <= 0 || !nivelGanado)
				MostrarGameOver();

			if (tecla == ConsoleKey.Escape) {
				Juego juego = new Juego();
				juego.Lanzar();
			}
		}

		private void MostrarPausa()
		{
			Pantalla.TextoCentrado("J U E G O   P A U S A D O", 10);
			Pantalla.TextoCentrado("Pulsa F10 para reanudar", 12);
			Pantalla.TextoCentrado("Pulsa ESC para volver al menu", 14);
		}

		private void MostrarGameOver()
		{
			Timer timer = new Timer(1000);

			ConsoleKey tecla = ConsoleKey.None;
			do {
				if (Console.KeyAvailable)
					tecla = Console.ReadKey(true).Key;

				if (timer.GetTicked()) {
					Pantalla.ActualizarFondo();
					Pantalla.TextoCentrado("G A M E   O V E R", 10, ConsoleColor.Red);
					Pantalla.TextoCentrado("Pulsa Intro para volver o ESC para salir", 12);
				}

				timer.Actualizar();
			} while (tecla == ConsoleKey.None || (tecla != ConsoleKey.Escape && tecla != ConsoleKey.Enter));

			Pantalla.Limpiar();

			if (tecla == ConsoleKey.Enter) {
				Juego juego = new Juego();
				juego.Lanzar();
			}
		}
	}
}
