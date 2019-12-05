using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;

namespace Marathon.Services {
	class EventAggregator : IEventAggregator {
		readonly Subject<object> _subject = new Subject<object>();

		public IObservable<TEvent> GetEvent<TEvent>() where TEvent : class {
			return _subject.OfType<TEvent>().ObserveOn(new NewThreadScheduler()).AsObservable();
		}

		public Task Publish(object message) {
			_subject.OnNext(message);
			return Task.CompletedTask;
		}
	}
}
