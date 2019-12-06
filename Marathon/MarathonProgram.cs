using System;
using Autofac;
using Marathon.Services;
using System.Collections.Generic;

namespace Marathon {
	class MarathonProgram {
		public MarathonProgram() {
			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterType<EventAggregator>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<Engine>().AsSelf().SingleInstance();
			builder.RegisterType<MarathonManager>().AsSelf().SingleInstance();
			builder.RegisterType<Participant>().AsSelf().InstancePerDependency();
			builder.RegisterType<DrinkingStation>().AsSelf().InstancePerDependency();

			IContainer container = builder.Build();
			Engine engine = container.Resolve<Engine>();

			MarathonManager marathonManager = container.Resolve<MarathonManager>();
			marathonManager.setRaceLength(100);

			DrinkingStation pauseStop1 = container.Resolve<DrinkingStation>();
			pauseStop1.SetLenghtInSeconds(3).SetLocationInRace(10).SetMaxPausable(2).SetName("1");
			Logger.LogEvent($"New stop added to the race! name: {pauseStop1.Name}  pos: {pauseStop1.PauseStopLocationInRace}  length: {pauseStop1.PauseLenghtInSeconds}");
			DrinkingStation pauseStop2 = container.Resolve<DrinkingStation>();
			pauseStop2.SetLenghtInSeconds(3).SetLocationInRace(50).SetMaxPausable(3).SetName("2");
			Logger.LogEvent($"New stop added to the race! name: {pauseStop2.Name}  pos: {pauseStop2.PauseStopLocationInRace}  length: {pauseStop2.PauseLenghtInSeconds}");
			DrinkingStation pauseStop3 = container.Resolve<DrinkingStation>();
			pauseStop3.SetLenghtInSeconds(2).SetLocationInRace(70).SetMaxPausable(2).SetName("3");
			Logger.LogEvent($"New stop added to the race! name: {pauseStop3.Name}  pos: {pauseStop3.PauseStopLocationInRace}  length: {pauseStop3.PauseLenghtInSeconds}");

			Random rnd = new Random(DateTime.Now.Second + DateTime.Now.Millisecond);
			for (int i = 0; i < 10; i++) {
				int runspeed = rnd.Next(50, 150);
				int iD = i;

				Participant participant = container.Resolve<Participant>();
				participant.SetRunSpeed(runspeed).SetID(iD);
				Logger.LogEvent($"New participant has entered the race! id: {iD} rnspeed: {runspeed}");
			}

			marathonManager.StartRace();
			Time.TimeScale = 10;

			//needs to run as last
			engine.Run();
		}
	}
}
