#region Copyright (c) 2009-2010 LOKAD SAS. All rights reserved.

// Copyright (c) 2009-2010 LOKAD SAS. All rights reserved.
// You must not remove this notice, or any other, from this software.
// This document is the property of LOKAD SAS and must not be disclosed.

#endregion

using Lokad.Logging;
using Lokad.Quality;

namespace Lokad
{
	/// <summary>
	/// Extends logging syntax
	/// </summary>
	public static class ExtendISupportSyntaxForLogging
	{
		/// <summary>
		/// Registers the log provider from the current log4net stack <see cref="LoggingStack.GetLogProvider"/>.
		/// See <see cref="LoggingStack"/> for options on configuring Apache log4net options.
		/// </summary>
		/// <typeparam name="TModule">The type of the module.</typeparam>
		/// <param name="module">The module.</param>
		/// <returns>same module for the inlining</returns>
		[UsedImplicitly]
		public static TModule LogToStack<TModule>(this TModule module)
			where TModule : ISupportSyntaxForLogging
		{
			module.RegisterLogProvider(LoggingStack.GetLogProvider());
			return module;
		}
	}
}