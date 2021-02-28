using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Calculator.Tests
{
	public class CalculatorTests
	{
        IWebDriver browser;
        private object driver;

        [SetUp]
        public void BeforeEachTest()
        {
            browser = new ChromeDriver();
            browser.Url = "http://127.0.0.1:8080";
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            browser.FindElement(By.Id("login")).SendKeys("test");
            browser.FindElement(By.Id("password")).SendKeys("newyork1");
            browser.FindElement(By.Id("loginBtn")).Click();

        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();

        }

        [Test]
        public void PositiveTestCalculator365days()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute ("value");
            Assert.AreEqual("110.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("10.00", actualInterest);

        }

        [Test]
        public void PositiveTestCalculator360days()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("360");
            browser.FindElement(By.Id("d360")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");
            Assert.AreEqual("110.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("10.00", actualInterest);

        }

        [Test]
        public void PositiveTestDepositAmountIsMandatoryField()
        {
            browser.FindElement(By.Id("amount")).SendKeys(" ");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");
            Assert.AreEqual("0.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);

        }

        [Test]
        public void PositiveTestRateOfInterestIsMandatoryField()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys(" ");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");
            Assert.AreEqual("100.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);

        }

        [Test]
        public void PositiveTestInvestmentTermIsMandatoryField()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys(" ");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");
            Assert.AreEqual("100.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);

        }

        [Test]
        public void PositiveTestStartDateIsMandatoryField()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("365");
            SelectElement selectElement = new SelectElement((IWebElement)By.Id("day"));
            selectElement.SelectByValue("28");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");
            Assert.AreEqual("100.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("0.00", actualInterest);

        }


    }
}
