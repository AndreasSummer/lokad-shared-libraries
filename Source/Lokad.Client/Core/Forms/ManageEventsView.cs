#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lokad.Client.Forms
{
	/// <summary>
	/// Windows.Forms implementation of <see cref="IManageEventsView"/>
	/// </summary>
	public partial class ManageEventsView : Form, IManageEventsView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ManageEventsView"/> class.
		/// </summary>
		/// <param name="editor">The editor.</param>
		/// <param name="workspace">The workspace.</param>
		/// <param name="specifics">The specifics.</param>
		public ManageEventsView(EditRequest<EventModel> editor, Workspace workspace, Specifics specifics)
		{
			_editor = editor;
			_workspace = workspace;

			InitializeComponent();

			_add.Text = String.Format("Add to {0}", specifics.ForecastItemName);
			_remove.Text = String.Format("Remove from {0}", specifics.ForecastItemName);
			_assignedEventsTitle.Text = String.Format("{0} events", specifics.ForecastItemName);


			foreach (DataGridViewColumn column in _eventPoolView.Columns)
			{
				_skuEventsView.Columns.Add((DataGridViewColumn) column.Clone());
			}
		}


		readonly EditRequest<EventModel> _editor = EditorFor<EventModel>.Null;
		readonly Workspace _workspace;

		Result<EventModel[]> IManageEventsView.ShowEditor()
		{
			if (DialogResult.OK == _workspace.ShowDialog(this))
			{
				var value = ((IEnumerable<EventModel>) _skuEventSource.DataSource).ToArray();
				return Result.Success(value);
			}
			return Result<EventModel[]>.Error("User declined");
		}

		void IManageEventsView.BindData(EventModel[] eventPool, EventModel[] selectedEvents)
		{
			_eventPoolSource.DataSource = eventPool;
			_skuEventSource.DataSource = selectedEvents;
		}

		void _poolView_SelectionChanged(object sender, EventArgs e)
		{
			_add.Enabled = _eventPoolView.SelectedRows.Count > 0;
		}

		void _skuEvents_SelectionChanged(object sender, EventArgs e)
		{
			_remove.Enabled = _skuEventsView.SelectedRows.Count > 0;
			_edit.Enabled = _skuEventsView.SelectedRows.Count == 1;
		}

		static EventModel[] GetSelected(DataGridView view)
		{
			return view.SelectedRows
				.Cast<DataGridViewRow>()
				.Select(r => r.DataBoundItem)
				.Cast<EventModel>()
				.ToArray();
		}

		static void TransferEvents(IEnumerable<EventModel> events, IList source, IList target)
		{
			foreach (var eventInfo in events)
			{
				source.Remove(eventInfo);
				target.Add(eventInfo);
			}
		}

		void _add_Click(object sender, EventArgs e)
		{
			TransferEvents(GetSelected(_eventPoolView), _eventPoolSource, _skuEventSource);
		}

		void _remove_Click(object sender, EventArgs e)
		{
			TransferEvents(GetSelected(_skuEventsView), _skuEventSource, _eventPoolSource);
		}

		void _create_Click(object sender, EventArgs e)
		{
			var result = _editor(null);
			if (result.IsSuccess)
			{
				_skuEventSource.Add(result.Value);
			}
		}

		void _edit_Click(object sender, EventArgs e)
		{
			var selected = GetSelected(_skuEventsView);
			if (selected.IsEmpty())
				return;

			var original = selected[0];
			var result = _editor(original);
			if (result.IsSuccess)
			{
				var i = _skuEventSource.IndexOf(original);
				_skuEventSource.RemoveAt(i);
				_skuEventSource.Insert(i, result.Value);
			}
		}

		void EventManagerView_Shown(object sender, EventArgs e)
		{
			_skuEvents_SelectionChanged(null, EventArgs.Empty);
			_poolView_SelectionChanged(null, EventArgs.Empty);
		}


		void IManageEventsView.SetText(string text)
		{
			Text = text;
		}
	}
}