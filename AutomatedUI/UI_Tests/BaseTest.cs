using NUnit.Framework;
using Webs_Test_Framework;

namespace UI_Tests
{
	[TestFixture]
	public class BaseTest
	{
		[TestFixtureSetUp]
		public void Init()
		{
			BasePage.GotoHome();
			BasePage.Login();
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			Browser.Close();
		}
	}
}
