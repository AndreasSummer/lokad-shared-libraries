#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;

namespace Lokad.Collections.Generic
{
	sealed class ProjectionComparer<TValue, TProjection> : IEqualityComparer<TValue>
	{
		readonly Func<TValue, TProjection> _projection;

		public ProjectionComparer(Func<TValue, TProjection> projection)
		{
			_projection = projection;
		}

		bool IEqualityComparer<TValue>.Equals(TValue x, TValue y)
		{
			var projectedX = _projection(x);
			var projectedY = _projection(y);

			return projectedX.Equals(projectedY);
		}

		int IEqualityComparer<TValue>.GetHashCode(TValue obj)
		{
			var projectedObj = _projection(obj);
			return projectedObj.GetHashCode();
		}
	}
}