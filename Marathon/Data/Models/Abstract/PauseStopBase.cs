using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marathon.Data;
using Marathon.Services;
using Autofac;

namespace Marathon {
	public abstract class PauseStopBase : GameBehaviourBase, IStartable {
		public string Name = "";
		public int MaxPausable = 0;
		public float PauseStopLocationInRace = 1;
		public float PauseLenghtInSeconds = 5;

		public List<Participant> ParticipantsPausing = new List<Participant>();
		public List<Participant> ParticipantsInQue = new List<Participant>();

		private readonly IEventAggregator eventAggregator;
		public PauseStopBase(IEventAggregator eventAggregator) {
			this.eventAggregator = eventAggregator;
			this.eventAggregator.GetEvent<ParticipantPausing>().Subscribe(HandleNewParticipant);
			eventAggregator.Publish(new NewBehaviour(this));
			eventAggregator.Publish(new NewPauseStop(this));
		}

		public PauseStopBase SetName(string Name) {
			this.Name = Name;
			return this;
		}

		public PauseStopBase SetMaxPausable(int MaxPausable) {
			this.MaxPausable = MaxPausable;
			return this;
		}

		public PauseStopBase SetLocationInRace(int PauseStopLocationInRace) {
			this.PauseStopLocationInRace = PauseStopLocationInRace;
			return this;
		}

		public PauseStopBase SetLenghtInSeconds(int PauseLenghtInSeconds) {
			this.PauseLenghtInSeconds = PauseLenghtInSeconds;
			return this;
		}

		public void HandleNewParticipant(ParticipantPausing obj) {
			if(obj.PauseStop != this) 
				return;
		
			if(ParticipantsPausing.Count <= MaxPausable) {
				ParticipantsPausing.Add(obj.Participant);
				Logger.LogEvent($"Participant {obj.Participant.ParticipantID} arrived at PauseStop {Name}");
			}
			else {
				ParticipantsInQue.Add(obj.Participant);
				Logger.LogEvent($"Participant {obj.Participant.ParticipantID} arrived at que for PauseStop {Name}");
			}

			obj.Participant.Pause();
		}

		//public override void Start() {
		//	Logger.LogEvent($"New {this.GetType().Name} added to the race! name: {Name}  pos: {PauseStopLocationInRace}  length: {PauseLenghtInSeconds}");
		//}

		public override void Update() {
			List<Participant> participantsToRemove = new List<Participant>();

			for (int i = 0; i < ParticipantsPausing.Count; i++) {
				//extra null check because there is a bug that randomly apears that gives an exception error on line 76
				if (ParticipantsPausing[i] == null)
					continue;

				if (ParticipantsPausing[i].PauseProgress <= PauseLenghtInSeconds) {
					ParticipantsPausing[i].PauseProgress += Time.DeltaTime;
				}
				else {
					participantsToRemove.Add(ParticipantsPausing[i]);
				}
			}

			foreach(Participant participant in participantsToRemove) {
				UnPauseParticipant(participant);
			}

			if (ParticipantsPausing.Count <= MaxPausable) {
				MoveParticipantFromQueToPause();
			}
		}

		private void UnPauseParticipant(Participant participant) {
			Logger.LogEvent($"Participant {participant.ParticipantID} left from PauseStop {this.GetType().Name} {Name}");
			participant.UnPause();
			ParticipantsPausing.Remove(participant);
		}

		private void MoveParticipantFromQueToPause() {
			if (ParticipantsInQue.Count == 0)
				return;

			Participant participant = ParticipantsInQue[0];
			ParticipantsPausing.Add(participant);
			ParticipantsInQue.Remove(participant);
			Logger.LogEvent($"Participant {participant.ParticipantID} went from PauseStop {Name} Que to Pausing");
		}
	}
}
