#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lokad.Quality;

namespace Lokad.Settings
{
	/// <summary>
	/// Settings provider based on a simple dictionary
	/// </summary>
	[Immutable]
	public sealed class DictionarySettingsProvider : ISettingsProvider
	{
		readonly IDictionary<string, string> _dictionary;

		/// <summary>
		/// Initializes a new instance of the <see cref="DictionarySettingsProvider"/> class.
		/// </summary>
		/// <param name="dictionary">The dictionary.</param>
		public DictionarySettingsProvider([NotNull] IDictionary<string, string> dictionary)
		{
			if (dictionary == null) throw new ArgumentNullException("dictionary");
			_dictionary = dictionary;
		}

		Maybe<string> ISettingsProvider.GetValue([NotNull] string name)
		{
			if (name == null) throw new ArgumentNullException("name");

			return _dictionary.GetValue(name);
		}

		ISettingsProvider ISettingsProvider.Filtered([NotNull] ISettingsKeyFilter acceptor)
		{
			if (acceptor == null) throw new ArgumentNullException("acceptor");

			var dict = new Dictionary<string, string>();

			foreach (var pair in _dictionary)
			{
				var v = pair.Value;
				acceptor
					.Filter(pair.Key)
					.Apply(f => dict.Add(f, v));
			}
			return dict.AsSettingsProvider();

		}
	}
}