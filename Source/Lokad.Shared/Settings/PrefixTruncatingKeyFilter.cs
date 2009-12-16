#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad.Settings
{
	/// <summary>
	/// 	Simple prefix-based path acceptor, that removes prefix from the path after match
	/// </summary>
	public sealed class PrefixTruncatingKeyFilter : ISettingsKeyFilter
	{
		readonly string _prefix;

		/// <summary>
		/// 	Initializes a new instance of the
		/// 	<see cref="PrefixTruncatingKeyFilter" />
		/// 	class.
		/// </summary>
		/// <param name="prefix">The prefix.</param>
		public PrefixTruncatingKeyFilter(string prefix)
		{
			Enforce.ArgumentNotEmpty(() => prefix);

			_prefix = prefix;
		}

		Maybe<string> ISettingsKeyFilter.Filter(string keyPath)
		{
			if (!keyPath.StartsWith(_prefix, StringComparison.InvariantCulture))
				return Maybe<string>.Empty;

			var filter = keyPath.Remove(0, _prefix.Length);
			return filter;
		}
	}
}