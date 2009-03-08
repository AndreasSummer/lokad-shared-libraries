#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using System.Windows.Forms;
using NUnit.Framework;

namespace Lokad.Client.Forms
{
	[TestFixture]
	public sealed class ControlExtensionsTests
	{
		[Test, Expects.ArgumentException]
		public void Disable_does_not_accept_disabled_controls()
		{
			var control = new Button {Enabled = false};
			control.Disable();
		}

		[Test]
		public void Disable_testing_failure()
		{
			var control = new Button();
			try
			{
				using (control.Disable())
				{
					throw new InvalidOperationException();
				}
			}
			catch (InvalidOperationException)
			{
				Assert.IsTrue(control.Enabled);
			}
		}

		[Test]
		public void Disable()
		{
			var control = new Button();
			using (control.Disable())
			{
				Assert.IsFalse(control.Enabled);
			}
			Assert.IsTrue(control.Enabled);
		}

		//[Test]
		//public void Invoke()
		//{
		//    var control = new Form();
		//    control.Show();
		//    var thread = new Thread(() => control.Invoke(control.Close));
		//    thread.Start();
		//    thread.Join();
		//}
	}
}