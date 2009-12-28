#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

namespace Lokad.Testing
{
	/// <summary>
	/// Extends the <see cref="MockContainer"/> and autoregisters the specified subject.
	/// </summary>
	/// <typeparam name="TSubject">The type of the subject.</typeparam>
	public sealed class MockContainer<TSubject> : MockContainer
	{
		/// <summary>
		/// Testing subject
		/// </summary>
		public TSubject Subject
		{
			get { return Resolve<TSubject>(); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MockContainer{TSubject}"/> class.
		/// </summary>
		public MockContainer()
		{
			Register<TSubject>();
		}
	}
}