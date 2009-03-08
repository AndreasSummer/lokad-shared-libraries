#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using Autofac;

namespace Lokad.Diagnostics
{
	///// <summary>
	///// Register this module in the config 
	///// to provide base monitoring access to the framework
	///// </summary>
	//public sealed class HttpRemotingMonitor : IModule
	//{
	//    /// <summary>
	//    /// Object URI for the remoting interface (defaults to Monitoring.bin)
	//    /// </summary>
	//    public string Endpoint { get; set; }

	//    /// <summary>
	//    /// Port to bind to (defaults to 8081)
	//    /// </summary>
	//    public int Port { get; set; }

	//    /// <summary>
	//    /// <see cref="IModule.Configure"/>
	//    /// </summary>
	//    /// <param name="container"></param>
	//    public void Configure(IContainer container)
	//    {
	//        var channel = new HttpChannel(Port == 0 ? 8081 : Port);
	//        ChannelServices.RegisterChannel(channel, false);

	//        // Record the endpoint type as 'Singleton' well-known type.
	//        var serviceTypeEntry = new WellKnownServiceTypeEntry(
	//            typeof (RemotingSystemMonitor),
	//            Endpoint ?? "Monitoring.bin",
	//            WellKnownObjectMode.Singleton);

	//        // Register the remote object as well-known type.
	//        RemotingConfiguration.RegisterWellKnownServiceType(serviceTypeEntry);
	//    }
	//}
}