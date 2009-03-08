using System;
using System.Linq;
using Lokad.Api;

namespace ManageDataAndTasks
{
	class Program
	{
		const string serviceUrl = "http://sandbox-ws.lokad.com/TimeSeries2.asmx";

		static void Main()
		{
			Output.Write("Connecting to '{0}'", serviceUrl);
			Output.Info("Please enter your Lokad username and press [Enter]");
			var login = Console.ReadLine();
			Output.Info("Please enter the corresponding password and press [Enter]");
			var pwd = Console.ReadLine();

			try
			{
				var lokadService = ServiceFactory.GetConnectorForTesting(login, pwd, serviceUrl);

				Output.Info("Reading tasks...");
				DisplayTasks(lokadService);

				Output.Info("Reading series...");
				DisplaySeries(lokadService);

				Output.Info("Adding new series...");
				var newSeries = CreateSampleSeries(lokadService);

				DisplaySeries(lokadService);

				Output.Info("Cleaning up...");
				lokadService.DeleteSeries(newSeries);
			}
			catch (Exception ex)
			{
				Output.Error(ex);
			}
			Output.Info("Press any key to exit");
			Output.Wait();
		}

		static void DisplayTasks(ILokadService service)
		{
			var tasks = service.GetTasks();
			var series = service.GetSeries().ToDictionary(s => s.SerieID);

			foreach (var task in tasks)
			{
				var serieForTask = series[task.SerieID];
				Console.WriteLine("Task for {0}: {1} x {2}", serieForTask.Name, task.FuturePeriods, task.Period);
			}
		}

		static SerieInfo[] CreateSampleSeries(ILokadService lokadService)
		{
			var series = TestHelper.CreateSeries(10);

			lokadService.AddSeries(series);
			lokadService.UpdateSerieSegments(TestHelper.CreateValues(series, 100));
			lokadService.SetTags(TestHelper.CreateTags(series));
			lokadService.SetEvents(TestHelper.CreateEvents(series,4));

			var tasks = TestHelper.CreateTasks(series);
			lokadService.AddTasks(tasks);

			return series;
		}

		public static void DisplaySeries(ILokadService api)
		{
			var series = api.GetSeries();
			
			var tags = api.GetTags(series).ToDictionary(t => t.SerieID);
			var events = api.GetEvents(series).ToDictionary(t => t.SerieID);
			series.ForEach(s =>
				{
					Console.WriteLine(s.Name);
					if (tags.ContainsKey(s.SerieID))
						Console.WriteLine("   Tagged: {0}", tags[s.SerieID].Tags.Join(", "));
					if (events.ContainsKey(s.SerieID))
						Console.WriteLine("   With {0} events", events[s.SerieID].Events.Length);
				});
		}
	}
}