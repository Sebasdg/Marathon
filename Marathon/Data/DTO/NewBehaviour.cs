using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marathon.Services;

namespace Marathon.Data {
	public class NewBehaviour {
		public GameBehaviourBase newBehaviour;

		public NewBehaviour(GameBehaviourBase newBehaviour) {
			this.newBehaviour = newBehaviour;
		}
	}
}
