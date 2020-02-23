using OpenQA.Selenium;
using System.Collections.Generic;
using SeleniumExtras.PageObjects;
using System;
using NUnit.Framework;

namespace TA_Lab.PageObjects
{
    class AliExpress : BaseClass
    {
        string name = "книга";
        string bookPrice = "100";
        public AliExpress(IWebDriver driver) : base(driver)
        {

        }

        [FindsBy(How = How.XPath, Using = "//div[@class='search-key-box']//input")]
        private IWebElement searchForm;

        [FindsBy(How = How.XPath, Using = "//span[@class='next-input next-small min-price']//input")]
        private IWebElement minPrice;

        [FindsBy(How = How.XPath, Using = "//span[@class='price-input popmode ltr']//a")]
        private IWebElement okButton;
      
        [FindsBy(How = How.XPath, Using = "//span[@class='price-current']")]
        private IList<IWebElement> firstSetPrices;


        public void goToSite()
        {
            driver.Navigate().GoToUrl("https://aliexpress.ru/");
        }

        public void searchItems()
        {
            searchForm.SendKeys(name + Keys.Enter);
        }

        public void setMinPrice()
        {
            minPrice.Clear();
            minPrice.SendKeys(bookPrice);
            okButton.Click();
        }

        public void checkFilterPrice()
        {
            Assert.AreEqual(bookPrice, "100");
        }

        public void checkFirstSet()
        {
            for (int i = 0; i < firstSetPrices.Count; i++)
            {
                //if (Convert.ToInt32(firstSetPrices[i]) < Convert.ToInt32(bookPrice));
                //continue;
                Assert.LessOrEqual(Convert.ToInt32(bookPrice), Convert.ToInt32(firstSetPrices[i]));

            }
        }

    }
}
