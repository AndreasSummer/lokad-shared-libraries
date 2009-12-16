#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using Lokad.Quality;

namespace Lokad.Settings
{
	/// <summary>
	/// 	Adds extensions for the simplicity of settings use.
	/// </summary>
	public static class ExtendISettingsProvider
	{
		/// <summary>
		/// 	Converts given dictionary to the settings provider.
		/// </summary>
		/// <param name="dict">The dict.</param>
		/// <returns>
		/// 	new instance of the settings provider
		/// </returns>
		public static ISettingsProvider AsSettingsProvider([NotNull] this IDictionary<string, string> dict)
		{
			if (dict == null) throw new ArgumentNullException("dict");
			return new DictionarySettingsProvider(dict);
		}

		/// <summary>
		/// 	Creates new settings provider, using the
		/// 	<see cref="PrefixTruncatingKeyFilter" />
		/// 	for the path filtering logic
		/// </summary>
		/// <param name="settingsProvider">
		/// 	The settings provider.
		/// </param>
		/// <param name="prefix">
		/// 	The prefix to look for and then truncate.
		/// </param>
		/// <returns>
		/// 	new instance of the settings provider, created by filtering and applying transformations
		/// </returns>
		public static ISettingsProvider FilteredByPrefix([NotNull] this ISettingsProvider settingsProvider, [NotNull] string prefix)
		{
			Enforce.Argument(() => settingsProvider);
			Enforce.ArgumentNotEmpty(() => prefix);

			return settingsProvider.Filtered(new PrefixTruncatingKeyFilter(prefix));
		}
	}
}