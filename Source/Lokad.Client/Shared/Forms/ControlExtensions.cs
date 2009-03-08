#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Rules;
using System.Threading;
using System.Windows.Forms;

namespace Lokad.Client.Forms
{
	/// <summary>
	/// Helper extensions for the <see cref="Control"/>
	/// </summary>
	public static class ControlExtensions
	{
		/// <summary>
		/// Invokes the specified <paramref name="action"/> on the thread that owns the <paramref name="control"/>.
		/// </summary>
		/// <typeparam name="TControl">type of the control to work with</typeparam>
		/// <param name="control">The control to execute action against.</param>
		/// <param name="action">The action to on the thread of the control.</param>
		public static void Invoke<TControl>(this TControl control, Action action) where TControl : Control
		{
			if (!control.InvokeRequired)
			{
				action();
			}
			else
			{
				control.Invoke(action);
			}
		}


		/// <summary>
		/// Just like <see cref="Invoke{T}"/>, but blocks the calling thread 
		/// till the function returns some value.
		/// </summary>
		/// <typeparam name="TControl">control wo work with</typeparam>
		/// <typeparam name="TResult">type of the result to expect</typeparam>
		/// <param name="control">The control.</param>
		/// <param name="function">The function to execute on the thread of the control.</param>
		/// <returns>value from the function</returns>
		public static TResult InvokeGet<TControl, TResult>(this TControl control, Func<TResult> function)
			where TControl : Control
		{
			if (!control.InvokeRequired)
			{
				return (function());
			}
			using (var e = new ManualResetEvent(false))
			{
				TResult result = default(TResult);
				control.Invoke(() =>
					{
						result = function();
						e.Set();
					});
				e.WaitOne();
				return result;
			}
		}


		/// <summary>
		/// <para>Disables the specified control and returns <see cref="IDisposable"/>
		/// that re-enables this control, when it gets out of the scope.</para> 
		/// <code>
		/// using (myButton.Disable())
		/// {
		///   // some code
		/// }
		/// </code>
		/// </summary>
		/// <param name="control">The control to disable.</param>
		/// <returns>new instance of <see cref="IDisposable"/>
		/// that will enable the control</returns>
		public static IDisposable Disable(this Control control)
		{
			Enforce.Argument(
				() => control,
				(control1, scope) => scope.Validate(() => control.Enabled, Is.Equal(true)));

			control.Enabled = false;
			return new DisposableAction(() => control.Enabled = true);
		}
	}
}