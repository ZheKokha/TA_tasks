using System;
using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using System.IO;
using System.Drawing.Imaging;
using WDSE.Decorators;
using WDSE.ScreenshotMaker;
using WDSE;
using TA_Lab.PageObjects;

namespace TA_Lab
{
    public class Tests
    {
        public IWebDriver driver;
        public Actions actions;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            actions = new Actions(driver);
            driver.Manage().Window.Maximize();
        }


        [Test]
        public void WikipediaTitle() //Practice exercise 1
        {
            string url = "https://en.wikipedia.org";
            driver.Navigate().GoToUrl(url);
            string title = driver.Title;
            int titleLenght = title.Length;
            Console.WriteLine($"Title:{title}");
            Console.WriteLine($"Title length:{titleLenght}");
            string actualUrl = driver.Url;

            if (actualUrl.Equals(url))
            {
                Console.WriteLine("Verification Successful - The correct Url is opened.");
            }
            else
            {
                Console.WriteLine("Verification Failed - An incorrect Url is opened.");
            }
            string pageSource = driver.PageSource;
            int pageSourceLength = pageSource.Length;
            Console.WriteLine($"PageSource length:{pageSourceLength}");
            driver.Quit();

        }

        [Test]
        public void WikipediaNavigate()  ////Practice exercise 2
        {
            driver.Navigate().GoToUrl("https://en.wikipedia.org/");
            driver.FindElement(By.LinkText("Help")).Click();
            driver.Navigate().Back();
            driver.Navigate().Forward();
            driver.Navigate().GoToUrl("https://en.wikipedia.org/");
            driver.Navigate().Refresh();
            driver.Quit();

        }

        [Test]
        public void WikipediaWindow()  ////Practice exercise 3
        {

            driver.Manage().Window.Size = new Size(500, 600);
            driver.Manage().Window.Position = new Point(200, 150);
            driver.Manage().Window.Maximize();
            driver.Quit();

        }


        [Test]
        public void ToolsQASite()  ////Practice exercise 
        {
            driver.Navigate().GoToUrl("http://toolsqa.com/automation-practice-form/");

            IWebElement button = driver.FindElement(By.Name("sex"));
            IList<IWebElement> sexButton = driver.FindElements(By.Name("sex"));

            bool bValue = false;

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,750)");

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            bValue = sexButton.ElementAt(0).Selected;

            if (bValue == true)
            {

                sexButton.ElementAt(1).Click();
            }
            else
            {
                sexButton.ElementAt(0).Click();
            }

            IWebElement yearsButton = driver.FindElement(By.Id("exp-2"));
            yearsButton.Click();

            IList<IWebElement> checkBoxProfession = driver.FindElements(By.Name("profession"));

            int iSize = checkBoxProfession.Count;

            for (int i = 0; i < iSize; i++)
            {
                String Value = checkBoxProfession.ElementAt(i).GetAttribute("value");

                if (Value.Equals("Automation Tester"))
                {
                    checkBoxProfession.ElementAt(i).Click();
                    break;
                }
            }

            IWebElement toolCheckBox = driver.FindElement(By.CssSelector("input[value='Selenium IDE']"));
            toolCheckBox.Click();

