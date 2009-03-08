#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Rules;
using Lokad.Api;

namespace Lokad.Client
{
	/// <summary>
	/// Controller that knows how to create and edit events.
	/// </summary>
	public sealed class EditEventController
	{
		readonly IEditView<EventModel> _view;

		/// <summary>
		/// Initializes a new instance of the <see cref="EditEventController"/> class.
		/// </summary>
		/// <param name="view">The view.</param>
		public EditEventController(IEditView<EventModel> view)
		{
			_view = view;
		}

		/// <summary>
		/// Creates the event or returns result with the description of failure.
		/// </summary>
		/// <returns>event or result with the description of failure</returns>
		public Result<EventModel> CreateEvent()
		{
			_view.SetTitle("Create new event");

			var model = new EventModel(DateTime.Now.Date, "New Event", 0, DateTime.MinValue);
			_view.BindModel(model);
			return _view.GetModel(ValidateEvent);
		}

		static void ValidateEvent(EventModel info, IScope scope)
		{
			var apiEvent = new SerieEvent
				{
					Name = info.Name,
					DurationDays = info.Duration.TotalDays,
					KnownSince = info.KnownSince,
					Time = info.Starts
				};

			scope.ValidateInScope(apiEvent, ApiRules.Event);
		}

		/// <summary>
		/// Edits the event or returns result with the description of failure.
		/// </summary>
		/// <returns>event (new instance) or result with the description of failure</returns>
		public Result<EventModel> EditEvent(EventModel model)
		{
			_view.SetTitle("Edit existing event");
			_view.BindModel(model);
			return _view.GetModel(ValidateEvent);
		}
	}
}