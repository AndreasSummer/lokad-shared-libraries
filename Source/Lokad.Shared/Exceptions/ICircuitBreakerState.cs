namespace System.Exceptions
{
	interface ICircuitBreakerState
	{
		Exception LastException { get; }
		bool IsBroken { get; }
		void Reset();
		void TryBreak(Exception ex);
	}
}