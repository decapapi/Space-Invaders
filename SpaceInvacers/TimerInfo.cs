using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvacers
{
	class TimerInfo
	{
		public long TickTime { get; set; }
		public long UltimoTick { get; set; }
		public bool Ticked { get; set; } = true;

		public TimerInfo(long tickTime, long ultimoTick)
		{
			this.TickTime = tickTime;
			this.UltimoTick = ultimoTick;
		}
	}
}
