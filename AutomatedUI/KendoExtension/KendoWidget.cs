﻿using System;
using System.Threading;
using OpenQA.Selenium;
using SeleniumExtension;

namespace KendoExtension
{
	public abstract class KendoWidget
	{
		private IWebElement _element;
		protected IWebDriver Driver;

		protected abstract string KendoName { get; }


		protected KendoWidget(IWebDriver driver)
		{
			Driver = driver;
		}

		protected KendoWidget(IWebDriver driver, IWebElement node)
		{
			Driver = driver;
			_element = node;
		}

		protected IWebElement KendoWidgetHtmlElement()
		{
			var dataRole = KendoName.Replace("kendo", string.Empty).ToLowerInvariant();
			IWebElement dataRoleElement = _element == null ?
				Driver.ScriptQuery<IWebElement>("return $('[data-role=\"{0}\"]').get(0);".Replace("{0}", dataRole))
				: Driver.ScriptQuery<IWebElement>("return ($(arguments[0]).data('" + KendoName + "') != null)? arguments[0] : $('[data-role=\"{0}\"]', $(arguments[0])).get(0);".Replace("{0}", dataRole), _element);

			Driver.WaitFor("$(arguments[0]).data('" + KendoName + "') != null", 15, dataRoleElement);

			return dataRoleElement;
		}

		protected void ScriptExecute(string command)
		{
			var kendoWidgetHtmlElement = KendoWidgetHtmlElement();

			var cmd = command.Replace("$k", "$(arguments[0]).data('" + KendoName + "')");

			Driver.ScriptExecute(cmd, kendoWidgetHtmlElement);
			Thread.Sleep(1000);
		}

		protected T ScriptQuery<T>(string command, Func<T, int, bool> doWhile = null)
		{
			var kendoWidgetHtmlElement = KendoWidgetHtmlElement();

			var cmd = command.Replace("$k", "$(arguments[0]).data('" + KendoName + "')");

			T result;
			var cnt = 0;
			do
			{
				if (cnt != 0)
				{
					Thread.Sleep(1000);
				}

				if (typeof(T).IsValueType)
				{
					var browserResult = ((IJavaScriptExecutor)Driver).ExecuteScript(cmd, kendoWidgetHtmlElement);

					result = (T)Convert.ChangeType(browserResult, typeof(T));
				}
				else
				{
					result = Driver.ScriptQuery<T>(cmd, kendoWidgetHtmlElement);
				}
				cnt++;
			} while (doWhile != null && doWhile(result, cnt));

			return result;
		}
	}
}