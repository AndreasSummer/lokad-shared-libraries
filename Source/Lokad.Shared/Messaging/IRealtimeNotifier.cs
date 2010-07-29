using System;

namespace Lokad.Messaging
{
	/// <summary>
	/// Real-time notification interface
	/// </summary>
	public interface IRealtimeNotifier
	{
		/// <summary>
		/// Notifies the specified recipient (reliability is determined by the implementation.
		/// </summary>
		/// <param name="recipient">The recipient.</param>
		/// <param name="body">The body.</param>
		/// <param name="options">The options.</param>
		void Notify(string recipient, string body, RealtimeNotificationType options = RealtimeNotificationType.Chat);
	}
}