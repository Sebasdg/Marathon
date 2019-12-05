using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marathon.Services {
	static class Logger {

		public static void LogEvent(string message) {
			//Console.WriteLine($@"[{DateTime.Now}] : {message}");

			Console.ForegroundColor = ConsoleColor.Green;
			Console.Write($"[{ DateTime.Now}]");
			Console.ResetColor();

			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write(" : ");
			Console.ResetColor();

			//Console.Write($"{message}");
			Console.WriteLine(message);

		}

		public static void LogMarathonStart() {
			for (int i = 0; i < 3; i++) {
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write($"[{ DateTime.Now}]");
				Console.ResetColor();

				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write(" : ");
				Console.ResetColor();

				if (i == 1)
					Console.WriteLine("==================== Race has started ====================");
				if (i == 0 || i == 2)
					Console.WriteLine("==========================================================");
			}
		}

		public static void LogMarathonEnd(string winnerName) {
			for (int i = 0; i < 2; i++) {
				Console.ForegroundColor = ConsoleColor.Green;
				Console.Write($"[{ DateTime.Now}]");
				Console.ResetColor();

				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write(" : ");
				Console.ResetColor();

				if(i == 0)
					Console.WriteLine("================================================================== Race has ended");
				if(i == 1)
					Console.WriteLine($"================================================================== {winnerName} won");
			}
		}
	}
}
