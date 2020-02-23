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
    class Wikipedia:BaseClass
    {
        public Wikipedia(IWebDriver driver) : base(driver)
        {

        }

        [FindsBy(How = How.CssSelector, Using = "div#mp-dyk")]
        private IWebElement didYouKnowContainer;

        [FindsBy(How = How.CssSelector, Using = "div#mp-itn")]
        private IWebElement inTheNewsContainer;

        [FindsBy(How = How.XPath, Using = "//div[@class='thumbinner mp-thumb']//img | //div[@id='mp-bottom']//img")]
        private IList<IWebElement> allImages;
        

        public void goToSite()
        {
            driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Main_Page");
        }

        public void makeDidYoyKnowScreen()
        {
            MakeScreenshot("C:/temp/screen1.png", didYouKnowContainer);
        }

        public void makeInTheNewsContainerScreen()
        {
            MakeScreenshot("C:/temp/screen2.png", inTheNewsContainer);
        }

        public void makeAllImagesScreen()
        {
            IList<IWebElement> allImage = allImages; 
            for (int i = 0; i < allImage.Count; i++)
            {

                MakeScreenshot($"C:/temp/Screenshot{i}.png", allImages[i]);

            }
        }

        public void MakeScreenshot(String imageSavePath, IWebElement elem)
        {
            var bytesArr = driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));
            var ms = new MemoryStream(bytesArr);
            Bitmap bitmap = new Bitmap(ms);
            Bitmap elemScreenshot = bitmap.Clone(new Rectangle(elem.Location, elem.Size), bitmap.PixelFormat);
            elemScreenshot.Save(imageSavePath);
            elemScreenshot.Dispose();

        }

        //public void MakeScreenshot(String imageSavePath, string imageName, IWebElement elem)
        //{
        //    var vcs = driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));
        //    var ms = new MemoryStream(vcs);
        //    Bitmap bitmap = new Bitmap(ms);
        //    bitmap.Save(imageSavePath + imageName);
        //}
    }
}
