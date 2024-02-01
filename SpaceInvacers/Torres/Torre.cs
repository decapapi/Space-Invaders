using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Torre
	{
		private PiezaTorre[,] piezas = new PiezaTorre[2, Pantalla.Ancho / 6];

		public Torre(int x, int y)
		{
			int inicioX = x - (this.piezas.GetLength(1) / 2);

			for (int i = 0; i < this.piezas.GetLength(0); i++)
				for (int j = 0; j < this.piezas.GetLength(1); j++)
					this.piezas[i, j] = new PiezaTorre(inicioX + j, y + i);
		}

		public void Dibujar()
		{
			for (int i = 0; i < this.piezas.GetLength(0); i++)
				for (int j = 0; j < this.piezas.GetLength(1); j++)
					if (this.piezas[i, j].Activo)
						this.piezas[i, j].Dibujar();
		}

		public PiezaTorre[,] GetPiezas()
		{
			return piezas;
		}
	}
}
