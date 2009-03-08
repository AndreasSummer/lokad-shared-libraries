#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Api.Legacy;
using NUnit.Framework;

namespace Lokad.Api.Core.Legacy
{
	[TestFixture]
	public class VersionCheckerTests
	{
		static Uri PadUrl =
			new Uri(@"http://www.lokad.com/GetFile.aspx?File=Products%5cDesktop%5cDesktopSalesForecasting.xml");

		[Test]
		public void DownloadMsi()
		{
			string fullPath =
				VersionChecker.DownloadMsi(new Uri("http://www.lokad.com/Home.ashx"), "VersionCheckerTests.html");

			Assert.IsFalse(string.IsNullOrEmpty(fullPath), "#A00");
		}

		[Test]
		public void GetVersionFromPad()
		{
			VersionChecker.PadInfo padInfo = VersionChecker.GetInfoFromPad(PadUrl);
			Assert.IsNotNull(padInfo.Version, "#A00 Version should not be null.");
			Assert.Greater(padInfo.Version, new Version(1, 0), "#A01 Unexpected version number.");
		}
	}
}