#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Extensions of <see cref="IPublishViews"/>
	/// </summary>
	public static class ExtendIPublishViews
	{
		/// <summary>
		/// Writes the specified view, scoped by the type, partition and identity.
		/// </summary>
		/// <typeparam name="TView">The type of the view.</typeparam>
		/// <param name="self">The interface being extended.</param>
		/// <param name="partition">The partition.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="view">The view.</param>
		public static void Write<TView>(this IPublishViews self, string partition, string identity, TView view)
		{
			self.Write(typeof (TView), partition, identity, view);
		}

		/// <summary>
		/// Deletes the specified type, given its .NET type, partition and identity.
		/// </summary>
		/// <typeparam name="TView">The type of the view.</typeparam>
		/// <param name="self">The interface being extended.</param>
		/// <param name="partition">The partition.</param>
		/// <param name="identity">The identity.</param>
		public static void Delete<TView>(this IPublishViews self, string partition, string identity)
		{
			self.Delete(typeof (TView), partition, identity);
		}

		/// <summary>
		/// Patches the specified view (executed the action, if view is present).
		/// </summary>
		/// <typeparam name="TView">The type of the view.</typeparam>
		/// <param name="self">The interface being extended.</param>
		/// <param name="partition">The partition in which view belongs.</param>
		/// <param name="identity">The identity of the view.</param>
		/// <param name="patch">The patch delegate.</param>
		public static void Patch<TView>(this IPublishViews self, string partition, string identity, Action<TView> patch)
		{
			self.Patch(typeof (TView), partition, identity, o => patch((TView) o));
		}

		/// <summary>
		/// Deletes the entire view partition, given partition identifier and view type
		/// </summary>
		/// <typeparam name="TView">The type of the view.</typeparam>
		/// <param name="self">The interface being extended.</param>
		/// <param name="partition">The partition.</param>
		public static void DeletePartition<TView>(this IPublishViews self, string partition)
		{
			self.DeletePartition(typeof (TView), partition);
		}
	}
}