using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace TA_Lab.PageObjects
{
    internal class Google : BaseClass
    {
        public Google(IWebDriver driver) : base(driver)
        {
            SearchField = searchField;
            ArticleHeaders = articleHeaders;
            NextPage = nextPage;
        }

        [FindsBy(How = How.CssSelector, Using = "input.gLFyf.gsfi")]
        private IWebElement searchField;

        [FindsBy(How = How.XPath, Using = "//div[@class='srg']//h3")]
        private IList<IWebElement> articleHeaders;

        [FindsBy(How = How.XPath, Using = "//table[@id='nav']//td[last()]//span[2]")]
        private IWebElement nextPage;

        public void goToSite()
        {
            driver.Navigate().GoToUrl("https://www.google.com.ua/?hl=ru");
        }
    }
}