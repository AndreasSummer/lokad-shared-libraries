#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Collections.Generic;
using NUnit.Framework;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class ModifierScopeTests
	{
		static readonly RuleLevel[] _levels = EnumUtil<RuleLevel>.Values;

		[Test]
		public void Test_Lower_Modifier()
		{
			var levels = new List<RuleLevel>();
			var scope = new SimpleScope("T", (path, level, message) => levels.Add(level)).Lower();

			_levels.ForEach(l => scope.Write(l, "test"));
			Assert.AreEqual(RuleLevel.Warn, scope.Level);
			Assert.AreEqual(levels.Count, 2);

			CollectionAssert.DoesNotContain(levels, RuleLevel.Error);
		}

		[Test]
		public void Test_Raise_Modifier()
		{
			var levels = new List<RuleLevel>();
			var scope = new SimpleScope("T", (path, level, message) => levels.Add(level)).Raise();

			_levels.ForEach(l => scope.Write(l, "test"));
			Assert.AreEqual(RuleLevel.Error, scope.Level);
			Assert.AreEqual(_levels.Length, levels.Count);

			CollectionAssert.DoesNotContain(levels, RuleLevel.None);
		}
	}
}