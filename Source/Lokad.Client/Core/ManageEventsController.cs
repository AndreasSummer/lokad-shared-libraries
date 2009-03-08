#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lokad.Client
{
	/// <summary>
	/// Controller that knows how to manage event collections
	/// </summary>
	public sealed class ManageEventsController
	{
		readonly IManageEventsView _managerView;
		readonly IDisplayView<EventModel[]> _eventView;

		/// <summary>
		/// Initializes a new instance of the <see cref="ManageEventsController"/> class.
		/// </summary>
		/// <param name="managerView">The manager view.</param>
		/// <param name="eventListView">The event list view.</param>
		public ManageEventsController(IManageEventsView managerView, IDisplayView<EventModel[]> eventListView)
		{
			_managerView = managerView;
			_eventView = eventListView;
		}


		/// <summary>
		/// Manages the events.
		/// </summary>
		/// <param name="models">The data.</param>
		/// <param name="model">The item.</param>
		/// <returns>true if the events were changed</returns>
		public bool ManageEvents(IForecastModels models, IForecastModel model)
		{
			// abdullin: switching the statement allows to edit events
			// even when adapter handles them. Good for debugging,
			// but do not forget to restore it!
			if (models.CanEditEvents)
			{
				var result = EditEvents(models.Items, model);
				if (result.IsSuccess)
				{
					model.Events = result.Value;
				}
				return result.IsSuccess;
			}
			ViewItemEvents(model);
			return false;
		}

		Result<EventModel[]> EditEvents(IEnumerable<IForecastModel> data, IForecastModel model)
		{
			_managerView.SetText(GetTitle(model));

			var eventHash = model.Events.ToSet(s => s.ToTuple());
			var eventPool = GetDistinctEvents(data)
				.Where(s => !eventHash.Contains(s.ToTuple()))
				.ToArray();


			_managerView.BindData(eventPool, model.Events);

			return _managerView.ShowEditor();
		}

		static string GetTitle(IForecastModel model)
		{
			return string.IsNullOrEmpty(model.Name)
				? string.Format("Events for {0}", model.Identifier)
				: string.Format("Events for '{0}' ({1})", model.Name, model.Identifier);
		}

		static IEnumerable<EventModel> GetDistinctEvents(IEnumerable<IForecastModel> collection)
		{
			return collection
				.Where(e => null != e.Events)
				.SelectMany(e => e.Events)
				.Distinct(e => e.ToTuple());
		}

		void ViewItemEvents(IForecastModel model)
		{
			Enforce.That(null != model.Events);

			_eventView.SetTitle(GetTitle(model));
			_eventView.Show(model.Events);
		}

		/// <summary>
		/// Views the event pool (unique items are shown).
		/// </summary>
		/// <param name="items">The items.</param>
		public void ViewEventPool(IEnumerable<IForecastModel> items)
		{
			_eventView.SetTitle("All events");
			var eventPool = GetDistinctEvents(items);
			_eventView.Show(eventPool.ToArray());
		}
	}
}