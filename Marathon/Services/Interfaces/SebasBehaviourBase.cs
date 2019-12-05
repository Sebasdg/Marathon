using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marathon.Services;

namespace Marathon{
	public abstract class GameBehaviourBase : IGameBehaviour {
		public virtual void Start() { }
		public virtual void Update() { }
	}
}
