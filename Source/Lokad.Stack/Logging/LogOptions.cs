#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using log4net.Layout;

namespace Lokad.Logging
{
	/// <summary>
	/// Log options
	/// </summary>
	public class LogOptions
	{
		/// <summary>
		/// Gets or sets the conversion pattern.
		/// </summary>
		/// <value>The conversion pattern.</value>
		/// <seealso cref="PatternLayout.DefaultConversionPattern"/>
		/// <seealso cref="PatternLayout.DetailConversionPattern"/>
		public string Pattern { get; set; }
	}
}