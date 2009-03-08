using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Xml;
using Microsoft.Win32;

namespace Lokad.Api.Legacy
{
	/// <summary>Utility classes to retrieve meta data stored in an online PAD file,
	/// and optionally perform the software update if needed.</summary>
	/// <remarks>See <a href="http://www.asp-shareware.org/pad/">http://www.asp-shareware.org/pad/</a>
	/// for more details about the Portable Application Description (PAD) format.</remarks>
	[Serializable]
	public class VersionChecker
	{
		class CheckerState
		{
			public readonly Uri PadUri;
			public readonly Version CurrentVersion;

			public CheckerState(Uri padUri, Version currentVersion)
			{
				PadUri = padUri;
				CurrentVersion = currentVersion;
			}
		}

		internal class PadInfo
		{
			public Version Version;
			public Uri DownloadUri;
			public string VersionedFileName;

			public PadInfo() { }

			public PadInfo(Version version, Uri downloadUri, string versionedFileName)
			{
				Version = version;
				DownloadUri = downloadUri;
				VersionedFileName = versionedFileName;
			}
		}

		readonly Guid productId;

		/// <summary>Constructor</summary>
		/// <param name="productId">GUID associated to the <c>ProductCode</c> in the MSI package.</param>
		public VersionChecker(Guid productId)
		{
			this.productId = productId;
		}

		/// <summary>Fired when the PAD info is retrieved.</summary>
		public event EventHandler<VersionDetectedEventArgs> VersionDetected;

		/// <summary>Fired when the new version has been retrieved.</summary>
		public event EventHandler<VersionRetrievedEventArgs> VersionRetrieved;

		/// <summary>Asynchronous retrieval of the specified PAD file.</summary>
		/// <remarks>
		/// <para>The event <see cref="VersionRetrieved"/> is fired once the version
		/// has been retrieved. The retrieval is queued in the threadpool.</para>
		/// <para>If a new version is detected, then the corresponding MSI package
		/// is downloaded to the temporary internet files folder.</para></remarks>
		public void AsyncGetVersionFromPad(Uri padUri, Version currentVersion)
		{
			ThreadPool.QueueUserWorkItem(InternalAsyncGetVersionFromPad,
				new CheckerState(padUri, currentVersion));
		}

		void InternalAsyncGetVersionFromPad(object state)
		{
			Uri padUri = ((CheckerState)state).PadUri;
			Version currentVersion = ((CheckerState)state).CurrentVersion;
			try
			{
				PadInfo padInfo = GetInfoFromPad(padUri);
				bool isNewVersionDetected = currentVersion.CompareTo(padInfo.Version) < 0;

				if (null != VersionDetected)
				{
					VersionDetected(this, new VersionDetectedEventArgs(isNewVersionDetected, padInfo.Version));
				}

				string localMsiFileName = null;
				if (isNewVersionDetected &&
					null != padInfo.DownloadUri && !string.IsNullOrEmpty(padInfo.VersionedFileName))
				{
					localMsiFileName =
						DownloadMsi(padInfo.DownloadUri,
							GetPackageNameFromRegistry(productId, padInfo.VersionedFileName));
				}

				if (null != VersionRetrieved)
				{
					VersionRetrieved(this,
						new VersionRetrievedEventArgs(isNewVersionDetected, padInfo.Version, localMsiFileName));
				}
			}
			catch (WebException)
			{
				// silent failure in case of network failure.
			}
			catch (InvalidOperationException)
			{
				// silent failure in case of PAD parsing failure.
			}
		}

