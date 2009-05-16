#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Threading;

namespace Lokad.Diagnostics
{
	/// <summary>
	/// Class holding information about exception.
	/// </summary>
	sealed class ExceptionCounter
	{
		readonly Guid _id;
		readonly Exception _firstInstance;
		long _count;

		/// <summary>
		/// Gets the unique identifier for the exception.
		/// </summary>
		/// <value>The unique identifier for the exception.</value>
		public Guid ID
		{
			get { return _id; }
		}

		/// <summary>
		/// Gets the first instance of the exception.
		/// </summary>
		/// <value>The first instance.</value>
		public Exception FirstInstance
		{
			get { return _firstInstance; }
		}

		/// <summary>
		/// Gets the exception count.
		/// </summary>
		/// <value>The count.</value>
		public long Count
		{
			get { return _count; }
		}

		/// <summary>
		/// Performs atomic increment of the exception count
		/// </summary>
		internal void InterlockedIncrement()
		{
			Interlocked.Increment(ref _count);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionCounter"/> class.
		/// </summary>
		/// <param name="firstInstance">The first instance.</param>
		public ExceptionCounter(Exception firstInstance)
		{
			_id = Guid.NewGuid();
			_firstInstance = firstInstance;
			_count = 1;
		}
	}
}