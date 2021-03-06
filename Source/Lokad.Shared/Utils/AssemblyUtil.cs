#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Reflection;

namespace Lokad
{
	/// <summary>
	/// Helper class for the managing .NET assemblies
	/// </summary>
	public static class AssemblyUtil
	{
		/// <summary>
		/// Retrieves value of the <see cref="AssemblyConfigurationAttribute"/> for the current assembly
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException">When the attribute is missing</exception>
		public static Result<string> GetAssemblyConfiguration()
		{
			var callingAssembly = Assembly.GetCallingAssembly();

			return GetAssemblyConfiguration(callingAssembly);
		}

// ReSharper disable SuggestBaseTypeForParameter
		internal static Result<string> GetAssemblyConfiguration(Assembly callingAssembly)
// ReSharper restore SuggestBaseTypeForParameter
		{
			var attributes = callingAssembly
				.GetAttributes<AssemblyConfigurationAttribute>(true);

			if (attributes.Length == 0)
				return Result<string>.CreateError("Attribute is not present");
			return Result.CreateSuccess(attributes[0].Configuration);
		}

		/// <summary>
		/// If <see cref="AssemblyDescriptionAttribute"/> is present in the calling assembly, 
		/// then its value is retrieved. <see cref="string.Empty"/> is returned otherwise.
		/// </summary>
		/// <returns></returns>
		public static Result<string> GetAssemblyDescription()
		{
			var attributes = Assembly
				.GetCallingAssembly()
				.GetAttributes<AssemblyDescriptionAttribute>(true);

			if (attributes.Length == 0)
				return Result<string>.CreateError("Attribute was not found");

			return Result.CreateSuccess(attributes[0].Description);
		}
	}
}