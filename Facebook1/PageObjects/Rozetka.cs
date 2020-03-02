using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace TA_Lab.PageObjects
{
    internal class Rozetka : BaseClass
    {
        public Rozetka(IWebDriver driver) : base(driver)
        {
            SearchForm = searchForm;
            MinPrice = minPrice;
            FirstSetPrices = firstSetPrices;
            Url = "https://rozetka.com.ua/";
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='search-form__input-wrapper']//input")]
        private IWebElement searchForm;

        [FindsBy(How = How.XPath, Using = "//div[@class='slider-filter__inner']/input[1]")]
        private IWebElement minPrice;

        [FindsBy(How = How.XPath, Using = "//span[@class='goods-tile__price-value']")]
        private IList<IWebElement> firstSetPrices;
    }
}