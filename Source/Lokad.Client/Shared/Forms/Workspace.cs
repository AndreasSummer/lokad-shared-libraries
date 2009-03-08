#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Lokad.Client.Forms
{
	/// <summary> Simple workspace for <see cref="System.Windows.Forms"/> UI. 
	/// This class is consumed by views and alows to decouple
	/// UI-specific logics from being wired through the controllers.</summary>
	public sealed class Workspace : IDisposable
	{
		readonly Form _owner;

		readonly List<Form> _forms = new List<Form>();

		/// <summary>
		/// Initializes a new instance of the <see cref="Workspace"/> class.
		/// </summary>
		/// <param name="owner">The owner.</param>
		public Workspace(Form owner)
		{
			_owner = owner;
		}

		/// <summary>
		/// Shows the specified form.
		/// </summary>
		/// <param name="form">The form.</param>
		public void Show(Form form)
		{
			form.Owner = _owner;
			form.ShowInTaskbar = false;

			form.Closed += (sender, e) => _forms.Remove(form);
			_forms.Add(form);
			_owner.Invoke(() => form.Show(_owner));
		}

		/// <summary>
		/// Shows the dialog form and blocks current thread till it returs some value.
		/// </summary>
		/// <param name="form">The form to display.</param>
		/// <returns></returns>
		public DialogResult ShowDialog(Form form)
		{
			form.StartPosition = FormStartPosition.CenterParent;
			return _owner.InvokeGet(() => form.ShowDialog(_owner));
		}

		/// <summary>
		/// Shows the dialog and blocks current thread till it returns some value.
		/// </summary>
		/// <param name="form">The form to display.</param>
		/// <returns>result of the dialog</returns>
		public DialogResult ShowDialog(CommonDialog form)
		{
			return _owner.InvokeGet(() => form.ShowDialog(_owner));
		}

		void IDisposable.Dispose()
		{
			_forms.ToArray().ForEach(f => _owner.Invoke(f.Dispose));
		}
	}
}