#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using NUnit.Framework;

namespace Lokad.Api.Core
{
	[TestFixture]
	public sealed class LokadHelperTests
	{
		[Test]
		public void ReplaceInvalidCharacters()
		{
			var valid = "My Serie Name";
			Assert.AreSame(valid, LokadHelper.ReplaceInvalidCharacters(valid, ' '));
			Assert.AreEqual(valid, LokadHelper.ReplaceInvalidCharacters("My[Serie]Name", ' '));
		}

		[Test, Expects.ArgumentException]
		public void ReplaceInvalidCharacters_Avoids_Loops()
		{
			LokadHelper.ReplaceInvalidCharacters("My Serie", '[');
		}
	}
}