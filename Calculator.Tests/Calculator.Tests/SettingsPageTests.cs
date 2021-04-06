using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Calculator.Tests
{
    public class SettingsPageTests
    {
        IWebDriver browser;
        private double actual;

        [SetUp]
        public void BeforeEachTest()
        {
            browser = new ChromeDriver();
            browser.Url = "http://127.0.0.1:8080";
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            browser.FindElement(By.XPath("//input[@id = 'login']")).SendKeys("test");
            browser.FindElement(By.XPath("//input[@id = 'password']")).SendKeys("newyork1");
            browser.FindElement(By.XPath("//button [@id = 'loginBtn']")).Click();

            browser.FindElement(By.XPath("//button[text()='Settings']")).Click();
        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();
        }

        [Test]
        public void PositiveTestCancelBtnWork()
        {
            browser.FindElement(By.XPath("//button[text()='Cancel']")).Click();
            string actual = browser.Url;

            Assert.AreEqual("http://127.0.0.1:8080/Deposit", actual);
        }

        [Test]
        public void PositiveTestLogoutBtnWork()
        {
            browser.FindElement(By.XPath("//button[text()='Logout']")).Click();
            browser.SwitchTo().Alert().Accept();
            string actual = browser.Url;
            
            Assert.AreEqual("http://127.0.0.1:8080/", actual);
        }

        [Test]
        public void TestDateFormatSelection()
        {
            SelectElement dateFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//select[@id = 'dateFormat']")));
            dateFormatSelect.SelectByText("dd-MM-yyyy");
            browser.FindElement(By.XPath("//button[text()='Save']")).Click();
            browser.SwitchTo().Alert().Accept();
            
            Assert.AreEqual("http://127.0.0.1:8080/Deposit", actual);
            // Assert.AreEqual ()here I want to compare dateFormatSelect and Data in End Date field on Calc Page

            // NEED HELP at line 62. as well as I see, the app should return us to Deposit page. But assert falls. 
            // It says that actual string was 0.0d\n
        }

    }
}
