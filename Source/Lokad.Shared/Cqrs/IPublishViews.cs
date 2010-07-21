using System;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Interface responsible for publishing views
	/// </summary>
	public interface IPublishViews
	{
		/// <summary>
		/// Writes the specified view, scoped by the type, partition and identity.
		/// </summary>
		/// <param name="view">The view.</param>
		/// <param name="partition">The partition.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="item">The item.</param>
		void Write(Type view, string partition, string identity, object item);
		/// <summary>
		/// Patches the specified view (executed the action, if view is present).
		/// </summary>
		/// <param name="type">The type of the view.</param>
		/// <param name="partition">The partition in which view belongs.</param>
		/// <param name="identity">The identity of the view.</param>
		/// <param name="patch">The patch delegate.</param>
		void Patch(Type type, string partition, string identity, Action<object> patch);
		/// <summary>
		/// Deletes the specified type, given its .NET type, partition and identity.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="partition">The partition.</param>
		/// <param name="identity">The identity.</param>
		void Delete(Type type, string partition, string identity);
		/// <summary>
		/// Deletes the entire view partition, given partition identifier and view type
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="partition">The partition.</param>
		void DeletePartition(Type type, string partition);
	}
}