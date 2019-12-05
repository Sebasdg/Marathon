using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marathon.Data;
using Marathon.Services;
using Autofac;

namespace Marathon.Services {
	class MarathonManager : GameBehaviourBase {
		public List<Participant> Participants = new List<Participant>();
		public List<PauseStopBase> PauseStops = new List<PauseStopBase>();

		private Participant winner;
		private int finishCount;

		public float RaceLength = 1;

		private readonly IEventAggregator eventAggregator;
		public MarathonManager(IEventAggregator eventAggregator) {
			this.eventAggregator = eventAggregator;
			eventAggregator.GetEvent<NewParticipant>().Subscribe(NewParticipant);
			eventAggregator.GetEvent<NewPauseStop>().Subscribe(NewPauseStop);
			eventAggregator.GetEvent<NewFinisher>().Subscribe(NewFinisher);
			eventAggregator.Publish(new NewBehaviour(this));
		}

		public MarathonManager setRaceLength(float RaceLength) {
			this.RaceLength = RaceLength;
			return this;
		}

		public override void Start() {
			for (int i = 0; i < Participants.Count; i++) {
				Participants[i].RaceLengthInMeters = RaceLength;
			}
		}

		public override void Update() {
			for (int i = 0; i < Participants.Count; i++) {
				if (Participants[i].Pausing) {
					continue;
				}

				for (int x = 0; x < PauseStops.Count; x++) {
					if (ParticipantArrivedAtPauseStop(Participants[i], PauseStops[x])){
						Participants[i].Pause();
						eventAggregator.Publish(new ParticipantPausing(Participants[i], PauseStops[x]));
					}
				}
			}

			if(finishCount == Participants.Count) {
				EndRace();
			}
		}

		private bool ParticipantArrivedAtPauseStop(Participant participant, PauseStopBase pauseStop) {
			if (participant.RaceProgress < pauseStop.PauseStopLocationInRace)
				return false;

			float length = Math.Abs(participant.RaceProgress - pauseStop.PauseStopLocationInRace);
			if (length < 1) {
				return true;
			}
			else {
				return false;
			}
		}

		public void NewParticipant(NewParticipant obj) {
			Participants.Add(obj.Participant);
		}

		public void NewPauseStop(NewPauseStop obj) {
			PauseStops.Add(obj.PauseStop);
		}

		public void StartRace() {
			Logger.LogMarathonStart();

			for (int i = 0; i < Participants.Count; i++) {
				Participants[i].StartRacing();
			}
		}

		public void NewFinisher(NewFinisher obj) {
			finishCount++;

			if (winner == null) {
				winner = obj.ParticipantFinished;
			}
		}

		void EndRace() {
			Logger.LogMarathonEnd(winner.ParticipantID.ToString());
			Engine.running = false;
		}
	}
}
