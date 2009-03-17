#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;

namespace Lokad
{
	/// <summary>
	/// Helper class for <see cref="Version"/>
	/// </summary>
	public static class VersionUtil
	{
		/// <summary>
		/// Normalizes the specified version by replacing all -1 with 0
		/// </summary>
		/// <param name="version">The version.</param>
		/// <returns>version that has all 0 replaced with -1</returns>
		public static Version Normalize(this Version version)
		{
			var c = version.Build == -1 ? 0 : version.Build;
			var d = version.Revision == -1 ? 0 : version.Revision;
			return new Version(version.Major, version.Minor, c, d);
		}
	}
}