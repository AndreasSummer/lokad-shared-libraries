using System;

namespace Lokad.Cqrs
{
	public static class ExtendIWriteEntity
	{
		/// <summary>
		/// Writes the specified entity to the store, given its type and identity (upsert operation)
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="store">The store.</param>
		/// <param name="identity">The identity.</param>
		/// <param name="item">The entity to write.</param>
		public static void Write<TEntity>(this IWriteEntity store, object identity, TEntity item)
		{
			store.Write(typeof(TEntity), identity, item);
		}

		/// <summary>
		/// Patches the specified value, if it is available.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="store">The store.</param>
		/// <param name="identity">The identity of the object to patch.</param>
		/// <param name="patch">The patch function.</param>
		/// <exception cref="OptimisticConcurrencyException">when patched object had been changed concurrently</exception>
		/// <exception cref="EntityNotFoundException">when entity being pathed is not found</exception>
		public static void Patch<TEntity>(this IWriteEntity store, object identity, Action<TEntity> patch)
		{
			store.Patch(typeof(TEntity), identity, obj => patch((TEntity)obj));
		}
		/// <summary>
		/// Deletes the specified entity, given it's type and identity
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="store">The store.</param>
		/// <param name="identity">The identity.</param>
		public static void Delete<TEntity>(this IWriteEntity store, string identity)
		{
			store.Delete(typeof(TEntity), identity);
		}
	}
}