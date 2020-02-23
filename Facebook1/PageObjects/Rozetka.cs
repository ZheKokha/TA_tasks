using OpenQA.Selenium;
using System.Collections.Generic;
using SeleniumExtras.PageObjects;
using System;
using NUnit.Framework;

namespace TA_Lab.PageObjects
{
    class Rozetka : BaseClass
    {
        string name = "книга";
        string bookPrice = "100";
        public Rozetka(IWebDriver driver) : base(driver)
            {

            }

            [FindsBy(How = How.XPath, Using = "//div[@class='search-form__input-wrapper']//input")]
            private IWebElement searchForm;

            [FindsBy(How = How.XPath, Using = "//div[@class='slider-filter__inner']/input[1]")]
            private IWebElement minPrice;

            [FindsBy(How = How.XPath, Using = "//span[@class='goods-tile__price-value']")]
            private IList<IWebElement> firstSetPrices;


            public void goToSite()
            {
                driver.Navigate().GoToUrl("https://rozetka.com.ua/");
            }


            public void searchItems()
            {
                searchForm.SendKeys(name + Keys.Enter);
            }

            public void setMinPrice()
            {
                minPrice.Clear();
                minPrice.SendKeys(bookPrice + Keys.Enter);
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
