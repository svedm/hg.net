using NUnit.Framework;
using System;

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
	}
}

