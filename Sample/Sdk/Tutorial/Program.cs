using System;
using Lokad.Api;

namespace Tutorial
{
	class Program
	{
		static void Main()
		{
			var serviceUrl = "http://ws.lokad.com/TimeSeries2.asmx";
			var login = "your.login@gmail.com"; // your Sandbox login here
			var pwd = "yourpassword"; // your Sandbox password here

			var service = ServiceFactory
				.GetConnectorForTesting(login, pwd, serviceUrl);

			var existingSeries = service.GetSeries();
			service.DeleteSeries(existingSeries);

			var serie1 = new SerieInfo
			{
				Name = "MySerie1"
			};
			var serie2 = new SerieInfo
			{
				Name = "MySerie2"
			};

			var mySeries = new[] {serie1, serie2};

			Console.WriteLine("Saving series...");
			service.AddSeries(mySeries);

			// add values
			var value1 = new TimeValue
			{
				Time = new DateTime(2008, 7, 1),
				Value = 10
			};
			var value2 = new TimeValue
			{
				Time = new DateTime(2008, 7, 2),
				Value = 12
			};
			var value3 = new TimeValue
			{
				Time = new DateTime(2008, 7, 3),
				Value = 11
			};
			// create association between serie1 and values 1,2,3
			var segment1 = new SegmentForSerie(serie1, new[] {value1, value2, value3});
			// create association between serie2 and values 1,2
			var segment2 = new SegmentForSerie(serie2, new[] {value1, value2});

			Console.WriteLine("Saving values...");
			service.UpdateSerieSegments(new[] {segment1, segment2});

			// create new forecasting task
			// to create 3 days forecast with daily interval
			var task = new TaskInfo(serie1)
			{
				FuturePeriods = 3,
				Period = Period.Day
			};

			Console.WriteLine("Saving Tasks...");
			service.AddTasks(new[] {task});

			Console.WriteLine("Retrieving forecasts...");
			var forecasts = service.GetForecasts(new[] {task});

			foreach (var forecast in forecasts)
			{
				Console.WriteLine("Forecast for task {0}", forecast.TaskID);
				foreach (var value in forecast.Values)
				{
					Console.WriteLine("  {0} - {1}", value.Time.ToShortDateString(), value.Value);
				}
			}

			Console.WriteLine("Press any key to continue");
			Console.ReadKey(true);
		}
	}
}