#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Runtime.Serialization;

namespace Lokad.Reflection
{
	/// <summary>
	/// Exception thrown, when <see cref="Reflect"/> fails to parse some lambda
	/// </summary>
	[Serializable]
	public class ReflectLambdaException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReflectLambdaException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public ReflectLambdaException(string message) : base(message)
		{
		}


		/// <summary>
		/// Initializes a new instance of the <see cref="ReflectLambdaException"/> class.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// The <paramref name="info"/> parameter is null.
		/// </exception>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">
		/// The class name is null or <see cref="P:System.Exception.HResult"/> is zero (0).
		/// </exception>
		protected ReflectLambdaException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}