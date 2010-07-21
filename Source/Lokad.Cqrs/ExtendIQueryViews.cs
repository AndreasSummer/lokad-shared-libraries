#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Extends <see cref="IQueryViews"/>
	/// </summary>
	public static class ExtendIQueryViews
	{
		/// <summary>
		/// 	<para>Queries the specified view storage, given the type and partition.</para>
		/// 	<para>Results are passed to the <paramref name="process"/> delegate, as they are retrieved.</para>
		/// </summary>
		/// <typeparam name="TView">The type of the view.</typeparam>
		/// <param name="self">The insterface being extended.</param>
		/// <param name="partition">The partition to query.</param>
		/// <param name="process">The process delegate, which will be executed against every view entity, as they are loaded.</param>
		public static void QueryPartition<TView>(this IQueryViews self, string partition, Action<ViewEntity<TView>> process)
		{
			self.QueryPartition(typeof (TView), partition, Maybe<IViewQuery>.Empty, ve => process(ve.Cast<TView>()));
		}

		/// <summary>
		/// 	<para>Queries the specified view storage, given the type, partition and optional query specification.</para>
		/// 	<para>Results are passed to the <paramref name="process"/> delegate, as they are retrieved.</para>
		/// </summary>
		/// <typeparam name="TView">The type of the view.</typeparam>
		/// <param name="self">The interface being extended.</param>
		/// <param name="partition">The partition to query.</param>
		/// <param name="query">The optional query to pass.</param>
		/// <param name="process">The process delegate, which will be executed against every view entity, as they are loaded.</param>
		public static void QueryPartition<TView>(this IQueryViews self, string partition, IViewQuery query,
			Action<ViewEntity<TView>> process)
		{
			self.QueryPartition(typeof (TView), partition, Maybe.From(query), ve => process(ve.Cast<TView>()));
		}

		/// <summary>
		/// Loads the single view instance, given its partition and identity
		/// </summary>
		/// <typeparam name="TView">The type of the view.</typeparam>
		/// <param name="self">The interface to extend.</param>
		/// <param name="partition">The partition to look in.</param>
		/// <param name="identity">The identity of the view to load.</param>
		/// <returns>view instance, if found</returns>
		public static Maybe<TView> Load<TView>(this IQueryViews self, string partition, string identity)
		{
			var result = Maybe<TView>.Empty;
			var q = new ViewQuery(1, new IdentityConstraint(ConstraintOperand.Equal, identity));
			self.QueryPartition(typeof (TView), partition, q, v => result = (TView) v.Value);
			return result;
		}

		/// <summary>
		/// 	<para>Queries the specified view storage, given the type and partition.</para>
		/// 	<para>Results are aggregated and returned in a collection.</para>
		/// </summary>
		/// <typeparam name="TView">The type of the view.</typeparam>
		/// <param name="self">The insterface being extended.</param>
		/// <param name="partition">The partition to query.</param>
		/// <returns>collection containing the results</returns>
		public static IList<ViewEntity<TView>> ListPartition<TView>(this IQueryViews self, string partition)
		{
			return self.ListPartition<TView>(partition, ViewQuery.Empty);
		}

		/// <summary>
		/// 	<para>Queries the specified view storage, given the type and partition.</para>
		/// 	<para>Results are aggregated and returned in a collection.</para>
		/// </summary>
		/// <typeparam name="TView">The type of the view.</typeparam>
		/// <param name="self">The insterface being extended.</param>
		/// <param name="partition">The partition to query.</param>
		/// <param name="query">The query.</param>
		/// <returns>collection containing the results</returns>
		public static IList<ViewEntity<TView>> ListPartition<TView>(this IQueryViews self, string partition, IViewQuery query)
		{
			var list = new List<ViewEntity<TView>>();
			self.QueryPartition<TView>(partition, query, list.Add);
			return list.AsReadOnly();
		}
	}
}