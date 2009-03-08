using System;

namespace Lokad.Client
{
	/// <summary>
	/// Default editor provider
	/// </summary>
	/// <typeparam name="TModel">The type of the model.</typeparam>
	public static class EditorFor<TModel> where TModel : class
	{
		/// <summary>
		/// Editor that always returns false
		/// </summary>
		public static readonly EditRequest<TModel> Null = original =>
			Result<TModel>.Error("No editor assigned for '{0}'", typeof(TModel).Name);
	}
}