using System;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Handles write operations for the state storage
	/// </summary>
	public interface IWriteEntity
	{
		/// <summary>
		/// Patches the specified value, if it is available.
		/// </summary>
		/// <param name="type">The type of the object to patch.</param>
		/// <param name="identity">The identity of the object to patch.</param>
		/// <param name="patch">The patch function.</param>
		/// <exception cref="OptimisticConcurrencyException">when patched object had been changed concurrently</exception>
		/// <exception cref="EntityNotFoundException">when entity being pathed is not found</exception>
		void Patch(Type type, object identity, Action<object> patch);

		/// <summary>
		/// Deletes the specified entity, given it's type and identity
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="identity">The identity.</param>
		void Delete(Type type, object identity);

		/// <summary>
		/// Writes the specified entity to the store, given its type and identity (upsert operation)
		/// </summary>
		/// <param name="type">The type of entity.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="item">The entity to write.</param>
		void Write(Type type, object identity, object item);
	}
}