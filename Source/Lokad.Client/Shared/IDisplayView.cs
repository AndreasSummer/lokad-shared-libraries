#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Client
{
	/// <summary>
	/// Generic display view that is capable of displaying objects
	/// </summary>
	/// <typeparam name="TModel">The type of the model.</typeparam>
	public interface IDisplayView<TModel> where TModel : class
	{
		/// <summary>
		/// Binds the model to view and displays it.
		/// </summary>
		/// <param name="models">The model object to display.</param>
		void Show(TModel models);

		/// <summary>
		/// Sets the title of the view.
		/// </summary>
		/// <param name="title">The title text.</param>
		void SetTitle(string title);
	}
}