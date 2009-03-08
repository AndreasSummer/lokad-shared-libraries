#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace System
{
	/// <summary>
	/// Generic interface for the command pattern.
	/// </summary>
	public interface ICommand
	{
		/// <summary>
		/// Encapsulates action
		/// </summary>
		void Execute();
	}
}