#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Quality;

namespace Lokad.Exceptions
{
	[Immutable]
	sealed class RetryState : IRetryState
	{
		readonly Action<Exception> _onRetry;

		public RetryState(Action<Exception> onRetry)
		{
			_onRetry = onRetry;
		}

		bool IRetryState.CanRetry(Exception ex)
		{
			_onRetry(ex);
			return true;
		}
	}
}