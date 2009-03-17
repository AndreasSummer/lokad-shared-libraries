#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad
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