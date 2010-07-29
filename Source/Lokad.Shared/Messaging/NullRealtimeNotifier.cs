#region (c)2009-2010 Lokad - New BSD license

// Copyright (c) Lokad 2009-2010 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Messaging
{
	/// <summary>
	/// Realtime notifier, that does not do anything
	/// </summary>
	public sealed class NullRealtimeNotifier : IRealtimeNotifier
	{
		/// <summary>
		/// Singleton instance of the <see cref="IRealtimeNotifier"/>
		/// </summary>
		public static readonly IRealtimeNotifier Instance = new NullRealtimeNotifier();

		NullRealtimeNotifier()
		{
		}

		/// <summary>
		/// Notifies the specified recipient (reliability is determined by the implementation.
		/// </summary>
		/// <param name="recipient">The recipient.</param>
		/// <param name="body">The body.</param>
		/// <param name="options">The options.</param>
		public void Notify(string recipient, string body, RealtimeNotificationType options)
		{
		}
	}
}