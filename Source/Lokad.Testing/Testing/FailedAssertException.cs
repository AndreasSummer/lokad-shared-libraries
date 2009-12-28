#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Runtime.Serialization;

namespace Lokad.Testing
{
	/// <summary>
	/// Failed assertion exception
	/// </summary>
	[Serializable]
	public sealed class FailedAssertException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FailedAssertException"/> class.
		/// </summary>
		public FailedAssertException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FailedAssertException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public FailedAssertException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FailedAssertException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="inner">The inner.</param>
		public FailedAssertException(string message, Exception inner)
			: base(message, inner)
		{
		}

		FailedAssertException(
			SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
		}
	}
}