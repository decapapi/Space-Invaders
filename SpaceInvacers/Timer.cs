using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Timer
	{
		private Dictionary<string, long> ultimoTick = new Dictionary<string, long>();
		private Dictionary<string, long> tickTime = new Dictionary<string, long>();
		private Dictionary<string, bool> ticked = new Dictionary<string, bool>();

		public void Agregar(string nombre, int milisegundos)
		{
			tickTime[nombre] = milisegundos * TimeSpan.TicksPerMillisecond;
			ultimoTick[nombre] = DateTime.Now.Ticks;
			ticked[nombre] = true;
		}

		public void Actualizar()
		{
			DateTime tiempoActual = DateTime.Now;

			foreach (string nombre in ultimoTick.Keys.ToList()) {
				if (tiempoActual.Ticks - ultimoTick[nombre] < tickTime[nombre]) {
					ticked[nombre] = false;
					continue;
				}

				ticked[nombre] = true;
				ultimoTick[nombre] = tiempoActual.Ticks;
			}
		}

		public void SetTickTime(string nombre, int milisegundos)
		{
			if (!tickTime.ContainsKey(nombre)) 
				return;
			tickTime[nombre] = milisegundos * TimeSpan.TicksPerMillisecond;
		}

		public bool GetTicked(string nombre)
		{
			if (!ticked.ContainsKey(nombre)) 
				return false;
			return ticked[nombre];
		}
	}
}
