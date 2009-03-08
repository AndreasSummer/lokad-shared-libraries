#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Rules;

namespace Lokad.Client
{
	/// <summary>
	/// Generic editor view that is capable of running validation locally
	/// </summary>
	/// <typeparam name="TModel">type of the item to edit</typeparam>
	public interface IEditView<TModel> where TModel : class
	{
		/// <summary>
		/// Asks user for the data
		/// </summary>
		/// <param name="rules">The rules to check against.</param>
		/// <returns>result that either contains valid response or the reason for failure</returns>
		Result<TModel> GetModel(params Rule<TModel>[] rules);

		/// <summary>
		/// Binds the model to view.
		/// </summary>
		/// <param name="model">The model object to display.</param>
		void BindModel(TModel model);

		/// <summary>
		/// Sets the title of the view.
		/// </summary>
		/// <param name="title">The title text.</param>
		void SetTitle(string title);
	}
}