using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon {
	static class Time {
		private static Stopwatch sw = new Stopwatch();
		public static void StartTime() {
			sw.Start();
		}

		public static float time {
			get {
				return (float)sw.Elapsed.TotalSeconds;
			}
		}
		public static long TimeScale = 1;

		private static long PreviousElapsedMilliseconds;
		private static float _DeltaTime;
		public static float DeltaTime {
			get {
				PreviousElapsedMilliseconds = sw.ElapsedMilliseconds;
				return 0.001f * _DeltaTime * TimeScale;
			}
			private set {
				_DeltaTime = value;
			}
		}

		public static void UpdateDeltaTime() {
			DeltaTime = sw.ElapsedMilliseconds - PreviousElapsedMilliseconds;
		}
	}
}