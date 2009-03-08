#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Api;

namespace TestConnection
{
	class Program
	{
		const string serviceUrl = "http://sandbox-ws.lokad.com/TimeSeries2.asmx";

		static void Main()
		{
			var login = ""; // put your Lokad login here
			var password = ""; // put your Lokad password here

			try
			{
				var service = ServiceFactory.GetConnectorForTesting(login, password, serviceUrl);
				var info = service.GetAccountInfo();
				Console.WriteLine("My account HR ID is {0}", info.AccountHRID);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			Console.WriteLine("Press any key to exit");
			Console.ReadKey(true);
		}
	}
}