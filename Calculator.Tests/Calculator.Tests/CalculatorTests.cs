using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Calculator.Tests
{
	public class CalculatorTests
	{
        IWebDriver browser;

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
        public void PositiveTestCalculator()
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


    }
}
