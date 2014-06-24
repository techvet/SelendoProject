using System;
using OpenQA.Selenium;
using SeleniumExtension;

namespace Webs_Test_Framework
{
	public class BasePage
	{
        private static string baseURL = "http://www.yourbasepage.com/";

		public static IWebDriver Driver
        {
            get { return Browser.Driver; }
        }

		public static ISearchContext SearchContext
		{
			get { return Driver; }
		}

		public string Title { get { return Browser.Title; }}

        public TBasePage NavigateTo<TBasePage>(By by) where TBasePage:BasePage, new()
        {
            Driver.FindElement(by).Click();
            return Activator.CreateInstance<TBasePage>();
        }

        public static void Execute(By by, Action<IWebElement> action)
        {
            var element = Browser.Driver.FindElement(Browser.SearchContext, by);
            action(element);
        }

        public static void SetText(By by, string newText)
        {
            Execute(by, e =>
                {
                    e.Clear();
                    e.SendKeys(newText);
                } );
        }

		public static void Login()
		{
			
		}

		public static void Goto(string url)
		{
			Driver.Url = baseURL + url;
		}

		public static void GotoHome()
		{
			Driver.Url = baseURL;
		}

		public static bool IsHeadlinePresent(string headLineText)
		{
			return Browser.IsHeadlinePresent(headLineText);
		}

		public static void Close()
		{
			Browser.Close();
		}
    }
}
