#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Client
{
	/// <summary>
	/// Generic interface to represent SKU, Queues etc
	/// </summary>
	public interface IForecastModel
	{
		/// <summary>
		/// Gets or sets the human-readable name.
		/// </summary>
		/// <value>The human-readable name.</value>
		string Name { get; set; }

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		string Identifier { get; }

		/// <summary>
		/// Gets or sets the events.
		/// </summary>
		/// <value>The events.</value>
		EventModel[] Events { get; set; }

		/// <summary>
		/// Gets or sets the tags.
		/// </summary>
		/// <value>The tags.</value>
		string[] Tags { get; set; }
	}
}