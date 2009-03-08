#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Rules;
using Lokad.Client.Core.Resources;
using NUnit.Framework;

namespace Lokad.Client
{
	[TestFixture]
	public sealed class SpecificsTests
	{
		[Test]
		public void CallCenter_Specifics()
		{
			EnumProperties(SafetyStock.ResourceManager);
		}

		[Test]
		public void SafetyStock_Specifics()
		{
			EnumProperties(SafetyStock.ResourceManager);
		}

		[Test]
		public void Default_Specifics()
		{
			EnumProperties(null);
		}

		static void EnumProperties(ResourceManager manager)
		{
			var specifics = new Specifics(manager);
			var values = typeof (Specifics)
				.GetProperties(BindingFlags.Instance)
				.Convert(p => p.GetValue(specifics, null))
				.Cast<string>();

			foreach (var s in values)
			{
				Enforce.That(s, StringIs.NotEmpty);
			}
		}
	}
}