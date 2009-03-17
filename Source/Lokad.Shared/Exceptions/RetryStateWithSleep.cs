#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using Lokad.Quality;

namespace Lokad.Exceptions
{
	[Immutable]
	sealed class RetryStateWithSleep : IRetryState
	{
		readonly IEnumerator<TimeSpan> _enumerator;
		readonly Action<Exception, TimeSpan> _onRetry;

		public RetryStateWithSleep(IEnumerable<TimeSpan> sleepDurations, Action<Exception, TimeSpan> onRetry)
		{
			_onRetry = onRetry;
			_enumerator = sleepDurations.GetEnumerator();
		}


		public bool CanRetry(Exception ex)
		{
			if (_enumerator.MoveNext())
			{
				var current = _enumerator.Current;
				_onRetry(ex, current);
				SystemUtil.Sleep(current);
				return true;
			}
			return false;
		}
	}
}