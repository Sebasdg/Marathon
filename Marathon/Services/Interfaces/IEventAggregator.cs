using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Services {
	public interface IEventAggregator {
		IObservable<TEvent> GetEvent<TEvent>() where TEvent : class;
		Task Publish(Object message);
	}
}
