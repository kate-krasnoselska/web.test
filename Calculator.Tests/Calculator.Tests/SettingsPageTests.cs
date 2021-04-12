using System;
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

            // Assert.AreEqual("http://127.0.0.1:8080/Deposit", actual);
            // Assert.AreEqual ()here I want to compare dateFormatSelect and Data in End Date field on Calc Page

            // NEED HELP at line 62. as well as I see, the app should return us to Deposit page. But assert falls. 
            // It says that actual string was 0.0d\n
        }

        [TestCase("MM dd yyyy")]
        [TestCase("dd/MM/yyyy")]
        [TestCase("dd-MM-yyyy")]
        [TestCase("MM/dd/yyyy")]
        public void SelectDateFormatIsApplied(string format)
        {
            // 1. Select the Date format "MM dd yyyy" from dropdown
            // 2. Click Save
            // 3. Click OK alert
            // 4. See End date is applied correctly

            SelectElement dateFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//select[@id = 'dateFormat']")));
            dateFormatSelect.SelectByText(format);
            browser.FindElement(By.XPath("//button[text()='Save']")).Click();
            browser.SwitchTo().Alert().Accept();
            browser.FindElement(By.XPath("//input [@id = 'term']")).GetAttribute("value");
            DateTime expected = DateTime.Today;
            string actual = browser.FindElement(By.XPath("//input [@id = 'endDate']")).GetAttribute("value");

            Assert.AreEqual(expected.ToString (format, CultureInfo.InvariantCulture), actual);
        }

        [TestCase("123,456,789.00")]
        [TestCase("123.456.789,00")]
        [TestCase("123 456 789.00")]
        [TestCase("123 456 789,00")]
        public void SelectNumberFormatIsApplied(string format)
        {
            // 1. Select the Number format
            // 2. Click Save
            // 3. Click OK alert
            // 4. Check if selected number format = income field format
            // 5. check if selected number format = interest earned field format

            SelectElement numberFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//select[@id = 'numberFormat']")));
            numberFormatSelect.SelectByText(format);
            browser.FindElement(By.XPath("//button[text()='Save']")).Click();
            browser.SwitchTo().Alert().Accept();

            browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");
            // browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");

            string actual = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");

            Assert.AreEqual(format, actual);
            //Assert.AreEqual(format, );

            // NEED HELP: how to add second assert? it could not be named actual too, right?
        }

        [TestCase("$ - US dollar")]
        [TestCase("€")]
        [TestCase("£")]
        public void SelectCurrencyFormatIsApplied(string format)

        {
            // 1. Select the Currency format
            // 2. Click Save
            // 3. Click OK alert
            // 4. Check if selected currency format is applied on Calculator page

            SelectElement currencyFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//select[@id = 'currency']")));
            currencyFormatSelect.SelectByText(format);
            browser.FindElement(By.XPath("//button[text()='Save']")).Click();
            browser.SwitchTo().Alert().Accept();
            browser.FindElement(By.XPath("//td[@id = 'currency']")).GetAttribute("textContent");
            string actual = browser.FindElement(By.XPath("//td[@id = 'currency']")).GetAttribute("textContent");

            Assert.AreEqual(format, actual);
        } 

    }
}
