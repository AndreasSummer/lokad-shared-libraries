using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Exceptions
{
	[Immutable]
	sealed class RetryStateWithSleep : IRetryState
	{
		readonly IEnumerator<TimeSpan> _enumerator;
		readonly Action<Exception,TimeSpan> _onRetry;

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