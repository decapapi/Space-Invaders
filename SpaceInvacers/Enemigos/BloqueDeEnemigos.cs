using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SpaceInvacers
{
    class BloqueDeEnemigos
	{
		private Enemigo[,] enemigos = new Enemigo[3, 10];
		private bool moviendoIzquierda = false;
		private bool bajar = false;
		private int enemigosRestantes;

		public Proyectil Proyectil { get; }

		public BloqueDeEnemigos()
		{
			this.enemigosRestantes = enemigos.GetLength(0) * enemigos.GetLength(1);

			for (int i = 0; i < enemigos.GetLength(1); i++) {
				int x = 15 + i * 3;
				this.enemigos[0, i] = new Calamar(x, 6);
				this.enemigos[1, i] = new Cangrejo(x, 7);
				this.enemigos[2, i] = new Pulpo(x, 8);
			}
			this.Proyectil = new Proyectil();
		}

		public void Mover()
		{
			bool puedeMoverDerecha = !this.moviendoIzquierda;
			bool puedeMoverIzquierda = true;

			if (this.enemigosRestantes <= 0)
				return;

			Enemigo primerEnemigo = null;
			Enemigo ultimoEnemigo = null;

			for (int i = 0; i < this.enemigos.GetLength(0); i++) {
				for (int j = 0; j < this.enemigos.GetLength(1); j++) {
					if (this.enemigos[i, j].Activo) {
						primerEnemigo = this.enemigos[i, j];
						ultimoEnemigo = this.enemigos[i, j];
						break;
					}
				}
				if (primerEnemigo != null)
					break;
			}

			for (int i = 0; i < this.enemigos.GetLength(0); i++) {
				for (int j = 0; j < this.enemigos.GetLength(1); j++) {
					if (this.enemigos[i, j].Activo) {
						if (this.enemigos[i, j].GetX() < primerEnemigo.GetX())
							primerEnemigo = this.enemigos[i, j];
						if (this.enemigos[i, j].GetX() > ultimoEnemigo.GetX())
							ultimoEnemigo = this.enemigos[i, j];
					}
				}
			}

			if (ultimoEnemigo.GetX() >= (Pantalla.SizeX - ultimoEnemigo.Imagen.Length) - 1) {
				puedeMoverDerecha = false;
				this.bajar = !this.bajar;
			}

			if (primerEnemigo.GetX() == 1) {
				this.moviendoIzquierda = false;
				puedeMoverIzquierda = false;
				puedeMoverDerecha = true;
				this.bajar = !this.bajar;
			}

			for (int i = 0; i < this.enemigos.GetLength(0); i++) {
				for (int j = 0; j < this.enemigos.GetLength(1); j++) {
					if (this.bajar) {
						this.enemigos[i, j].Bajar();
						continue;
					}
					if (puedeMoverDerecha && !this.moviendoIzquierda)
						this.enemigos[i, j].MoverDerecha();
					else if (puedeMoverIzquierda) {
						this.moviendoIzquierda = true;
						this.enemigos[i, j].MoverIzquierda();
					}

				}
			}
		}

		public void MoverProyectil()
		{
			if (this.Proyectil.Activo)
				this.Proyectil.Mover(true);
		}

		public void Disparar()
		{
			Random random = new Random();

			int fila, columna;
			do {
				fila = random.Next(0, enemigos.GetLength(0));
				columna = random.Next(0, enemigos.GetLength(1));
			} while (!this.enemigos[fila, columna].Activo);

			Enemigo enemigo = this.enemigos[fila, columna];

			if (!this.Proyectil.Activo)
				this.Proyectil.Disparar(enemigo.GetX(), enemigo.GetY());
		}

		public bool ColisionaCon(Proyectil proyectil)
		{
			for (int i = 0; i < this.enemigos.GetLength(0); i++)
				for (int j = 0; j < this.enemigos.GetLength(1); j++)
					if (this.enemigos[i, j].Activo)
						if (proyectil.Colisiona(this.enemigos[i, j])) {
							this.enemigos[i, j].Destruir();
							this.enemigosRestantes--;
							proyectil.Destruir();
							return true;
						}
			return false;
		}

		public bool ColisionaCon(Nave nave)
		{
			for (int i = 0; i < this.enemigos.GetLength(0); i++)
				for (int j = 0; j < this.enemigos.GetLength(1); j++)
					if (this.enemigos[i, j].Activo)
						if (nave.Colisiona(this.enemigos[i, j]))
							return true;
			return false;
		}

		public bool ColisionaCon(BloqueDeTorres torres) {
			for (int i = 0; i < this.enemigos.GetLength(0); i++)
				for (int j = 0; j < this.enemigos.GetLength(1); j++) {
					if (this.enemigos[i, j].Activo)
						torres.Colisionan(this.enemigos[i, j]);
				}
			return false;
		}

		public bool ProyectilColisionaCon(Nave nave)
		{
			if (this.Proyectil.Activo)
				if (this.Proyectil.Colisiona(nave)) {
					this.Proyectil.Destruir();
					return true;
				}
			return false;
		}

		public void Dibujar()
		{
			for (int i = 0; i < this.enemigos.GetLength(0); i++)
				for (int j = 0; j < this.enemigos.GetLength(1); j++)
					if (this.enemigos[i, j].Activo)
						this.enemigos[i, j].Dibujar();
		}

		public int GetEnemigosRestantes()
		{
			return this.enemigosRestantes;
		}
	}
}
