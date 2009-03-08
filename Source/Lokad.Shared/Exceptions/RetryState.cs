using System.Diagnostics.CodeAnalysis;

namespace System.Exceptions
{
	[Immutable]
	sealed class RetryState : IRetryState
	{
		readonly Action<Exception> _onRetry;

		public RetryState(Action<Exception> onRetry)
		{
			_onRetry = onRetry;
		}

		public bool CanRetry(Exception ex)
		{
			_onRetry(ex);
			return true;
		}
	}
}