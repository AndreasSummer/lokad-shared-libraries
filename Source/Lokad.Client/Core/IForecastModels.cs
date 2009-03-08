#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;

namespace Lokad.Client
{
	/// <summary>
	/// Represents a collection of <see cref="IForecastModel"/> that could
	/// be manipulated
	/// </summary>
	public interface IForecastModels
	{
		/// <summary>
		/// Gets a value indicating whether this instance allows to edit events.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance allows edit events; otherwise, <c>false</c>.
		/// </value>
		bool CanEditEvents { get; }

		/// <summary>
		/// Gets the items associated with this collection.
		/// </summary>
		/// <value>The items.</value>
		IEnumerable<IForecastModel> Items { get;}
	}
}