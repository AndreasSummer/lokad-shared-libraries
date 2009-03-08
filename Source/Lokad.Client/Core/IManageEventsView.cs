#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Client
{
	/// <summary>
	/// Interface for a view that allows to manage 
	/// multiple events, optionally editing or creating them.
	/// </summary>
	public interface IManageEventsView
	{
		/// <summary>
		/// Sets the title of the view and name of the collections.
		/// </summary>
		/// <param name="text">The text for the caption.</param>
		void SetText(string text);

		/// <summary>
		/// Shows the editor and returns.
		/// </summary>
		/// <returns>either new result or failure with the explanation</returns>
		Result<EventModel[]> ShowEditor();

		/// <summary>
		/// Binds the data to this view.
		/// </summary>
		/// <param name="eventPool">The event pool.</param>
		/// <param name="selectedEvents">The selected events.</param>
		void BindData(EventModel[] eventPool, EventModel[] selectedEvents);
	}
}