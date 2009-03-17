#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Diagnostics
{
	///// <summary>
	///// This class exposes framework information over the remoting interface
	///// </summary>
	//[NoCodeCoverage]
	//public sealed class RemotingSystemMonitor : MarshalByRefObject, ISystemMonitor
	//{
	//    /// <summary>
	//    /// <see cref="ISystemMonitor.GetStatistics"/>
	//    /// </summary>
	//    /// <returns></returns>
	//    public ExecutionStatistics[] GetStatistics()
	//    {
	//        return Framework.ExecutionCounters.GetStatistics().ToArray();
	//    }

	//    /// <summary>
	//    /// <see cref="ISystemMonitor.ResetCounters"/>
	//    /// </summary>
	//    public void ResetCounters()
	//    {
	//        Framework.ExceptionCounter.Clear();
	//        Framework.ExecutionCounters.Clear();
	//    }

	//    /// <summary>
	//    /// <see cref="ISystemMonitor.GetDescriptor"/>
	//    /// </summary>
	//    /// <returns></returns>
	//    public SystemDescriptor GetDescriptor()
	//    {
	//        return Framework.Descriptor;
	//    }

	//    /// <summary>
	//    /// <see cref="ISystemMonitor.GetExceptions"/>
	//    /// </summary>
	//    /// <returns></returns>
	//    public ExceptionStatistics[] GetExceptions()
	//    {
	//        var array = Framework.ExceptionCounter.History.ToArray();
	//        return array.OrderByDescending(es => es.Count).Take(50).ToArray();
	//    }
	//}
}