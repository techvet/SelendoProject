using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumExtension;

namespace Webs_Test_Framework
{
    public static class Browser
    {
        static IWebDriver webDriver = new FirefoxDriver();

        public static string Title
        {
            get { return webDriver.Title; }
        }

        public static ISearchContext SearchContext
        {
            get { return webDriver; }
        }

        public static IWebDriver Driver
        {
	        get
	        {
				webDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
		        return webDriver;
	        }
        }

		 public static bool IsHeadlinePresent(string headLineText)
		 {
             //Finds H2 and matches it to your specified headline, should be modified to match your specific headline layout
			 var pageHeadline = Driver.FindElement(SearchContext, By.CssSelector("h2")).Text;
			 return pageHeadline == headLineText || pageHeadline.Contains(headLineText);
		 }

        public static void Close()
        {
            webDriver.Close();
        }
    }
}