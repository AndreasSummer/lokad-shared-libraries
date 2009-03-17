#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

#if !SILVERLIGHT2

using System;
using System.IO;
using System.IO.Compression;
using Lokad.Quality;

namespace Lokad
{
	/// <summary>
	/// Simple helper extensions for the <see cref="Stream"/>
	/// </summary>
	public static class StreamExtensions
	{
		/// <summary>
		/// Wraps the stream with Compression stream
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		[NoCodeCoverage]
		public static GZipStream Compress(this Stream stream)
		{
			return new GZipStream(stream, CompressionMode.Compress);
		}

		/// <summary>
		/// Wraps the stream with Decompressing stream
		/// </summary>
		[NoCodeCoverage]
		public static GZipStream Decompress(this Stream stream)
		{
			return new GZipStream(stream, CompressionMode.Decompress);
		}

		/// <summary>
		/// Copies contents of this stream to the target stream
		/// </summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="bufferSize"></param>
		/// <returns></returns>
		public static long PumpTo(this Stream source, Stream target, int bufferSize)
		{
			if (source == null) throw new ArgumentNullException("source");
			if (target == null) throw new ArgumentNullException("target");

			Enforce.With<ArgumentOutOfRangeException>(bufferSize > 0, "bufferSize must be positive");

			var buffer = new byte[bufferSize];
			long total = 0;
			int count;
			while (0 < (count = source.Read(buffer, 0, bufferSize)))
			{
				target.Write(buffer, 0, count);
				total += count;
			}
			return total;
		}
	}
}

#endif