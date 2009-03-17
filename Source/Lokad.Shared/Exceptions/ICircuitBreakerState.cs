#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Exceptions
{
	interface ICircuitBreakerState
	{
		Exception LastException { get; }
		bool IsBroken { get; }
		void Reset();
		void TryBreak(Exception ex);
	}
}