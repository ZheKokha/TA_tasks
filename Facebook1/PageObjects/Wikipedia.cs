using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace TA_Lab.PageObjects
{
    internal class Wikipedia : BaseClass
    {
        public Wikipedia(IWebDriver driver) : base(driver)
        {
            DidYouKnowContainer = didYouKnowContainer;
            InTheNewsContainer = inTheNewsContainer;
            AllImages = allImages;
            Url = "https://en.wikipedia.org/wiki/Main_Page";
        }

        [FindsBy(How = How.CssSelector, Using = "div#mp-dyk")]
        private IWebElement didYouKnowContainer;

        [FindsBy(How = How.CssSelector, Using = "div#mp-itn")]
        private IWebElement inTheNewsContainer;

        [FindsBy(How = How.XPath, Using = "//table//img")]
        private IList<IWebElement> allImages;
    }
}