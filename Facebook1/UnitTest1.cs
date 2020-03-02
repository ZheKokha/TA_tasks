using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
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

        //screenshots of specific containers
        [Test]
        public void WikiContainer()
        {
            Wikipedia wiki = new Wikipedia(driver);
            wiki.GoToSite();
            wiki.MakeInTheNewsContainerScreen();
            wiki.MakeDidYoyKnowScreen();
        }

        //screenshots of all content images
        [Test]
        public void WikiAllImages()
        {
            Wikipedia wiki1 = new Wikipedia(driver);
            wiki1.GoToSite();
            wiki1.MakeAllImagesScreen();
        }

        //search for a company name in all pages
        [Test]
        public void Google()
        {
            Google google = new Google(driver);
            google.GoToSite();
            google.SendWord();
            google.HeadersToListAllPages();
        }

        //search for a company name in all pages
        [Test]
        public void Bing()
        {
            Bing bing = new Bing(driver);
            bing.GoToSite();
            bing.SendWord();
            bing.HeadersToListAllPages();
        }

        //search for a company name in all pages
        [Test]
        public void Yahoo()
        {
            Yahoo yahoo = new Yahoo(driver);
            yahoo.GoToSite();
            yahoo.SendWord();
            yahoo.HeadersToListAllPages();
        }

        //search for items, setting min price, asserting
        [Test]
        public void Rozetka()
        {
            Rozetka rozetka = new Rozetka(driver);
            rozetka.GoToSite();
            rozetka.SearchItems();
            rozetka.Scroll();
            rozetka.SetMinPrice();
            rozetka.CheckFilterPrice();
            rozetka.CheckFirstSet();
        }

        //search for items, setting min price, asserting
        [Test]
        public void AliExpress()
        {
            AliExpress aliExpress = new AliExpress(driver);
            aliExpress.GoToSite();
            aliExpress.SigningIn();
            aliExpress.AliRegister();
            aliExpress.SearchItems();
            aliExpress.Close();
            aliExpress.SetMinPriceAli();
            aliExpress.CheckFilterPrice();
            aliExpress.CheckFirstSet();
        }

        [TearDown]
        public void Quit()
        {
            driver.Quit();
        }
    }
}