using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class BloqueDeTorres
	{
		private Torre[] torres = new Torre[3];

		public BloqueDeTorres()
		{
			int espacioEntreTorres = Pantalla.Ancho / (this.torres.Length + 1);
			int inicioX = espacioEntreTorres;

			for (int i = 0; i < this.torres.Length; i++) {
				this.torres[i] = new Torre(inicioX, Pantalla.Alto - 7);
				inicioX += espacioEntreTorres;
			}
		}

		public void Dibujar()
		{
			foreach (Torre torre in this.torres)
				torre.Dibujar();
		}

		public bool Colisionan(Sprite otro) {
			for (int i = 0; i < this.torres.Length; i++) {
				Torre torre = this.torres[i];
				for (int j = 0; j < torre.GetPiezas().GetLength(0); j++)
					for (int k = 0; k < torre.GetPiezas().GetLength(1); k++) {
						PiezaTorre piezaTorre = torre.GetPiezas()[j, k];
						if (!piezaTorre.Activo)
							continue;
						if (piezaTorre.Colisiona(otro)) {
							piezaTorre.Destruir();
							return true;
						}
					}
			}
			return false;
		}
	}
}
