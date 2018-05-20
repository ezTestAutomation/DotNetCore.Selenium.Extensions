namespace OpenQA.Selenium.Extension
{
	using System;

	public class Utilities
	{
		/// <summary>
		/// ParseLocator
		/// </summary>
		/// <param name="locator"></param>
		/// <param name="locatorKey"></param>
		/// <param name="strategy"></param>
		public static void ParseLocator(string locator, ref string locatorKey, ref string strategy)
		{
			string[] arr = locator.Split(':');
			if (arr.Length == 2)
			{
				strategy = arr[0];
				locatorKey = arr[1];
			}
			else if (arr.Length > 2)
			{
				strategy = arr[0];
				locatorKey = locator.Replace($"{arr[0]}:", "");
			}
			else
			{
				strategy = "Id";
				locatorKey = locator;
			}
		}

		/// <summary>
		/// ParseEnum<T>
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static T ParseEnum<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}
	}
}
