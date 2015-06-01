using NUnit.Framework;
using System;

namespace Hg.Net.Tests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void TestCase()
		{
			var a = 1;
			Assert.AreEqual(a, 1);
		}
	}
}

