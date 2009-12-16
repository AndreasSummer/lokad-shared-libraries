#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Settings
{
	/// <summary>
	/// 	Simple repository that merely loads all available
	/// 	and applicable settings for this instance.
	/// 	Tenant filtering and other options, if applicable,
	/// 	would be hidden behind this interface
	/// </summary>
	public interface ISettingsRepository
	{
		/// <summary>
		/// 	Loads the currently active settings from the repo.
		/// </summary>
		/// <returns>
		/// 	dictionary containing settings
		/// </returns>
		ISettingsProvider LoadSettings();
	}
}