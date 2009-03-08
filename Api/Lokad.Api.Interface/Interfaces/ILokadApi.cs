#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Api
{
	/// <summary>
	/// Collection of all methods exposed by the Lokad API
	/// </summary>
	public interface ILokadApi : ITimeSerieApi, IForecastApi, IAccountApi, ILegacyApi, ISystemApi
	{
		// Any name changes of this interface should be reflected in API documentation,
		// since external links point to the description of this class.
	}
}