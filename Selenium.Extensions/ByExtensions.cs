namespace OpenQA.Selenium.Extension
{
	using System;
	using static OpenQA.Selenium.Extension.Constants;

	public class ByExtensions
	{
		public static By ByLocator(string locator)
		{
			return GetBy(locator);
		}

		private static By GetBy(string locator)
		{
			var locatorKey = "";
			var strategy = "";

			Utilities.ParseLocator(locator, ref locatorKey, ref strategy);
			StrategyEnum strategyEnum = Utilities.ParseEnum<StrategyEnum>(strategy);
			if (strategyEnum == StrategyEnum.ClassName)
				return By.ClassName(locatorKey);
			else if (strategyEnum == StrategyEnum.Css)
				return By.CssSelector(locatorKey);
			else if (strategyEnum == StrategyEnum.Tag)
				return By.TagName(locatorKey);
			else if (strategyEnum == StrategyEnum.Id)
				return By.Id(locatorKey);
			else if (strategyEnum == StrategyEnum.Link)
				return By.LinkText(locatorKey);
			else if (strategyEnum == StrategyEnum.Name)
				return By.Name(locatorKey);
			else if (strategyEnum == StrategyEnum.Partial)
				return By.PartialLinkText(locatorKey);
			else if (strategyEnum == StrategyEnum.XPath)
				return By.XPath(locatorKey);

			throw new Exception($"Locator {locator} does not support.");
		}
	}
}
