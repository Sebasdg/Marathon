using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Data {
	public class NewParticipant {
		public Participant Participant;

		public NewParticipant(Participant Participant) {
			this.Participant = Participant;
		}
	}
}
