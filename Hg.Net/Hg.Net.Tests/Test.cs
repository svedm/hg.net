using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Hg.Net.Tests
{
	[TestFixture]
	public class HgClinetTest
	{
		[Test]
		public void StartTest()
		{
			var hgClient = new HgClient();
			var res = hgClient.Connect(@"C:\\");
			Assert.AreEqual(res, true);
			Assert.IsFalse(string.IsNullOrEmpty(hgClient.HgEncoding));
			Assert.IsTrue(hgClient.Capabilities.Count > 0);
		}

		[Test]
		public void RunCommandTest()
		{
			var hgClient = new HgClient();
			var res = hgClient.Connect(@"C:\\");
			hgClient.RunCommand(new List<string> { "summary" });
		}
	}
}

