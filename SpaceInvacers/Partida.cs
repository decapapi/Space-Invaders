﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
    class Partida
	{
		private bool pausa;
		private bool salir;
		private bool nivelGanado;
		private int nivel;
		private static Marcador marcador;

		public Partida() : this(true, 1) { }

		public Partida(bool nuevaPartida, int nivel)
		{
			this.nivel = nivel;
			this.pausa = false;
			this.salir = false;
			this.nivelGanado = false;
			this.MostrarIntro();
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

			Timer timer = new Timer();

			timer.Agregar("moverBloque", 800 / this.nivel);
			timer.Agregar("moverOvni", 150 / this.nivel);
			timer.Agregar("disparoEnemigo", 1500 / this.nivel);
			timer.Agregar("actualizarDisparo", 60);
			timer.Agregar("actualizarDisparoEnemigo", 40);

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

				if (this.pausa) {
					if (tecla == ConsoleKey.Escape)
						this.salir = Pantalla.Confirmacion("¿Seguro que quieres salir?");
					if (!this.salir)
						MostrarPausa();
					continue;
				}

				switch (tecla) {
					case ConsoleKey.LeftArrow: nave.MoverIzquierda(); break;
					case ConsoleKey.RightArrow: nave.MoverDerecha(); break;
					case ConsoleKey.Spacebar:
						if (!proyectil.Activo)
							proyectil.Disparar(nave.GetX(), nave.GetY());
						break;
					case ConsoleKey.Escape:
						this.salir = Pantalla.Confirmacion("¿Seguro que quieres salir?");
						marcador.Actuallizar();
						break;
				}

				if (bloqueDeEnemigos.GetEnemigosRestantes() <= 0)
					this.nivelGanado = true;

				timer.Actualizar();

				if (proyectil.Activo) {
					if (timer.GetTicked("actualizarDisparo"))
						proyectil.Mover(false);
				}

				bloqueDeEnemigos.Dibujar();
				bloqueDeTorres.Dibujar();
				nave.Dibujar();

				if (ovni.GetActivo())
					ovni.Dibujar();

				if (timer.GetTicked("moverBloque"))
					bloqueDeEnemigos.Mover();

				if (timer.GetTicked("moverOvni"))
					ovni.Mover();

				if (timer.GetTicked("actualizarDisparoEnemigo"))
					bloqueDeEnemigos.MoverProyectil();

				if (timer.GetTicked("disparoEnemigo"))
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

			} while (!this.nivelGanado && !this.salir && marcador.GetVidas() > 0);

			Pantalla.Limpiar();

			if (nivelGanado) {
				new Partida(false, this.nivel + 1).Lanzar();
				return;
			}

			if (marcador.GetVidas() <= 0)
				MostrarGameOver();

			if (this.salir)
				new Juego().Lanzar();
		}

		private void MostrarIntro()
		{
			Timer timer = new Timer();
			timer.Agregar("fondo", 1000);

			ConsoleKey tecla = ConsoleKey.None;
			do
			{
				if (Console.KeyAvailable)
					tecla = Console.ReadKey(true).Key;

				if (timer.GetTicked("fondo")) {
					Pantalla.ActualizarFondo();
					Pantalla.TextoCentrado("S P A C E   I N V A D E R S", Pantalla.PosTextoY(-3));
					Pantalla.TextoCentrado("* TABLA DE PUNTUACIÓN *", Pantalla.PosTextoY());
					Pantalla.TextoCentrado("\u0F3A\u1d16\u0F3B = ? MISTERY  ", Pantalla.PosTextoY(1));
					Pantalla.TextoCentrado("\u15A7\u15A8  = 30 POINTS", Pantalla.PosTextoY(2));
					Pantalla.TextoCentrado("\u14FF\u1502  = 20 POINTS", Pantalla.PosTextoY(3));
					Pantalla.TextoCentrado("\u1578\u157A  = 10 POINTS", Pantalla.PosTextoY(4));
					Pantalla.TextoCentrado("Pulsa cualquier tecla para iniciar", Pantalla.PosTextoY(7));
				}

				timer.Actualizar();
			} while (tecla == ConsoleKey.None);

			Pantalla.Limpiar();
		}

		private void MostrarPausa()
		{
			Pantalla.TextoCentrado("J U E G O   P A U S A D O", Pantalla.PosTextoY());
			Pantalla.TextoCentrado("Pulsa F10 para reanudar", Pantalla.PosTextoY(2));
			Pantalla.TextoCentrado("Pulsa ESC para volver al menu", Pantalla.PosTextoY(4));
		}

		private void MostrarGameOver()
		{
			Timer timer = new Timer();
			timer.Agregar("fondo", 1000);

			ConsoleKey tecla = ConsoleKey.None;
			do {
				if (Console.KeyAvailable)
					tecla = Console.ReadKey(true).Key;

				if (timer.GetTicked("fondo")) {
					Pantalla.ActualizarFondo();
					Pantalla.TextoCentrado("G A M E   O V E R", Pantalla.PosTextoY(), ConsoleColor.Red);
					Pantalla.TextoCentrado("Pulsa Intro para volver o ESC para salir", Pantalla.PosTextoY(2));
				}

				timer.Actualizar();
			} while (tecla == ConsoleKey.None || (tecla != ConsoleKey.Escape && tecla != ConsoleKey.Enter));

			Pantalla.Limpiar();

			if (tecla == ConsoleKey.Enter)
				new Juego().Lanzar(); ;
		}
	}
}
