using System;
using Calculator.Tests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Calculator.Tests
{
    public class HistoryPageTests
    {
        IWebDriver browser;

        [SetUp]
        public void BeforeEachTest()
        {
            browser = new ChromeDriver();
            browser.Url = "http://127.0.0.1:8080";
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            new LoginPage(browser).Login();

            new CalculatorPage(browser).EnterCalculatorData("100", "10", "365", true);
            new CalculatorPage(browser).Calculate();
            // can not make these two methods as one, because part of .Calculate is Private

            new CalculatorPage(browser).OpenHistoryPage();
        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();
        }

        [Test]
        public void CalculatorButtonWork()
        {
            HistoryPage historyPage = new HistoryPage(browser);
            historyPage.CalculatorBtn.Click();

            string actual = browser.Url;

            Assert.AreEqual("http://127.0.0.1:8080/Deposit", actual);
        }

        [Test]
        public void ClearBtnWork()
        {
            HistoryPage historyPage = new HistoryPage(browser);
            historyPage.ClearBtn.Click();
            // NEED HELP: Which assert to use here? I want to say that data is cleared
            //Assert.AreEqual(historyPage.CalculatedAmount, " ");
        }


    }
}


