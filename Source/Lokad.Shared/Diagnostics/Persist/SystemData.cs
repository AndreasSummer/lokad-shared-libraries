#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Xml.Serialization;

namespace Lokad.Diagnostics.Persist
{
	/// <summary>
	/// Diagnostics: Persistence class for system information data
	/// </summary>
	[Serializable]
	public sealed class SystemData
	{
		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		/// <value>The version.</value>
		[XmlIgnore]
		public Version Version { get; set; }


		/// <summary>
		/// Gets or sets the version as string.
		/// </summary>
		/// <value>The version as string.</value>
		[XmlAttribute]
		public string VersionAsString
		{
			get
			{
				return Version == null
					? string.Empty
					: Version.ToString();
			}
			set
			{
				Version = string.IsNullOrEmpty(value)
					? default(Version)
					: new Version(value);
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		[XmlAttribute]
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the configuration.
		/// </summary>
		/// <value>The configuration.</value>
		[XmlAttribute]
		public string Configuration { get; set; }

		/// <summary>
		/// Gets or sets the instance identifier.
		/// </summary>
		/// <value>The instance identifier.</value>
		[XmlAttribute]
		public string Instance { get; set; }

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0} (v.{1}/{2}) @ {3}", Name, Version, Configuration, Instance);
		}

		/// <summary>
		/// Initialization time of the descriptor
		/// </summary>
		public static readonly DateTime Initialized = SystemUtil.Now;
	}
}