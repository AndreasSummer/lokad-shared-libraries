#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace Lokad
{
	public static class Expects
	{
		public sealed class ArgumentNullException : ExpectedExceptionAttribute
		{
			public ArgumentNullException()
				: base(typeof (System.ArgumentNullException))
			{
			}
		}

		public sealed class InvalidOperationException : ExpectedExceptionAttribute
		{
			public InvalidOperationException()
				: base(typeof (System.InvalidOperationException))
			{
			}
		}

		public sealed class TimeoutException : ExpectedExceptionAttribute
		{
			public TimeoutException() : base(typeof (System.TimeoutException))
			{
			}
		}

		public sealed class ArgumentException : ExpectedExceptionAttribute
		{
			public ArgumentException() : base(typeof (System.ArgumentException))
			{
			}
		}

		public sealed class RuleException : ExpectedExceptionAttribute
		{
			public RuleException()
				: base(typeof (Rules.RuleException))
			{
			}
		}

		public sealed class ResolutionException : ExpectedExceptionAttribute
		{
			public ResolutionException()
				: base(typeof (Lokad.ResolutionException))
			{
			}
		}

		public sealed class ArgumentOutOfRangeException : ExpectedExceptionAttribute
		{
			public ArgumentOutOfRangeException()
				: base(typeof (System.ArgumentOutOfRangeException))
			{
			}
		}
	}
}