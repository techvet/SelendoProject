using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace KendoExtension
{
	public class KendoMenu
	{
        private IWebElement _element;
		protected static IWebDriver Driver;

		protected KendoMenu(IWebDriver driver)
		{
			Driver = driver;
		}

		protected KendoMenu(IWebDriver driver, IWebElement node)
		{
			Driver = driver;
			_element = node;
		}

		public static void MenuSelect(string topLevelMenuId, string linkText)
		{
			Actions builder = new Actions(Driver);
			builder.MoveToElement(Driver.FindElement(By.LinkText(topLevelMenuId)));
			builder.MoveByOffset(0,5);
			builder.Perform();
			var link = Driver.FindElement(By.LinkText(linkText));
			link.Click();
		}
	}
}
