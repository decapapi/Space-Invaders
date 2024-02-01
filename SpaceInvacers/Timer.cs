using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class Timer
	{
		private Dictionary<string, TimerInfo> timers = new Dictionary<string, TimerInfo>();

		public void Agregar(string nombre, int milisegundos)
		{
			if (timers.ContainsKey(nombre)) 
				return;

			long tickTime = milisegundos * TimeSpan.TicksPerMillisecond;
			this.timers[nombre] = new TimerInfo(tickTime, DateTime.Now.Ticks);
		}

		public void Actualizar()
		{
			DateTime tiempoActual = DateTime.Now;

			foreach (string nombre in timers.Keys.ToList()) {
				if (tiempoActual.Ticks - timers[nombre].UltimoTick < timers[nombre].TickTime) {
					this.timers[nombre].Ticked = false;
					continue;
				}

				this.timers[nombre].Ticked = true;
				this.timers[nombre].UltimoTick = tiempoActual.Ticks;
			}
		}

		public bool GetTicked(string nombre)
		{
			if (!timers.ContainsKey(nombre)) 
				return false;
			return timers[nombre].Ticked;
		}
	}
}
