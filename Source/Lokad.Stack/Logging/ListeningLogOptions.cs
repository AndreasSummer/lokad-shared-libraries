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
	public class ListeningLogOptions
	{
		/// <summary>
		/// Gets or sets the conversion pattern.
		/// </summary>
		/// <value>The conversion pattern.</value>
		/// <seealso cref="PatternLayout.DefaultConversionPattern"/>
		/// <seealso cref="PatternLayout.DetailConversionPattern"/>
		public string Pattern { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to auto flush the log.
		/// </summary>
		/// <value><c>true</c> if auto flush; otherwise, <c>false</c>.</value>
		public bool ImmediateFlush { get; set; }
	}
}