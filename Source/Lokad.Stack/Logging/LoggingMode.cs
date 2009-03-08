#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Logging
{
	/// <summary>
	/// Defines log modes for the <see cref="LoggingModule"/>
	/// </summary>
	internal enum LoggingMode
	{
		/// <summary>
		/// Initializes logging system to write to the console.
		/// </summary>
		Console,
		/// <summary>
		/// All logging is configured via lognet config
		/// </summary>
		Config,
		/// <summary>
		/// Logging is configured from file
		/// </summary>
		File
	}
}