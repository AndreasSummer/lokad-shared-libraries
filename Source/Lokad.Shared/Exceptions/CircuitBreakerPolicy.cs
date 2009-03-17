#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Exceptions
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