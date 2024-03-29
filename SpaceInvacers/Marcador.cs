﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Marcador
	{
		private int puntuacion;
		private int highScore;
		private int vidas;

		public Marcador()
		{
			this.puntuacion = 0;
			this.vidas = 3;
			int highScore = Configuracion.GetInt32("Highscore");
			this.highScore = highScore > -1 ? highScore : 0;
			this.Actuallizar();
		}

		public void Actuallizar()
		{
			this.MostrarPuntuacion();
			this.MostrarVidas();
			Pantalla.Texto("Volver: ESC", Pantalla.Ancho - 13, Pantalla.Alto - 1);
			Pantalla.TextoCentrado("Pausa: F10", Pantalla.Alto - 1);
		}

		public void ActualizarPuntuacion(int puntos)
		{
			this.puntuacion += puntos;
			if (this.puntuacion > this.highScore) {
				Configuracion.Guardar("Highscore", this.puntuacion);
				this.highScore = this.puntuacion;
			}
			this.Actuallizar();
		}

		public void ConsumirVidar()
		{
			if (this.vidas > 0)
				this.vidas--;
			this.Actuallizar();
		}

		public int GetVidas()
		{
			return this.vidas;
		}

		private void MostrarPuntuacion()
		{
			Pantalla.Texto("SCORE <1>", 3, 1);
			Pantalla.TextoCentrado("HI-SCORE", 1);
			Pantalla.Texto("SCORE <2>", Pantalla.Ancho - 12, 1);

			Pantalla.Texto(this.puntuacion.ToString("D5"), 4, 2); 
			Pantalla.TextoCentrado(highScore.ToString("D5"), 2);
		}

		private void MostrarVidas()
		{
			Pantalla.Texto(this.vidas.ToString(), 2, Pantalla.Alto - 1);
			Pantalla.Texto(new String(' ', 9), 3, Pantalla.Alto - 1);
			string vidasString = "";
			for (int i = 1; i < this.vidas; i++)
				vidasString += "\u15B9\u15BA ";
			Pantalla.Texto(vidasString, 4, Pantalla.Alto - 1, ConsoleColor.Green);
		}
	}
}
