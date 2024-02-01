using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Sprite
	{
		public string Imagen { get; set; }
		public ConsoleColor Color { get; set; }

		protected int x;
		protected int y;

		public Sprite(int x, int y, string imagen, ConsoleColor color = ConsoleColor.White) : base ()
		{
			this.x = x;
			this.y = y;
			this.Imagen = imagen;
			this.Color = color;
		}

		public void Mover(int x, int y)
		{
			if (this.x == x && this.y == y)
				return;
			if (!EstaEntreLimites(x, y))
				return;

			this.Borrar();

			if (EstaEntreLimitesX(x))
				this.x = x;
			if (EstaEntreLimitesY(y))
				this.y = y;
		}

		public void Dibujar()
		{
			Console.ForegroundColor = this.Color;
			if (Console.GetCursorPosition().Left != this.x || Console.GetCursorPosition().Top != this.y)
				Console.SetCursorPosition(this.x, this.y);
			Console.Write(this.Imagen);
			Console.ResetColor();
		}

		public void Borrar()
		{
			Console.SetCursorPosition(this.x, this.y);
			Console.Write(new String(' ', this.Imagen.Length));
		}

		public bool Colisiona(Sprite otro)
		{
			if (this.y != otro.y)
				return false;

			return this.x <= otro.x + otro.Imagen.Length - 1 &&
					this.x + this.Imagen.Length - 1 >= otro.x;
		}

		public bool EstaEntreLimites(int x, int y)
		{
			return x >= 1 && x + this.Imagen.Length <= Pantalla.Ancho && y >= 0 && y <= Pantalla.Alto;
		}

		public bool EstaEntreLimitesX(int x)
		{
			return x >= 1 && x + this.Imagen.Length <= Pantalla.Ancho - 1;
		}

		public bool EstaEntreLimitesY(int y)
		{
			return y >= 1 && y <= Pantalla.Alto - 1;
		}

		public int GetX()
		{
			return this.x;
		}

		public int GetY()
		{
			return this.y;
		}
	}
}
