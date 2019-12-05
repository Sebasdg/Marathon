using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marathon.Services;

namespace Marathon {
	public class FirstAidStation : PauseStopBase{
		public FirstAidStation(IEventAggregator eventAggregator) : base(eventAggregator) { }
	}
}
