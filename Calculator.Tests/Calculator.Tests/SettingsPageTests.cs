using System;
using Calculator.Tests.Pages;
using System.Globalization;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Calculator.Tests
{
    public class SettingsPageTests
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
            SettingsPage settingsPage = new SettingsPage(browser);
            settingsPage.CancelBtn.Click();
            string actual = browser.Url;

            Assert.AreEqual("http://127.0.0.1:8080/Deposit", actual);
        }

        [Test]
        public void PositiveTestLogoutBtnWork()
        {
            SettingsPage settingsPage = new SettingsPage(browser);
            settingsPage.LogoutBtn.Click();
            browser.SwitchTo().Alert().Accept();
            string actual = browser.Url;

            Assert.AreEqual("http://127.0.0.1:8080/", actual);
        }

        [TestCase("MM dd yyyy")]
        [TestCase("dd/MM/yyyy")]
        [TestCase("dd-MM-yyyy")]
        [TestCase("MM/dd/yyyy")]
        public void SelectDateFormatIsApplied(string format)
        {
            SelectElement dateFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//select[@id = 'dateFormat']")));
            dateFormatSelect.SelectByText(format);

            SettingsPage settingsPage = new SettingsPage(browser);
            settingsPage.SaveBtn.Click();

            browser.SwitchTo().Alert().Accept();
            browser.FindElement(By.XPath("//input [@id = 'term']")).GetAttribute("value");
            DateTime expected = DateTime.Today;
            string actual = browser.FindElement(By.XPath("//input [@id = 'endDate']")).GetAttribute("value");

            Assert.AreEqual(expected.ToString (format, CultureInfo.InvariantCulture), actual);
        }

        [TestCase("123,456,789.00", "11,000.00", "1,000.00")]
        [TestCase("123.456.789,00", "11.000,00", "1.000,00")]
        [TestCase("123 456 789.00", "11 000.00", "1 000.00")]
        [TestCase("123 456 789,00", "11 000,00", "1 000,00")]
        public void SelectNumberFormat (string format, string expectedIncome, string expectedInterest)
        {
            SelectElement numberFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//select[@id = 'numberFormat']")));
            numberFormatSelect.SelectByText(format);

            SettingsPage settingsPage = new SettingsPage(browser);
            settingsPage.SaveBtn.Click();

            browser.SwitchTo().Alert().Accept();

            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("10000");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("10");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("365");
            browser.FindElement(By.XPath("//input[@type][2]")).Click();
            browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");
            browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");

            string actualIncome = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");
            string actualInterest = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");

            Assert.AreEqual(expectedIncome, actualIncome);
            Assert.AreEqual(expectedInterest, actualInterest);
        }

        [TestCase("$ - US dollar", "$")]
        [TestCase("€ - euro", "€")]
        [TestCase("£ - Great Britain Pound", "£")]
        public void SelectCurrencyFormat(string currencyName, string currencyCode)
        {
            SelectElement currencyFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//select[@id = 'currency']")));
            currencyFormatSelect.SelectByText(currencyName);

            SettingsPage settingsPage = new SettingsPage(browser);
            settingsPage.SaveBtn.Click();

            browser.SwitchTo().Alert().Accept();
            browser.FindElement(By.XPath("//td[@id = 'currency']")).GetAttribute("textContent");
            string actualCurrency = browser.FindElement(By.XPath("//td[@id = 'currency']")).GetAttribute("textContent");

            Assert.AreEqual(actualCurrency, currencyCode);
        } 

    }
}
