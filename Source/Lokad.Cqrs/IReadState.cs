#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Handles read operations for the state storage
	/// </summary>
	public interface IReadState
	{
		/// <summary>
		/// Retrieves the specified state from the store, if it is found
		/// </summary>
		/// <param name="type">The type of the state (needed to deserialize).</param>
		/// <param name="identity">The identity.</param>
		/// <returns>state</returns>
		Maybe<object> Load(Type type, string identity);
	}
}