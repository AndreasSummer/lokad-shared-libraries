#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace System.Exceptions
{
	sealed class CircuitBreakerState : ICircuitBreakerState
	{
		public CircuitBreakerState(TimeSpan duration, int exceptionsToBreak)
		{
			_duration = duration;
			_exceptionsToBreak = exceptionsToBreak;
			Reset();
		}
		
		readonly TimeSpan _duration;
		readonly int _exceptionsToBreak;

		int _count;
		DateTime _blockedTill;
		Exception _lastException;

		public Exception LastException
		{
			get { return _lastException; }
		}

		public bool IsBroken
		{
			get { return SystemUtil.Now < _blockedTill; }
		}

		public void Reset()
		{
			_count = 0;
			_blockedTill = DateTime.MinValue;
			_lastException = new InvalidOperationException("This exception should never be thrown");
		}

		public void TryBreak(Exception ex)
		{
			_lastException = ex;

			_count += 1;
			if (_count >= _exceptionsToBreak)
			{
				BreakTheCircuit();
			}
		}

		void BreakTheCircuit()
		{
			_blockedTill = SystemUtil.Now.Add(_duration);
		}
	}
}