#region Copyright (c) 2009-2010 LOKAD SAS. All rights reserved.

// Copyright (c) 2009-2010 LOKAD SAS. All rights reserved.
// You must not remove this notice, or any other, from this software.
// This document is the property of LOKAD SAS and must not be disclosed.

#endregion

using Lokad.Serialization;

namespace Lokad
{
	/// <summary>
	/// Syntax extensions for <see cref="ISupportSyntaxForSerialization"/>
	/// </summary>
	public static class ExtendISupportSyntaxForSerialization
	{
		/// <summary>
		/// Uses the binary formatter.
		/// </summary>
		/// <typeparam name="TModule">The type of the module.</typeparam>
		/// <param name="module">The module.</param>
		/// <returns></returns>
		public static TModule UseBinaryFormatter<TModule>(this TModule module)
			where TModule : ISupportSyntaxForSerialization
		{
			module.RegisterSerializer<BinaryMessageSerializer>();
			return module;
		}

		/// <summary>
		/// Uses the data contract serializer.
		/// </summary>
		/// <typeparam name="TModule">The type of the module.</typeparam>
		/// <param name="module">The module.</param>
		/// <returns></returns>
		public static TModule UseDataContractSerializer<TModule>(this TModule module)
			where TModule : ISupportSyntaxForSerialization
		{
			module.RegisterSerializer<DataContractMessageSerializer>();
			return module;
		}
	}
}