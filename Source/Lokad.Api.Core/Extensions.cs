#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;

namespace Lokad.Api
{
	static class Extensions
	{
		public static IEnumerable<TaskInfo> GetTasks(IForecastApi self, Identity identity, int pageSize)
		{
			var cursor = Guid.Empty;

			while (true)
			{
				var page = self.GetTasks(identity, cursor, pageSize);

				cursor = page.Cursor;

				foreach (var task in page.Tasks)
				{
					yield return task;
				}

				if (!page.ThereAreMorePages)
					yield break;
			}
		}

		public static IEnumerable<SerieInfo> GetSerieInfos(ITimeSerieApi self, Identity identity, int pageSize)
		{
			var cursor = Guid.Empty;

			while (true)
			{
				var page = self.GetSeries(identity, cursor, pageSize);

				cursor = page.Cursor;

				foreach (var task in page.Series)
				{
					yield return task;
				}

				if (!page.ThereAreMorePages)
					yield break;
			}
		}
	}
}