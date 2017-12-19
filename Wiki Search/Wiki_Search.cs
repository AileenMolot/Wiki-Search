using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Wiki_Search
{
    [TestFixture]
    public class Base
    {
        public IWebDriver driver;
        public StringBuilder verificationErrors;
        public string baseURL;
        public bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.google.com.ua/";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

    }

    public class FindWiki : Base
    {
        [Test]
        public void TheFindWikiTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/search?q=wikipedia&ie=utf-8&oe=utf-8&gws_rd=cr&dcr=0&ei=NRE5Wsr1AY2asAfI2ZXgCA");
            driver.FindElement(By.LinkText("Вікіпедія")).Click();
            Assert.IsTrue(Regex.IsMatch(driver.Url, "^[\\s\\S]*wikipedia\\.org[\\s\\S]*$"));
        }

    }

    public class CheckDate : Base
    {
        [Test]
        public void TheCheckDateTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/search?q=wikipedia&ie=utf-8&oe=utf-8&gws_rd=cr&dcr=0&ei=NRE5Wsr1AY2asAfI2ZXgCA");
            driver.FindElement(By.LinkText("Вікіпедія")).Click();
            Assert.AreEqual("19 грудня", driver.FindElement(By.LinkText("19 грудня")).Text);
        }

    }

    public class Current : Base
    {
        [Test]
        public void TheCurrentTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/search?q=wikipedia&ie=utf-8&oe=utf-8&gws_rd=cr&dcr=0&ei=NRE5Wsr1AY2asAfI2ZXgCA");
            driver.FindElement(By.LinkText("Вікіпедія")).Click();
            driver.FindElement(By.LinkText("Поточні події")).Click();
            Assert.AreEqual("https://uk.wikipedia.org/wiki/%D0%92%D1%96%D0%BA%D1%96%D0%BF%D0%B5%D0%B4%D1%96%D1%8F:%D0%9F%D0%BE%D1%82%D0%BE%D1%87%D0%BD%D1%96_%D0%BF%D0%BE%D0%B4%D1%96%D1%97", driver.Url);
        }

    }

    public class OctoberNews : Base
    {
        [Test]
        public void TheOctoberNewsTest()
        {
            try
            {
                driver.Navigate().GoToUrl(baseURL + "/search?q=wikipedia&ie=utf-8&oe=utf-8&gws_rd=cr&dcr=0&ei=NRE5Wsr1AY2asAfI2ZXgCA");
                driver.FindElement(By.LinkText("Вікіпедія")).Click();
                driver.FindElement(By.LinkText("Поточні події")).Click();
                Assert.IsTrue(IsElementPresent(By.LinkText("23 жовтня")));
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            Assert.IsTrue(IsElementPresent(By.XPath("//div[@id='mw-content-text']/div/table/tbody/tr/td/div[4]/ul[2]/li[33]/ul/li")));
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }

    public class FindPairwiseTesting: Base
    {
        [Test]
        public void TheFindPairwiseTestingTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/search?q=wikipedia&ie=utf-8&oe=utf-8&gws_rd=cr&dcr=0&ei=NRE5Wsr1AY2asAfI2ZXgCA");
            driver.FindElement(By.LinkText("Вікіпедія")).Click();
            driver.FindElement(By.Id("searchInput")).Click();
            driver.FindElement(By.Id("searchInput")).Clear();
            driver.FindElement(By.Id("searchInput")).SendKeys("Testing");
            driver.FindElement(By.Id("searchButton")).Click();
            driver.FindElement(By.XPath("//div[@id='mw-content-text']/div[2]/ul/li[4]/div/a/span")).Click();
            Assert.AreEqual("https://uk.wikipedia.org/wiki/Pairwise_testing", driver.Url);
        }
    }
}

