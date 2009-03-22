#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Quality;

namespace Lokad
{
	/// <summary>
	/// Same as <see cref="ActionPolicy"/>, but indicates that this policy
	/// holds some state and thus must have syncronized access.
	/// </summary>
	[Immutable]
	[Serializable]
	public sealed class ActionPolicyWithState : ActionPolicy
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ActionPolicyWithState"/> class.
		/// </summary>
		/// <param name="policy">The policy.</param>
		public ActionPolicyWithState(Action<Action> policy) : base(policy)
		{
		}
	}
}