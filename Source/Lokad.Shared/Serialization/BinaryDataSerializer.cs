#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Lokad.Quality;

namespace Lokad.Serialization
{
	/// <summary>
	/// Binary data serializer using <see cref="BinaryFormatter"/>
	/// </summary>
	[UsedImplicitly]
	public sealed class BinaryDataSerializer : IDataSerializer
	{
		readonly BinaryFormatter _formatter = new BinaryFormatter();

		/// <summary>
		/// Singleton instance of the serializer
		/// </summary>
		public static readonly IDataSerializer Instance = new BinaryDataSerializer();

		/// <summary>
		/// Serializes the object to the specified stream
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <param name="destinationStream">The destination stream.</param>
		public void Serialize(object instance, Stream destinationStream)
		{
			_formatter.Serialize(destinationStream, instance);
		}

		/// <summary>
		/// Deserializes the object from specified source stream.
		/// </summary>
		/// <param name="sourceStream">The source stream.</param>
		/// <param name="type">The type of the object to deserialize.</param>
		/// <returns>deserialized object</returns>
		public object Deserialize(Stream sourceStream, Type type)
		{
			return _formatter.Deserialize(sourceStream);
		}
	}
}