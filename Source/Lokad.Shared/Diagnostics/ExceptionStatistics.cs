#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Diagnostics.CodeAnalysis;

namespace Lokad.Diagnostics
{
	/// <summary>
	/// Final statistics about the exceptions in some system
	/// </summary>
	[Immutable]
	[Serializable]
	public sealed class ExceptionStatistics
	{
		readonly Guid _id;
		readonly long _count;
		readonly string _name;
		readonly string _message;
		readonly string _text;

		/// <summary>
		/// Gets the unique identifier of the exception
		/// </summary>
		/// <value>The ID.</value>
		public Guid ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Gets the number of times this exception did occur.
		/// </summary>
		/// <value>The number of times this exception did occur.</value>
		public long Count
		{
			get { return _count; }
		}

		/// <summary>
		/// Gets the name of the exception.
		/// </summary>
		/// <value>The name.</value>
		public string Name
		{
			get { return _name; }
		}

		/// <summary>
		/// Gets the message associated with the exception.
		/// </summary>
		/// <value>The message.</value>
		public string Message
		{
			get { return _message; }
		}

		/// <summary>
		/// Gets the textual representation of the exception.
		/// </summary>
		/// <value>The text.</value>
		public string Text
		{
			get { return _text; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionStatistics"/> class.
		/// </summary>
		/// <param name="id">The exception ID.</param>
		/// <param name="count">The exception count.</param>
		/// <param name="exception">The exception to associate with.</param>
		public ExceptionStatistics(Guid id, long count, Exception exception)
		{
			_id = id;
			_count = count;
			_name = exception.GetType().Name;
			_message = exception.GetType().Name;
			_text = exception.ToString();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionStatistics"/> class.
		/// </summary>
		/// <param name="id">Exception ID.</param>
		/// <param name="count">Exception count.</param>
		/// <param name="name">Exception name.</param>
		/// <param name="message">Exception message.</param>
		/// <param name="text">Exception text.</param>
		public ExceptionStatistics(Guid id, long count, string name, string message, string text)
		{
			_id = id;
			_count = count;
			_name = name;
			_message = message;
			_text = text;
		}
	}
}