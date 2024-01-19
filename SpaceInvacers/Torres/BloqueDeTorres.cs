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
			for (int i = 0; i < this.torres.Length; i++)
				this.torres[i] = new Torre(10 + (i * 15), 19);
		}

		public void Dibujar()
		{
			foreach (Torre torre in this.torres)
				torre.Dibujar();
		}
	}
}
