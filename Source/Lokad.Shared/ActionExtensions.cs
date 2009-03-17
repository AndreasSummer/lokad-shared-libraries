#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad
{
	/// <summary>
	/// Extensions for the <see cref="Action"/>
	/// </summary>
	public static class ActionExtensions
	{
		/// <summary>
		/// Converts the action into <see cref="DisposableAction"/>
		/// </summary>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		public static IDisposable AsDisposable(this Action action)
		{
			return new DisposableAction(action);
		}
	}
}