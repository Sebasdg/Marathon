using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Marathon.Services;
using Marathon.Data;
using Autofac;

namespace Marathon {
	public class Participant : GameBehaviourBase, IStartable {
		public int ParticipantID;

		public bool Pausing;
		public float PauseProgress = 0;
		public float RaceLengthInMeters = 100;
		public float RaceProgress = 0;

		private float RunSpeed = 1;
		private bool StartedRacing;
		private bool IsFinished;

		private readonly IEventAggregator eventAggregator;
		public Participant(IEventAggregator eventAggregator) {
			this.eventAggregator = eventAggregator;
			eventAggregator.Publish(new NewBehaviour(this));
			eventAggregator.Publish(new NewParticipant(this));
		}

		public Participant SetRunSpeed(int runSpeed) {
			this.RunSpeed = runSpeed / 100f;
			return this;
		}
		public Participant SetID(int iD) {
			this.ParticipantID = iD;
			return this;
		}

		//public override void Start() {
		//	Logger.LogEvent($"New participant has entered the race! id: {ParticipantID} rnspeed: {RunSpeed}");
		//}

		public override void Update() {
			if (!IsFinished && StartedRacing) {
				if (Pausing)
					return;

				if (RaceProgress >= RaceLengthInMeters) {
					FinishRacing();
				}
				else {
					RaceProgress += (RunSpeed * 30) * Time.DeltaTime;
					//Logger.LogEvent($"Participant {RaceProgress}");
				}
			}
		}

		public void Pause() {
			Pausing = true;
			PauseProgress = 0;
		}

		public void UnPause() {
			RaceProgress += 1;
			Pausing = false;
			PauseProgress = 0;
		}

		public void StartRacing() {
			Logger.LogEvent($"Participant {ParticipantID} has started running");
			StartedRacing = true;
		}

		public void FinishRacing() {
			//IsRacing = false;
			IsFinished = true;
			Logger.LogEvent($"Participant {ParticipantID} has crossed the finish line in {Time.time.ToString("F4")} Seconds");
			eventAggregator.Publish(new NewFinisher(this));
		}
	}
}