            driver.Quit();

        }

        [Test]
        public void Facebook()

        {
            driver.Navigate().GoToUrl("https://facebook.com/");
            string email = "e-mail";
            string password = "password";
            IWebElement emailField = driver.FindElement(By.CssSelector("input#email"));
            emailField.SendKeys(email);

            IWebElement passwordField = driver.FindElement(By.CssSelector("input#pass"));
            passwordField.SendKeys(password);

            IWebElement loginButton = driver.FindElement(By.CssSelector(" label#loginbutton"));
            loginButton.Click();
            string expectedUrl = "https://www.facebook.com/";
            string actualUrl = driver.Url;
            Assert.AreEqual(actualUrl, expectedUrl);
            driver.Quit();

        }

        [Test]
        public void WikipediaScreen()
        {
            driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Main_Page");
            Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
            //Save the screenshot
            image.SaveAsFile("C:/temp/Screenshot.png", ScreenshotImageFormat.Png);
        }



        [Test]

        public void WikipediaContainerScreen()
        {
            driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Main_Page");
            IWebElement didYouKnowContainer = driver.FindElement(By.CssSelector("div#mp-dyk"));
            MakeElemScreenshot(driver, didYouKnowContainer);
            driver.Quit();

        }

        public void MakeElemScreenshot(IWebDriver driver, IWebElement elem)
        {
            Screenshot myScreenShot = ((ITakesScreenshot)driver).GetScreenshot();

            Bitmap screen = new Bitmap(new MemoryStream(myScreenShot.AsByteArray));
            Bitmap elemScreenshot = screen.Clone(new Rectangle(elem.Location, elem.Size), screen.PixelFormat);

            elemScreenshot.Save("C:/temp/Screenshot.png", ImageFormat.Png);
        }


        [Test]
        public void WikwpediaContainer2Screen()
        {
            driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Main_Page");
            IWebElement inTheNewsContainer = driver.FindElement(By.CssSelector("div#mp-itn"));
            MakeElemScreenshot1(driver, inTheNewsContainer);

            driver.Quit();

        }

        public void MakeElemScreenshot1(IWebDriver driver, IWebElement elem)
        {
            Screenshot myScreenShot = ((ITakesScreenshot)driver).GetScreenshot();

            Bitmap screen = new Bitmap(new MemoryStream(myScreenShot.AsByteArray));
            Bitmap elemScreenshot = screen.Clone(new Rectangle(elem.Location, elem.Size), screen.PixelFormat);

            elemScreenshot.Save("C:/temp/Screenshot1.png", ImageFormat.Png);
        }


        [Test]

        public void Google()
        {
           
            Google google = new Google(driver);
            google.goToSite();
            google.sendWord();
            google.headersToListFirstPage();
        }
        
       

        [Test]

        public void WikwpediaAllImageScreens()
        {
         
            driver.Navigate().GoToUrl("https://en.wikipedia.org/wiki/Main_Page");

            IList<IWebElement> allImages = driver.FindElements(By.XPath("//div[@class='thumbinner mp-thumb']//img | //div[@id='mp-bottom']//img"));

            for (int i = 0; i < allImages.Count; i++)
            {
               
                MakeScreenshot1($"C:/screens/Screenshot{i}.png", allImages[i]);
                
            }
            driver.Quit();
        }
        
        public void MakeScreenshot1(String imageSavePath, IWebElement elem)
        {
            var bytesArr = driver.TakeScreenshot(new VerticalCombineDecorator(new ScreenshotMaker()));
            var ms = new MemoryStream(bytesArr);
            Bitmap bitmap = new Bitmap(ms);
            Bitmap elemScreenshot = bitmap.Clone(new Rectangle(elem.Location, elem.Size), bitmap.PixelFormat);
            elemScreenshot.Save(imageSavePath);
            elemScreenshot.Dispose();
            
        }

        [Test]

        public void FindConsist()
        {
            Google google = new Google(driver);
            google.goToSite();
            google.sendWord();
            google.headersToListAllPages();
        }

        [Test]

        public void WikiContainer()
        {
            Wikipedia wiki = new Wikipedia(driver);
            wiki.goToSite();
            wiki.makeDidYoyKnowScreen();
            wiki.makeInTheNewsContainerScreen();
        }

        [Test]

        public void WikiContainer1()
        {
            Wikipedia wiki1 = new Wikipedia(driver);
            wiki1.goToSite();
            wiki1.makeAllImagesScreen();
        }

        [Test]
        public void Rozetka()
        {
            Rozetka rozetka = new Rozetka(driver);
            rozetka.goToSite();
            rozetka.searchItems();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollBy(0,750)");
            rozetka.setMinPrice();
            rozetka.checkFilterPrice();
            rozetka.checkFirstSet();

           
        }


        [Test]
        public void Bing()
        {
            Bing bing = new Bing(driver);
            bing.goToSite();
            bing.sendWord();
            bing.headersToListFirstPage();
                       
        }

        [Test]
        public void Yahoo()
        {
            Yahoo yahoo = new Yahoo(driver);
            yahoo.goToSite();
            yahoo.sendWord();
            yahoo.headersToListFirstPage();

        }

        [Test]
        public void AliEkspress()
        {
            AliExpress aliExpress = new AliExpress(driver);
            aliExpress.goToSite();
            aliExpress.searchItems();
            aliExpress.setMinPrice();
            aliExpress.checkFilterPrice();
            aliExpress.checkFirstSet();


        }

    }

    }








    
