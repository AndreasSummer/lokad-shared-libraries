#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Exceptions
{
	static class RetryPolicy
	{
		internal static void Implementation(Action action, ExceptionHandler canRetry, Func<IRetryState> stateBuilder)
		{
			var state = stateBuilder();
			while (true)
			{
				try
				{
					action();
					return;
				}
				catch (Exception ex)
				{
					if (!canRetry(ex))
					{
						throw;
					}

					if (!state.CanRetry(ex))
					{
						throw;
					}
				}
			}
		}
	}
}