#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Runtime.Serialization;

namespace Lokad.Quality
{
	/// <summary>
	/// Exception thrown by rules checking code quality
	/// </summary>
	[Serializable, NoCodeCoverage]
	public sealed class QualityException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="QualityException"/> class.
		/// </summary>
		public QualityException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="QualityException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public QualityException(string message) : base(message)
		{
		}


		QualityException(
			SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
		}
	}
}