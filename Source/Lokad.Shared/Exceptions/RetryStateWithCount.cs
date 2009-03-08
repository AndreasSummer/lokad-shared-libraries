namespace System.Exceptions
{
	sealed class RetryStateWithCount : IRetryState
	{
		int _errorCount;
		readonly Action<Exception, int> _onRetry;
		readonly Predicate<int> _canRetry;
		
		public RetryStateWithCount(int retryCount, Action<Exception, int> onRetry)
		{
			_onRetry = onRetry;
			_canRetry = i => _errorCount <= retryCount;
		}
		
		public bool CanRetry(Exception ex)
		{
			_errorCount += 1;
			
			bool result = _canRetry(_errorCount);
			if (result)
			{
				_onRetry(ex, _errorCount-1);
			}
			return result;
		}
	}
}