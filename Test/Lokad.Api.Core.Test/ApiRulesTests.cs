#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Rules;
using NUnit.Framework;

namespace Lokad.Api.Core
{
	[TestFixture]
	public sealed class ApiRulesTests
	{
		[Test]
		public void Test()
		{
			ShouldPass("rinat.abdullin@lokad.com", "pwd", "http://ws.lokad.com/TimeSerieS2.asmx");
			ShouldPass("some@nowhere.net", "pwd", "http://127.0.0.1/TimeSerieS2.asmx");
			ShouldPass("rinat.abdullin@lokad.com", "pwd", "http://sandbox-ws.lokad.com/TimeSerieS2.asmx");

			ShouldFail("invalid", "pwd", "http://ws.lokad.com/TimeSerieS.asmx");
			ShouldFail("rinat.abdullin@lokad.com", "pwd", "http://identity-theift.com/TimeSerieS2.asmx");
		}

		static void ShouldFail(string username, string pwd, string url)
		{
			try
			{
				ShouldPass(username, pwd, url);
				Assert.Fail("Expected {0}", typeof (RuleException).Name);
			}
			catch (RuleException)
			{
			}
		}

		static void ShouldPass(string username, string pwd, string url)
		{
			var connection = new ServiceConnection(username, pwd, new Uri(url));
			Enforce.That(connection, ApiRules.ValidConnection);
		}
	}
}