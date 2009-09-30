#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using log4net.Appender;
using log4net.Filter;

namespace Lokad.Logging
{
	/// <summary>
	/// Syntax for configuring logging within the logging stack
	/// </summary>
	public class LogSyntax : Syntax
	{
		readonly AppenderSkeleton _skeleton;

		internal LogSyntax(AppenderSkeleton skeleton)
		{
			_skeleton = skeleton;
		}

		/// <summary>
		/// Appends inclusive filter to the log
		/// </summary>
		/// <param name="minRange">The min range.</param>
		/// <param name="maxRange">The max range.</param>
		/// <returns>same syntax</returns>
		public LogSyntax Filter(LogLevel minRange, LogLevel maxRange)
		{
			var filter = new LevelRangeFilter
				{
					LevelMin = minRange.ToLog4Net(),
					LevelMax = maxRange.ToLog4Net(),
					AcceptOnMatch = true
				};
			filter.ActivateOptions();
			_skeleton.AddFilter(filter);
			//_skeleton.ActivateOptions();
			return this;
		}
	}
}