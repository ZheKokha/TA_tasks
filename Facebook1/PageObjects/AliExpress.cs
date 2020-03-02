using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;

namespace TA_Lab.PageObjects
{
    internal class AliExpress : BaseClass
    {
        public AliExpress(IWebDriver driver) : base(driver)
        {
            SearchForm = searchForm;
            MinPrice = minPrice;
            FirstSetPrices = firstSetPrices;
            OkButton = okButton;
            Login = login;
            Password = password;
            Iframe = iframe;
            Iframe1 = Iframe1;
            SignIn = signIn;
            CloseAdd = closeAdd;
            CloseButton = close;
            Url = "https://aliexpress.ru/";
        }

        [FindsBy(How = How.XPath, Using = "//div[@class='search-key-box']//input")]
        private IWebElement searchForm;

        [FindsBy(How = How.XPath, Using = "//span[@class='next-input next-small min-price']//input")]
        private IWebElement minPrice;

        [FindsBy(How = How.XPath, Using = "//span[@class='price-input popmode ltr']//a")]
        private IWebElement okButton;

        [FindsBy(How = How.XPath, Using = "//span[@class='price-current']")]
        private IList<IWebElement> firstSetPrices;

        [FindsBy(How = How.XPath, Using = "//iframe[@id='alibaba-login-box']")]
        private IWebElement iframe;

        [FindsBy(How = How.CssSelector, Using = "span.register-btn a")]
        private IWebElement signIn;

        [FindsBy(How = How.CssSelector, Using = "input#fm-login-id")]
        private IWebElement login;

        [FindsBy(How = How.CssSelector, Using = "input#fm-login-password")]
        private IWebElement password;

        [FindsBy(How = How.XPath, Using = "//a[@class='close-layer']")]
        private IWebElement closeAdd;

        [FindsBy(How = How.XPath, Using = "//div[@class='next-dialog next-closeable ui-newuser-layer-dialog']/a")]
        private IWebElement close;
    }
}