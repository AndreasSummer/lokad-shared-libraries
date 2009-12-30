#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Quality
{
	/// <summary>
	/// 	Predefined class design tags
	/// </summary>
	public enum DesignTag
	{
		/// <summary>Undefined</summary>
		Undefined,
		/// <summary>
		/// 	The object is a data model
		/// </summary>
		Model,
		/// <summary>
		/// 	The object is immutable, using readonly fields
		/// </summary>
		ImmutableWithFields,
		/// <summary>
		/// 	The object is immutable, using properties with private setters
		/// </summary>
		ImmutableWithProperties,
	}
}