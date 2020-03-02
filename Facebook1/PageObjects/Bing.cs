using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace TA_Lab.PageObjects
{
    internal class Bing : BaseClass
    {
        public Bing(IWebDriver driver) : base(driver)
        {
            SearchField = searchField;
            ArticleHeaders = articleHeaders;
            NextPage = nextPage;
            Url = "https://www.bing.com/";
        }

        private string companyName = "Університет Короля Данила";

        [FindsBy(How = How.XPath, Using = "//form[@id='sb_form']/input[1]")]
        private IWebElement searchField;

        [FindsBy(How = How.XPath, Using = "//ol[@id='b_results']//h2//a")]
        private IList<IWebElement> articleHeaders;

        [FindsBy(How = How.XPath, Using = "//ul[@class='sb_pagF']//li[last()]//a")]
        private IWebElement nextPage;
    }
}