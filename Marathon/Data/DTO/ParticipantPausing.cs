using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Data {
	public class ParticipantPausing {
		public Participant Participant;
		public PauseStopBase PauseStop;

		public ParticipantPausing(Participant Participant, PauseStopBase PauseStop) {
			this.Participant = Participant;
			this.PauseStop = PauseStop;
		}
	}
}
