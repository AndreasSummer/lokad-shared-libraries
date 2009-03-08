
namespace System.Exceptions
{

	static class CircuitBreakerPolicy
	{
		internal static void Implementation(Action action, ExceptionHandler canHandle, ICircuitBreakerState breaker)
		{
			if (breaker.IsBroken)
				throw breaker.LastException;

			try
			{
				action();
				breaker.Reset();
				return;
			}
			catch (Exception ex)
			{
				if (!canHandle(ex))
				{
					throw;
				}
				breaker.TryBreak(ex);
				throw;
			}
		}
	}
}