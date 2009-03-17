#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Threading;
using Lokad.Quality;
using Lokad.Threading;

#if !SILVERLIGHT2

namespace Lokad.Exceptions
{
	[Immutable]
	sealed class CircuitBreakerStateLock : ICircuitBreakerState
	{
		readonly ICircuitBreakerState _inner;
		readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

		public CircuitBreakerStateLock(ICircuitBreakerState inner)
		{
			_inner = inner;
		}

		public Exception LastException
		{
			get
			{
				using (_lock.GetReadLock())
				{
					return _inner.LastException;
				}
			}
		}

		public bool IsBroken
		{
			get
			{
				using (_lock.GetReadLock())
				{
					return _inner.IsBroken;
				}
			}
		}

		public void Reset()
		{
			using (_lock.GetWriteLock())
			{
				_inner.Reset();
			}
		}

		public void TryBreak(Exception ex)
		{
			using (_lock.GetWriteLock())
			{
				_inner.TryBreak(ex);
			}
		}
	}
}

#endif