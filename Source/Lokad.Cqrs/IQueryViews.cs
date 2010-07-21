#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Handles CQRS view query operations
	/// </summary>
	public interface IQueryViews
	{
		/// <summary>
		/// <para>Queries the specified view storage, given the type, partition and optional query specification.</para>
		/// <para>Results are passed to the <paramref name="process"/> delegate, as they are retrieved.</para>
		/// </summary>
		/// <param name="type">The type of the view to query.</param>
		/// <param name="partition">The partition to query.</param>
		/// <param name="query">The optional query to pass.</param>
		/// <param name="process">The process delegate, which will be executed against every view entity, as they are loaded.</param>
		void QueryPartition(Type type, string partition, Maybe<IViewQuery> query, Action<ViewEntity> process);
	}
}