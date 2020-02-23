using OpenQA.Selenium;
using System.Collections.Generic;
using SeleniumExtras.PageObjects;
using System;
using System.Drawing;
using System.IO;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;
using WDSE;
using System.Linq;

namespace TA_Lab.PageObjects
{
    class Bing : BaseClass
    {

        public Bing(IWebDriver driver) : base(driver)
        {

        }

        string name = "університет";
        string companyName = "Університет Короля Данила";
     
        [FindsBy(How = How.XPath, Using = "//form[@id='sb_form']/input[1]")]
        private IWebElement searchField;

        [FindsBy(How = How.XPath, Using = "//ol[@id='b_results']//h2//a")]
        private IList<IWebElement> articleHeaders;

        [FindsBy(How = How.XPath, Using = "//ul[@class='sb_pagF']//li[last()]//a")]
        private IWebElement nextPage;

        public void goToSite()
        {
            driver.Navigate().GoToUrl("https://www.bing.com/");
        }

        public void sendWord()
        {
            searchField.SendKeys(name + Keys.Enter);

        }

        public void headersToListFirstPage()
        {
            string[] allText = new string[articleHeaders.Count];
            for (int k = 0; k < articleHeaders.Count; k++)
            {
                int i = 0;
                foreach (IWebElement element in articleHeaders)
                {
                    allText[i++] = element.Text;

                }
            }

            for (int j = 0; j < articleHeaders.Count; j++)
            {

                if (allText[j] == companyName)
                    break;
            }
            MakeScreenshot1("C:/temp/", "screenbing.png");

        }

        public void headersToListAllPages()
        {
            for (int i = 1; i < 10; i++)
            {
                List<IWebElement> listOfTitles = articleHeaders.ToList();

                int iterator = 0;
                foreach (IWebElement item in listOfTitles)
                {

                    if (item.Text.Contains(companyName))
                    {
                        MakeScreenshot1("C:/temp/", "screenbingall.png");
                        int numPage = i;
                        i = 9;
                        break;
                    }
                    else
                    {
                        iterator++;
                    }
                }
                if (iterator == listOfTitles.Count - 1)
                {
                    nextPage.Click();
                }
                iterator = 0;


            }
        }

        public void MakeScreenshot1(string imageSavePath, string imageName)
        {
            var vcs = driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));
            var ms = new MemoryStream(vcs);
            Bitmap bitmap = new Bitmap(ms);
            bitmap.Save(imageSavePath + imageName);
        }
    }
}