		/// <summary>Retrieve the version number contained in an online PAD file.</summary>
		internal static PadInfo GetInfoFromPad(Uri padUri)
		{
			// HACK: if network connection is down, this method must fail in a clean manner.
			// For ex, an 'null' value could be returned.

			// retrieving the latest version number from the published PAD file
			using (var reader = new XmlTextReader(padUri.ToString()))
			{
				try
				{
					var document = new XmlDocument();
					document.Load(reader);

					var padInfo = new PadInfo();

					XmlNode node = document.SelectSingleNode(@"/XML_DIZ_INFO/Program_Info/Program_Version");

					if (null != node)
					{
						var version = new Version(node.InnerText);
						padInfo.Version = VersionUtil.Normalize(version);
					}

					node = document.SelectSingleNode(@"/XML_DIZ_INFO/Program_Info/File_Info/Filename_Versioned");
					if (null != node)
					{
						padInfo.VersionedFileName = node.InnerText;
					}

					node = document.SelectSingleNode(@"/XML_DIZ_INFO/Web_Info/Download_URLs/Primary_Download_URL");
					if (null != node)
					{
						try
						{
							padInfo.DownloadUri = new Uri(node.InnerText);
						}
						catch (UriFormatException)
						{
							padInfo.DownloadUri = null;
						}
					}

					return padInfo;
				}
				catch (XmlException)
				{
					throw new InvalidOperationException("PAD file does not appear to be correct XML.");
				}
			}
		}

		/// <summary>Download the MSI package.</summary>
		/// <param name="msiLocation">URL of the MSI package.</param>
		/// <param name="versionedFileName">Name of the local copy of the MSI package.</param>
		/// <returns>The full path of the local copy of the MSI package.</returns>
		internal static string DownloadMsi(Uri msiLocation, string versionedFileName)
		{
			string fullName =
				Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\" + versionedFileName;

			// Delete previous copies of the MSI package if any.
			if (File.Exists(fullName))
			{
				File.Delete(fullName);
			}

			try
			{
				var wc = new WebClient();
				wc.DownloadFile(msiLocation, fullName + ".tmp");

				// Heuristic: download is (probably) not atomic - at least nothing is
				// specified in the MSDN docs - yet, the application can be closed during
				// download. This would typically lead to a corrupted .msi file. Thus,
				// we are moving the file at the end of the download.
				File.Move(fullName + ".tmp", fullName);
			}
			catch (WebException)
			{
				return null;
			}

			return fullName;
		}


		/// <summary>Returns the package name as installed for the specified Product ID.</summary>
		/// <remarks>
		/// The new MSI package name must match the old one, otherwise the MSI install fails.
		/// Absurd but true, see
		/// http://groups.google.fr/group/Wixg/browse_thread/thread/d5cb261379f29479
		/// </remarks>
		static string GetPackageNameFromRegistry(Guid productId, string defaultName)
		{
			string regPath = "HKEY_CURRENT_USER\\Software\\Microsoft\\Installer\\Products\\" +
				GetProductCodeFromProductId(productId) + "\\SourceList";

			return (string)Registry.GetValue(regPath, "PackageName", null) ?? defaultName;
		}

		/// <summary>For weird reasons, installed packages are not listed against
		/// their <c>ProductCode</c>, but against a permutation of their <c>ProductCode</c>.
		/// This method returns the token needed for retrieving the package name
		/// from the windows registry.</summary>
		/// <remarks>
		/// Code token found at
		/// http://www.hanselman.com/blog/CommentView.aspx?guid=4e93e0a7-7af9-4397-95dd-db013901e6ee
		/// </remarks>
		static string GetProductCodeFromProductId(Guid productId)
		{
			string raw = productId.ToString("N");
			char[] aRaw = raw.ToCharArray();

			//compressed format reverses 11 byte sequences of the original guid
			var revs = new[] { 8, 4, 4, 2, 2, 2, 2, 2, 2, 2, 2 };
			int pos = 0;
			for (int i = 0; i < revs.Length; i++)
			{
				Array.Reverse(aRaw, pos, revs[i]);
				pos += revs[i];
			}
			var n = new string(aRaw);
			var newGuid = new Guid(n);

			//GUID in registry are all caps.
			return newGuid.ToString("N").ToUpper();
		}
	}
}
