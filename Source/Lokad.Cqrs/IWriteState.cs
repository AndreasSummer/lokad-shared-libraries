using System;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Handles write operations for the state storage
	/// </summary>
	public interface IWriteState
	{

		/// <summary>
		/// Patches the specified value, if it is available.
		/// </summary>
		/// <param name="type">The type of the object to patch.</param>
		/// <param name="identity">The identity of the object to patch.</param>
		/// <param name="patch">The patch function.</param>
		void Patch(Type type, string identity, Action<object> patch);

		/// <summary>
		/// Deletes the specified item, given it's type and identity
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="identity">The identity.</param>
		void Delete(Type type, string identity);

		/// <summary>
		/// Writes the specified item to the store, given its type and identity (upsert operation)
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="item">The item.</param>
		void Write(Type type, string identity, object item);
	}
}