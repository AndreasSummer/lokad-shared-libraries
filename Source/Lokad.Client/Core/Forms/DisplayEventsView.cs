#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Windows.Forms;

namespace Lokad.Client.Forms
{
	/// <summary>
	/// Windows.Forms implementation of <see cref="IDisplayView{TModel}"/> 
	/// for <see cref="EventModel"/>
	/// </summary>
	public partial class DisplayEventsView : Form, IDisplayView<EventModel[]>
	{
		readonly Workspace _workspace;

		/// <summary>
		/// Initializes a new instance of the <see cref="DisplayEventsView"/> class.
		/// </summary>
		/// <param name="workspace">The workspace.</param>
		public DisplayEventsView(Workspace workspace)
		{
			_workspace = workspace;
			InitializeComponent();

			_grid.AutoGenerateColumns = false;
		}

		void IDisplayView<EventModel[]>.Show(EventModel[] models)
		{
			_grid.DataSource = models;
			_workspace.ShowDialog(this);
		}

		void IDisplayView<EventModel[]>.SetTitle(string title)
		{
			Text = title;
		}
	}
}