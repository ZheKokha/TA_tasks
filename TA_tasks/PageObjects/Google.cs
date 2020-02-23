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
    class Google : BaseClass
    {
        string name = "університет";
        string companyName = "Європейський університет";
        public Google(IWebDriver driver) : base(driver)
        {

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
        
        public void  sendWord() 
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
       
            for (int j = 0; j<articleHeaders.Count; j++)
            {
               
                if (allText[j] == companyName)
                    break;
            }
            MakeScreenshot("C:/temp/", "screen.png");

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
                        MakeScreenshot("C:/temp/", "screenall.png");
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
        
        public void MakeScreenshot(String imageSavePath, string imageName)
        {
            var vcs = driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));
            var ms = new MemoryStream(vcs);
            Bitmap bitmap = new Bitmap(ms);
            bitmap.Save(imageSavePath + imageName);
        }




    }
}
