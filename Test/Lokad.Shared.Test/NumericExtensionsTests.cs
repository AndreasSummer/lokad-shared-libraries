#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace Lokad
{
	[TestFixture]
	public sealed class NumericExtensionsTests
	{
		[Test]
		public void UseCases()
		{
			1.Milliseconds();
			1.Seconds();
			1.Minutes();
			1.Hours();
			1.Days();

			1.Kb();
			1.Mb();

			1D.Milliseconds();
			1D.Seconds();
			1D.Minutes();
			1D.Hours();
			1D.Days();

			1D.Round(3);
		}
	}
}