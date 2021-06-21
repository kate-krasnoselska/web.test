using System;
using System.Threading;
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
            // NEED HELP: how to say that historyPage.CalculatedData is dissapeared?
            // 'no such element' 
            HistoryPage historyPage = new HistoryPage(browser);
            //historyPage.CalculatedData.GetAttribute("textContent");
   
            historyPage.ClearBtn.Click();

            Assert.IsFalse(historyPage.CalculatedData.Displayed);
           
        }

        [Test]
        public void HeaderAmountIsDisplayed()
        {
            HistoryPage historyPage = new HistoryPage(browser);
            historyPage.HeaderAmount.GetAttribute("textContent");

            string actualHeaderAmount = historyPage.HeaderAmount.GetAttribute("textContent");

            Assert.AreEqual("Amount", actualHeaderAmount);
        }

        [Test]
        public void CalculatedDataIsDisplayed()
        {
            HistoryPage historyPage = new HistoryPage(browser);
            historyPage.CalculatedData.GetAttribute("textContent");

            string actualCalculatedData = historyPage.CalculatedData.GetAttribute("textContent");

            Assert.AreEqual("100", actualCalculatedData);
        }

        [Test]
        public void CalculateRowsAmount()
        {
            HistoryPage historyPage = new HistoryPage(browser);
            historyPage.RowCount.ToString();

            string actual = historyPage.RowCount.ToString();

            Assert.AreEqual("1", historyPage.RowCount.ToString());
        }

      
    }
}


