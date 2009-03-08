namespace System.Exceptions
{
	interface IRetryState
	{
		bool CanRetry(Exception ex);
	}
}