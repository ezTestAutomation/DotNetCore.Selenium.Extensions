namespace OpenQA.Selenium.Extension
{
	using OpenQA.Selenium.Interactions;
	using OpenQA.Selenium.Support.UI;
	using System;
	using System.Linq;

	public static class KeywordsElementExtension
	{
		#region Messages

		const string Msg_Default = "None";
		const string Msg_Element_Not_Exist_Or_Invisible = "The element '{0}' not exist or invisible.";
		const string Msg_ElementShouldContains = "The expected value '{0}' and actual value '{1}' does not match.";
		const string Msg_Element_Not_Enable = "The element '{0}' not enable.";
		const string Msg_Element_Not_Visible = "The element '{0}' not visiable.";

		#endregion
		private static string CustomMessage(string custom1, string custom2)
		{
			if (custom1 == Msg_Default)
				return custom2;
			return custom1;
		}

		#region Private method

		private static IWebElement Find(IWebDriver webDriver, By by, int timeout = -1, bool scroll = false)
		{
			if (timeout != -1)
				webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);

			var element = webDriver.FindElement(by);
			if (element != null)
				return element;

			if (scroll)
			{
				var jscommand = $"window.scroll(0, {element.Location.Y});";
				webDriver.ExecuteJavaScript(jscommand);

				WebDriverWait wait = new WebDriverWait(webDriver, webDriver.Manage().Timeouts().ImplicitWait);
				return wait.Until((condition) =>
				{
					element = condition.FindElement(by);
					if (element.Displayed &&
						element.Enabled &&
						element.GetAttribute("aria-disabled") == null)
					{
						return element;
					}
					return null;
				});
			}

			return null;
		}

		#endregion

		public static object ExecuteJavaScript(this IWebDriver webDriver, string javaScriptCommand)
		{
			return ((IJavaScriptExecutor)webDriver).ExecuteScript(javaScriptCommand);
		}

		public static string ElementShouldContains(this IWebDriver webDriver, string locator, string expectedValue, string message = Msg_Default, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, ByExtensions.ByLocator(locator), timeout, scroll);
				if (webElement.Text == expectedValue)
					return string.Empty;
				return CustomMessage(message, string.Format(Msg_ElementShouldContains, expectedValue, webElement.Text));
			}
			catch (NoSuchElementException)
			{
				return CustomMessage(message, string.Format(Msg_Element_Not_Exist_Or_Invisible, locator));
			}
		}

		public static string ElementShouldContains(this IWebDriver webDriver, By by, string expectedValue, string message = Msg_Default, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, by, timeout, scroll);
				if (webElement.Text == expectedValue)
					return string.Empty;
				return CustomMessage(message, string.Format(Msg_ElementShouldContains, expectedValue, webElement.Text));
			}
			catch (NoSuchElementException)
			{
				return CustomMessage(message, string.Format(Msg_Element_Not_Exist_Or_Invisible, by));
			}
		}

		public static string GetText(this IWebDriver webDriver, By by, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, by, timeout, scroll);
				return webElement.Text;
			}
			catch (NoSuchElementException)
			{
				return CustomMessage("", string.Format(Msg_Element_Not_Exist_Or_Invisible, by));
			}
		}

		public static string GetText(this IWebDriver webDriver, string locator, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, ByExtensions.ByLocator(locator), timeout, scroll);
				return webElement.Text;
			}
			catch (NoSuchElementException)
			{
				return CustomMessage("", string.Format(Msg_Element_Not_Exist_Or_Invisible, locator));
			}
		}

		public static string GetDataInCell(this IWebDriver webDriver, By tableLocator, int rowIndex, string columnName, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, tableLocator, timeout, scroll);
				var columnIndex = webElement.FindElements(By.TagName("th")).ToList().FindIndex(f => f.Text == columnName);
				var row = webElement.FindElements(By.TagName("tr")).Skip(rowIndex).Take(1).FirstOrDefault();
				var cell = row.FindElements(By.TagName("td"))[columnIndex];
				return cell.Text;
			}
			catch (NoSuchElementException)
			{
				return CustomMessage("", string.Format(Msg_Element_Not_Exist_Or_Invisible, tableLocator));
			}
		}

		public static void ScrollElementToView(this IWebDriver webDriver, string locator)
		{
			var webElement = Find(webDriver, ByExtensions.ByLocator(locator));
			Actions actions = new Actions(webDriver);
			actions.MoveToElement(webElement);
			actions.Perform();
		}

		public static bool ScrollElementToView(this IWebDriver webDriver, By by)
		{
			var webElement = Find(webDriver, by);
			Actions actions = new Actions(webDriver);
			actions.MoveToElement(webElement);
			actions.Perform();
			return true;
		}

		public static string IsElementEnabled(this IWebDriver webDriver, By by, string message = Msg_Default, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, by, timeout, scroll);
				if (webElement.Enabled)
					return string.Empty;
				else
					return CustomMessage(message, string.Format(Msg_Element_Not_Enable, by));
			}
			catch (NoSuchElementException)
			{
				return CustomMessage(message, string.Format(Msg_Element_Not_Exist_Or_Invisible, by));
			}
		}

		public static string IsElementVisisble(this IWebDriver webDriver, string locator, string message = Msg_Default, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, ByExtensions.ByLocator(locator), timeout, scroll);
				if (webElement.Enabled)
					return string.Empty;
				else
					return CustomMessage(message, string.Format(Msg_Element_Not_Enable, locator));
			}
			catch (NoSuchElementException)
			{
				return CustomMessage(message, string.Format(Msg_Element_Not_Exist_Or_Invisible, locator));
			}
		}

		public static int CountElement(this IWebDriver webDriver, string locator, int timeout = -1, bool scroll = false)
		{
			try
			{
				var webElement = Find(webDriver, ByExtensions.ByLocator(locator), timeout, scroll);
				var element = webDriver.FindElements(ByExtensions.ByLocator(locator));
				return element.Count;
			}
			catch (NoSuchElementException)
			{
				return 0;
			}
		}

		public static int CountElement(this IWebDriver webDriver, By locator)
		{
			try
			{
				var element = webDriver.FindElements(locator);
				return element.Count;
			}
			catch (NoSuchElementException)
			{
				return 0;
			}
		}
	}
}
