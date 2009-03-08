using System;

namespace ManageDataAndTasks
{
	static class Output
	{
		public static void Wait()
		{
			Console.ReadKey(true);
		}

		public static void Error(Exception ex)
		{
			Error("Exception encountered");
			Exception inner = ex;
			while (inner.InnerException != null)
			{
				inner = inner.InnerException;
			}
			Write(ex.Message);
			Info("Press any key to view full exception informaiton");
			Wait();
			Write(ex.ToString());
		}

		static void Error(string message, params object[] args)
		{
			var color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine(message, args);
			Console.ForegroundColor = color;
		}


		public static void Warn(string message, params object[] args)
		{
			var color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine(message, args);
			Console.ForegroundColor = color;
		}

		public static void Write(string message, params object[] args)
		{
			var color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine(message, args);
			Console.ForegroundColor = color;
		}
		public static void Info(string message, params object[] args)
		{
			var color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(message, args);
			Console.ForegroundColor = color;
		}
	}
}