using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	internal class Timer
	{
		private long ultimoTick;
		private long tickTime;
		private bool ticked;

		public Timer(int milisegundos)
		{
			tickTime = milisegundos * TimeSpan.TicksPerMillisecond;
			ultimoTick = DateTime.Now.Ticks;
		}

		public Timer() : this(500) { }

		public void Actualizar()
		{
			DateTime timpoActual = DateTime.Now;

			if (timpoActual.Ticks - this.ultimoTick < tickTime)	{
				this.ticked = false;
				return;
			}

			this.ticked = true;

			ultimoTick = timpoActual.Ticks;
		}

		public void SetTickTime(int milisegundos)
		{
			tickTime = milisegundos * TimeSpan.TicksPerMillisecond;
		}

		public bool GetTicked()
		{ 
			return ticked;
		}
	}
}
