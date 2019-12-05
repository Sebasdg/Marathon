using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Data {
	public class NewFinisher {
		public Participant ParticipantFinished;

		public NewFinisher(Participant ParticipantFinished) {
			this.ParticipantFinished = ParticipantFinished;
		}
	}
}
