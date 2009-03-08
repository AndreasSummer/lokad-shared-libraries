using System;
using System.Diagnostics.CodeAnalysis;

namespace Lokad.Api.Legacy
{
	/// <seealso cref="VersionChecker"/>
	[Serializable]
	[NoCodeCoverage]
	[Immutable]
	public class VersionDetectedEventArgs : EventArgs
	{
		readonly bool _isNewVersionDetected;
		readonly Version _version;

		/// <remarks/>
		public VersionDetectedEventArgs(bool isNewVersionDetected, Version version)
		{
			this._isNewVersionDetected = isNewVersionDetected;
			this._version = version;
		}

		/// <summary>Indicates whether a new version has been detected.</summary>
		public bool IsNewVersionDetected
		{
			get { return _isNewVersionDetected; }
		}

		/// <summary>Gets the new version that has been detected.</summary>
		public Version Version
		{
			get { return _version; }
		}
	}
}