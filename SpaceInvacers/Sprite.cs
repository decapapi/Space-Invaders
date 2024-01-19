﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
			if (this.y != otro.y) return false;

			if (this.x <= otro.x + otro.Imagen.Length - 1 &&
				this.x + this.Imagen.Length - 1 >= otro.x)
				return true;

			return false;
		}

		public bool EstaEntreLimites(int x, int y)
		{
			return x >= 1 && x + this.Imagen.Length <= Console.BufferWidth && y >= 0 && y <= Console.BufferHeight;
		}

		public bool EstaEntreLimitesX(int x)
		{
			return x >= 1 && x + this.Imagen.Length <= Console.BufferWidth - 1;
		}

		public bool EstaEntreLimitesY(int y)
		{
			return y >= 1 && y <= Console.BufferHeight - 1;
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
