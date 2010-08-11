namespace Lokad.Cqrs
{
	/// <summary>
	/// Extends <see cref="IReadEntity"/>
	/// </summary>
	public static class ExtendIReadEntity
	{
		/// <summary>
		/// Retrieves the specified entity from the store, if it is found.
		/// Underlying storage could be event, cloud blob or whatever
		/// </summary>
		/// <typeparam name="T">type of the entity</typeparam>
		/// <param name="store">The store.</param>
		/// <param name="identity">The identity.</param>
		/// <returns>loaded entity (if found)</returns>
		public static Maybe<T> Load<T>(this IReadEntity store, object identity)
		{
			return store.Load(typeof(T), identity).Convert(o => (T)o);
		}
	}
}