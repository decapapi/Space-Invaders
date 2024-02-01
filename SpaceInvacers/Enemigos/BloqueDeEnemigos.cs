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
		private Enemigo[,] enemigos = new Enemigo[3, Pantalla.Ancho / 6];
		private bool moviendoIzquierda = false;
		private bool bajar = false;
		private int enemigosRestantes;
		private Random random = new Random();

		public Proyectil Proyectil { get; }

		public BloqueDeEnemigos()
		{
			this.enemigosRestantes = enemigos.GetLength(0) * enemigos.GetLength(1);

			int inicioX = (Pantalla.Ancho - (enemigos.GetLength(1) * 3)) / 2;

			for (int i = 0; i < enemigos.GetLength(1); i++) {
				int x = inicioX + i * 3;
				this.enemigos[0, i] = new Calamar(x, 6);
				this.enemigos[1, i] = new Cangrejo(x, 7);
				this.enemigos[2, i] = new Pulpo(x, 8);
			}
			this.Proyectil = new Proyectil();
		}

		public void Mover()
		{
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

			bool puedeMoverDerecha = ultimoEnemigo.GetX() < (Pantalla.Ancho - ultimoEnemigo.Imagen.Length) - 1;
			bool puedeMoverIzquierda = primerEnemigo.GetX() > 1;

			if (!puedeMoverDerecha || !puedeMoverIzquierda)
			{
				this.bajar = !this.bajar;
				this.moviendoIzquierda = !puedeMoverDerecha;
			}

			for (int i = 0; i < this.enemigos.GetLength(0); i++) {
				for (int j = 0; j < this.enemigos.GetLength(1); j++) {
					if (this.bajar) {
						this.enemigos[i, j].Bajar();
						continue;
					}
					if (this.moviendoIzquierda)
						this.enemigos[i, j].MoverIzquierda();
					else
						this.enemigos[i, j].MoverDerecha();
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
			if (this.Proyectil.Activo)
				return;

			int fila, columna;
			do {
				fila = random.Next(0, enemigos.GetLength(0));
				columna = random.Next(0, enemigos.GetLength(1));
			} while (!this.enemigos[fila, columna].Activo);

			Enemigo enemigo = this.enemigos[fila, columna];

			this.Proyectil.Disparar(enemigo.GetX(), enemigo.GetY());
		}

		public bool ColisionaCon(Proyectil proyectil, out Type tipo)
		{
			tipo = null;
			for (int i = 0; i < this.enemigos.GetLength(0); i++)
				for (int j = 0; j < this.enemigos.GetLength(1); j++)
					if (this.enemigos[i, j].Activo)
						if (proyectil.Colisiona(this.enemigos[i, j])) {
							tipo = this.enemigos[i, j].GetType();
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
