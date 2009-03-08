namespace System
{
	/// <summary>
	/// Interface that abstracts away providers
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TValue"></typeparam>
	/// <remarks>
	/// things like IDataCache (from the Database layers) or IResolver (from the IoC layers) 
	/// are just samples of this interface
	/// </remarks>
	public interface IProvider<TKey, TValue>
	{
		/// <summary>
		/// Retrieves <typeparamref name="TValue"/> given the
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		/// <exception cref="ResolutionException">when the key can not be resolved</exception>
		TValue Get(TKey key);
	}
}