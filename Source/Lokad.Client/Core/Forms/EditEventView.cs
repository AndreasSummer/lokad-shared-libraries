#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Rules;
using System.Windows.Forms;

namespace Lokad.Client.Forms
{
	/// <summary>
	/// Windows.Forms implementation of <see cref="IEditView{TModel}"/>
	/// for <see cref="EventModel"/>
	/// </summary>
	public partial class EditEventView : Form, IEditView<EventModel>
	{
		readonly Workspace _workspace;
		readonly Validator<EventModel> _validator;
		Rule<EventModel>[] _rules;

		/// <summary>
		/// Initializes a new instance of the <see cref="EditEventView"/> class.
		/// </summary>
		/// <param name="workspace">The workspace.</param>
		public EditEventView(Workspace workspace)
		{
			_workspace = workspace;
			InitializeComponent();

			_validator = new Validator<EventModel>(_name, errorProvider1);

			_validator.Bind(_name, i => i.Name);
			_validator.Bind(_duration, i => i.Duration);
			_validator.Bind(_starts, i => i.Starts);
			_validator.Bind(_knownSinceDate, i => i.KnownSince);
		}

		Result<EventModel> IEditView<EventModel>.GetModel(params Rule<EventModel>[] rules)
		{
			_rules = rules;

			if (DialogResult.OK == _workspace.ShowDialog(this))
			{
				return Result.Success(GetModel());
			}

			return Result<EventModel>.Error("User gave up.");
		}

		void IEditView<EventModel>.BindModel(EventModel model)
		{
			_name.Text = model.Name;
			_starts.Value = model.Starts;
			_duration.Value = new Decimal(model.Duration.TotalDays);
			if (model.KnownSince == DateTime.MinValue)
			{
				_knownSinceCheck.Checked = false;
			}
			else
			{
				_knownSinceCheck.Checked = true;
				_knownSinceDate.Value = model.KnownSince;
			}
		}

		EventModel GetModel()
		{
			return new EventModel(
				_starts.Value,
				_name.Text,
				Decimal.ToDouble(_duration.Value),
				_knownSinceCheck.Checked ? _knownSinceDate.Value : DateTime.MinValue);
		}

		void IEditView<EventModel>.SetTitle(string title)
		{
			Enforce.ArgumentNotEmpty(() => title);
			Text = title;
		}

		private void _knownSinceCheck_CheckedChanged(object sender, EventArgs e)
		{
			_knownSinceDate.Enabled = _knownSinceCheck.Checked;
		}

		private void _ok_Click(object sender, EventArgs e)
		{
			var model = GetModel();
			if (_validator.RunRules(model, _rules).Level == RuleLevel.None)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}