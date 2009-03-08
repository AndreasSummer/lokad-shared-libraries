using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Lokad.Api.Legacy
{
	/// <seealso cref="VersionChecker"/>
	[Serializable]
	[NoCodeCoverage]
	public class VersionRetrievedEventArgs : EventArgs
	{
		readonly bool _isNewVersionDetected;
		readonly Version _version;
		readonly string _localMsiFileName;

		/// <remarks/>
		public VersionRetrievedEventArgs(bool isNewVersionDetected, Version version, string localMsiFileName)
		{
			this._isNewVersionDetected = isNewVersionDetected;
			this._version = version;
			this._localMsiFileName = localMsiFileName;
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

		/// <summary>Get the path of the MSI package (locally retrieved
		/// during the async process).</summary>
		public string LocalMsiFileName
		{
			get { return _localMsiFileName; }
		}

		/// <summary>Install the local MSI package (launching an independent process).</summary>
		public void InstallMsiAsync()
		{
			Process.Start("msiexec", string.Format(@"/i ""{0}"" REINSTALL=ALL REINSTALLMODE=vomus", LocalMsiFileName));
		}
	}
}