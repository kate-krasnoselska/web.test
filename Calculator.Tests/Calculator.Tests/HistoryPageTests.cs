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

            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("100");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("10");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();

            new CalculatorPage(browser).Calculate();

            browser.FindElement(By.XPath("//button[text()='History']")).Click();
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

