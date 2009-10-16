#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if !SILVERLIGHT2

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lokad.Quality;

namespace Lokad
{
	/// <summary>
	/// Helper utility for debugging
	/// </summary>
	[NoCodeCoverage, UsedImplicitly]
	public static class DebugUtil
	{
		static readonly BinaryFormatter Formatter = new BinaryFormatter();

		/// <summary>
		/// Saves the object graph to the specified path.
		/// </summary>
		/// <param name="graph">The graph.</param>
		/// <param name="path">The path to save to.</param>
		public static void SaveTo(object graph, string path)
		{
			if (graph == null) throw new ArgumentNullException("graph");
			if (path == null) throw new ArgumentNullException("path");

			using (var stream = File.Create(path))
			{
				Formatter.Serialize(stream, graph);
			}
		}
		/// <summary>
		/// Writes a formatted message with a line terminator to the <see cref="Debug.Listeners"/> collection.
		/// </summary>
		/// <param name="format">The message format.</param>
		/// <param name="args">The args.</param>
		[StringFormatMethod("format"), UsedImplicitly]
		[Conditional("DEBUG")]
		public static void WriteLine(string format, params object[] args)
		{
			Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		/// <summary>
		/// Writes a formatted message to the <see cref="Debug.Listeners"/> collection.
		/// </summary>
		/// <param name="format">The message format.</param>
		/// <param name="args">The args.</param>
		[StringFormatMethod("format"), UsedImplicitly]
		[Conditional("DEBUG")]
		public static void Write(string format, params object[] args)
		{
			Debug.Write(string.Format(CultureInfo.InvariantCulture, format, args));
		}

		/// <summary>
		/// Writes a message followed by the line terminator to the <see cref="Debug.Listeners"/> collection.
		/// </summary>
		/// <param name="message">The message.</param>
		[Conditional("DEBUG")]
		[UsedImplicitly]
		public static void WriteLine(object message)
		{
			Debug.WriteLine(message.ToString());
		}

		/// <summary>
		/// Writes a message to the <see cref="Debug.Listeners"/> collection.
		/// </summary>
		/// <param name="message">The message.</param>
		[Conditional("DEBUG")]
		[UsedImplicitly]
		public static void Write(object message)
		{
			Debug.Write(message.ToString());
		}

		/// <summary>
		/// Loads the graph from the specified path.
		/// </summary>
		/// <param name="path">The path to load from.</param>
		/// <returns>graph loaded from the specified path</returns>
		public static object LoadFrom(string path)
		{
			if (path == null) throw new ArgumentNullException("path");

			using (var stream = File.OpenRead(path))
			{
				return Formatter.Deserialize(stream);
			}
		}

		/// <summary>
		/// Loads the graph from the specified path.
		/// </summary>
		/// <typeparam name="TGraph">The type of the item.</typeparam>
		/// <param name="path">The path to load from.</param>
		/// <returns>graph loaded from the specified path</returns>
		public static TGraph LoadFrom<TGraph>(string path)
		{
			if (path == null) throw new ArgumentNullException("path");

			return (TGraph) LoadFrom(path);
		}
	}
}

#endif