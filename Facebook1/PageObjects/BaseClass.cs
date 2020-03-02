using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using WDSE;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;

namespace TA_Lab.PageObjects
{
    internal class BaseClass
    {
        public IWebDriver driver;
        public WebDriverWait wait;
        public virtual IList<IWebElement> ArticleHeaders { get; set; }
        public IWebElement SearchField { get; set; }
        public IWebElement NextPage { get; set; }
        public IWebElement DidYouKnowContainer { get; set; }
        public IWebElement InTheNewsContainer { get; set; }
        public IList<IWebElement> AllImages { get; set; }
        public IWebElement SearchForm { get; set; }
        public IWebElement MinPrice { get; set; }
        public IList<IWebElement> FirstSetPrices { get; set; }
        public IWebElement OkButton { get; set; }
        public IWebElement Login { get; set; }
        public IWebElement Password { get; set; }
        public IWebElement Iframe { get; set; }
        public IWebElement Iframe1 { get; set; }
        public IWebElement SignIn { get; set; }
        public IWebElement CloseAdd { get; set; }
        public IWebElement CloseButton { get; set; }
        public string Url { get; set; }

        private string name = "університет";
        private string companyName = "Європейський університет";
        private string imageName = "screen" + DateTime.Now.ToString("HH_mm_ss") + ".png";
        private string itemname = "книга";
        private string bookPrice = "20";

        public BaseClass(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(driver, this);
        }

        public void GoToSite()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void SendWord()
        {
            SearchField.SendKeys(name + Keys.Enter);
        }

        public void HeadersToListFirstPage()
        {
            string[] allText = new string[ArticleHeaders.Count];
            for (int k = 0; k < ArticleHeaders.Count; k++)
            {
                int i = 0;
                foreach (IWebElement element in ArticleHeaders)
                {
                    allText[i++] = element.Text;
                }
            }

            for (int j = 0; j < ArticleHeaders.Count; j++)
            {
                if (allText[j] == companyName)
                    break;
            }
            MakeScreenshot("C:/temp/", imageName);
        }

        public void HeadersToListAllPages()
        {
            for (int i = 1; i < 10; i++)
            {
                List<IWebElement> listOfTitles = ArticleHeaders.ToList();

                int iterator = 0;
                foreach (IWebElement item in listOfTitles)
                {
                    if (item.Text.Contains(companyName))
                    {
                        MakeScreenshot("C:/temp/", imageName);
                        int numPage = i;
                        i = 9;
                        break;
                    }
                    else
                    {
                        iterator++;
                    }
                }
                if (iterator == listOfTitles.Count)
                {
                    NextPage.Click();
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

        public void MakeElementScreenshot(String imageSavePath, IWebElement elem)
        {
            var bytesArr = driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));
            var ms = new MemoryStream(bytesArr);
            Bitmap bitmap = new Bitmap(ms);
            Bitmap elemScreenshot = bitmap.Clone(new Rectangle(elem.Location, elem.Size), bitmap.PixelFormat);
            elemScreenshot.Save(imageSavePath);
            elemScreenshot.Dispose();
        }

        public void MakeDidYoyKnowScreen()
        {
            MakeElementScreenshot($"C:/temp/{imageName}", DidYouKnowContainer);
        }

        public void MakeInTheNewsContainerScreen()
        {
            MakeElementScreenshot($"C:/temp/{imageName}", InTheNewsContainer);
        }

        public void MakeAllImagesScreen()
        {
            IList<IWebElement> allImage = AllImages;
            for (int i = 0; i < allImage.Count; i++)
            {
                MakeElementScreenshot($"C:/temp/Screenshoot{i}.png", AllImages[i]);
            }
        }

        public void SearchItems()
        {
            SearchForm.SendKeys(itemname + Keys.Enter);
        }

        public void SetMinPrice()

        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='slider-filter__inner']/input[1]")));
            MinPrice.Clear();
            MinPrice.SendKeys(bookPrice + Keys.Enter);
        }

        public void SetMinPriceAli()
        {
            MinPrice.Clear();
            MinPrice.SendKeys(bookPrice);
            OkButton.Click();
        }

        public void CheckFilterPrice()
        {
            Assert.AreEqual(bookPrice, "20");
        }

        public void CheckFirstSet()
        {
            string[] allPrice = new string[FirstSetPrices.Count];
            for (int k = 0; k < FirstSetPrices.Count; k++)
            {
                foreach (IWebElement elements in FirstSetPrices)
                {
                    allPrice[k++] = elements.Text;
                }
            }

            for (int i = 0; i < allPrice.Length; i++)
            {
                Console.WriteLine(allPrice[i]);
            }

            for (int i = 0; i < allPrice.Length; i++)
            {
                int temp = 0;
                string element = allPrice[i];
                Int32.TryParse(Filter(element), out temp);
                Assert.LessOrEqual(Convert.ToInt32(bookPrice), temp);
            }
        }

        private string Filter(string NumInString)
        {
            string digit = "0123456789";
            string result = "";
            for (int i = 0; i < NumInString.Length; i++)
            {
                for (int j = 0; j < digit.Length; j++)
                {
                    if (NumInString[i] == digit[j])
                    {
                        result += NumInString[i];
                    }
                }
                if (NumInString[i] == ',')
                {
                    break;
                }
            }
            return result;
        }

        public void Scroll()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,1000)");
        }

        public void SigningIn()
        {
            if (CloseAdd.Displayed)
            { CloseAdd.Click(); }
            SignIn.Click();
        }

        public void Close()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='next-dialog next-closeable ui-newuser-layer-dialog']/a")));
            if (CloseButton.Enabled)
                CloseButton.Click();
        }

        public void AliRegister()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//iframe[@id='alibaba-login-box']")));
            driver.SwitchTo().Frame(Iframe);
            Login.SendKeys("zheniakokhan@ukr.net");
            Password.SendKeys("aa1111" + Keys.Enter);
            driver.SwitchTo().DefaultContent();
        }
    }
}