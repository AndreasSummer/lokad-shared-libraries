#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lokad.Testing
{
	/// <summary>
	/// Simple dictionary-based cache for the equality providers
	/// </summary>
	sealed class TestModelEqualityCache : ITestModelEqualityProvider
	{
		readonly IDictionary<Type, TestModelEqualityDelegate> _dictionary = new Dictionary<Type, TestModelEqualityDelegate>();

		public Func<Type, TestModelEqualityDelegate> UnknownType { get; set; }

		public TestModelEqualityCache()
		{
			UnknownType = type => { throw new KeyInvalidException(string.Format(CultureInfo.InvariantCulture, "Key has invalid value '{0}'", (object) type)); };
		}

		public TestModelEqualityDelegate GetEqualityTester(Type type)
		{
			TestModelEqualityDelegate value;
			if (!_dictionary.TryGetValue(type, out value))
			{
				value = UnknownType(type);
				_dictionary.Add(type, value);
			}
			return value;
		}
	}
}