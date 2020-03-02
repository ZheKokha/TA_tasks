using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace TA_Lab.PageObjects
{
    internal class Yahoo : BaseClass
    {
        public Yahoo(IWebDriver driver) : base(driver)
        {
            SearchField = searchField;
            ArticleHeaders = articleHeaders;
            NextPage = nextPage;
            Url = "https://www.yahoo.com/";
        }

        [FindsBy(How = How.XPath, Using = "//form[@id='header-search-form']/input[1]")]
        private IWebElement searchField;

        [FindsBy(How = How.XPath, Using = "//div[@class='compTitle options-toggle']//a")]
        private IList<IWebElement> articleHeaders;

        [FindsBy(How = How.XPath, Using = "//div[@class='compPagination']//a[last()]")]
        private IWebElement nextPage;
    }
}