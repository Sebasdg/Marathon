using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marathon;
using Marathon.Data;
using System.Reflection;

namespace Marathon.Services {
	class Engine {
		private List<GameBehaviourBase> behaviours = new List<GameBehaviourBase>();

		public static bool running;

		private readonly IEventAggregator eventAggregator;
		public Engine(IEventAggregator eventAggregator) {
			this.eventAggregator = eventAggregator;
			running = true;
			this.eventAggregator.GetEvent<NewBehaviour>().Subscribe(HandleNewBehaviour);
		}

		public void HandleNewBehaviour(NewBehaviour newBehaviour) {
			behaviours.Add(newBehaviour.newBehaviour);
		}

		// use system.reflection to get all start and update functions based on attributes

		public void Run() {
			Time.StartTime();
			Logger.LogEvent($"Game engine started.");

			for (int i = 0; i < behaviours.Count; i++) {
				behaviours[i].Start();
			}

			while (running) {
				//Logger.LogEvent($"FPS = {Time.deltaTime}");

				for (int i = 0; i < behaviours.Count; i++) {
					behaviours[i].Update();
				}

				Time.UpdateDeltaTime();
			}
		}
	}
}