namespace OpenQA.Selenium.Extension
{
    class Constants
    {
		public enum StrategyEnum
		{
			Id = 1, //Element id. 	id:example
			Name = 2, //name attribute. 	name:example
			Identifier = 3, //Either id or name. 	identifier:example
			ClassName = 4, //Element class. 	class:example
			Tag = 5, //Tag name. 	tag:div
			XPath = 6, //XPath expression. 	xpath://div[@id="example"]
			Css = 7, //CSS selector. 	css:div#example
			Dom = 8, //DOM expression. 	dom:document.images[5]
			Link = 9, //Exact text a link has. 	link:The example
			Partial = 10, //link 	Partial link text. 	partial link:he ex
			Sizzle = 11, //Sizzle selector provided by jQuery. 	sizzle:div.example
			Jquery = 12, //Same as the above. 	jquery:div.example
		}
	}
}
