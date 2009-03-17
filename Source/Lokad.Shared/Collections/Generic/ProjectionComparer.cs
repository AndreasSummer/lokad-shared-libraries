#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;

namespace Lokad.Collections.Generic
{
	sealed class ProjectionComparer<T, K> : IEqualityComparer<T>
	{
		readonly Func<T, K> _projection;

		public ProjectionComparer(Func<T, K> projection)
		{
			_projection = projection;
		}

		bool IEqualityComparer<T>.Equals(T x, T y)
		{
			return _projection(x).Equals(_projection(y));
		}

		int IEqualityComparer<T>.GetHashCode(T obj)
		{
			return _projection(obj).GetHashCode();
		}
	}
}