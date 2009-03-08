#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Client
{
	/// <summary>
	/// Delegate used to wire editing events from the view 
	/// back through the Controller
	/// </summary>
	/// <typeparam name="TModel">type of the model to edit</typeparam>
	/// <param name="model">model instance being edited</param>
	/// <returns>result that contains either new model or denial
	/// with the explanation</returns>
	public delegate Result<TModel> EditRequest<TModel>(TModel model)
		where TModel : class;
}