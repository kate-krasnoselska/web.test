using System;
using Calculator.Tests.Pages;
using System.Globalization;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;

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

            new LoginPage(browser)
                .Login()
                .OpenSettingsPage()
                .SetSettingsData("dd-MM-yyyy", "123 456 789.00", "$ - US dollar");

            //LoginPage loginPage = new LoginPage(browser);
            //loginPage.Login();

            //calculatorPage = new LoginPage(browser).Login();
            //calculatorPage.OpenSettingsPage().SetSettingsData("dd-MM-yyyy", "123 456 789.00", "$ - US dollar");

            //browser.FindElement(By.Id("login")).SendKeys("test");
            //browser.FindElement(By.Id("password")).SendKeys("newyork1");
            //browser.FindElement(By.Id("loginBtn")).Click();

            //browser.FindElement(By.XPath("//button[text()='Settings']")).Click();

            //SettingsPage settingsPage = new SettingsPage(browser);
            //settingsPage.Set("dd-MM-yyyy", "123 456 789.00", "$ - US dollar");

            //new SettingsPage(browser).SetSettingsData("dd-MM-yyyy", "123 456 789.00", "$ - US dollar");

            /*SelectElement dateFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//select[@id = 'dateFormat']")));
            dateFormatSelect.SelectByText("dd-MM-yyyy");

            SelectElement numberFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'numberFormat']")));
            numberFormatSelect.SelectByText("123 456 789.00");

            SelectElement currenceFormatSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'currency']")));
            currenceFormatSelect.SelectByText("$ - US dollar");*/
        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();
        }

        [Test]
        public void PositiveTestCalculator365days()
        {
            new CalculatorPage(browser)
                .EnterCalculatorData365days("100", "10", "365");

            new CalculatorPage(browser).Calculate();

            string actualIncome = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");
            Assert.AreEqual("110.00", actualIncome);
           
            string actualInterest = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");
            Assert.AreEqual("10.00", actualInterest);

            //calculatorPage.DepositAmountFld.SendKeys("100");
            //calculatorPage.RateOfInterestFld.SendKeys("10");
            //calculatorPage.InvestmentTermFld.SendKeys("365");
            //calculatorPage.FinancialYearRadio365.Click();

            //browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("100");
            //browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("10");
            //browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("365");
            //browser.FindElement(By.XPath("//input[@type][2]")).Click();

            //new WebDriverWait(browser, TimeSpan.FromSeconds(10))
            //.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(By.XPath("//button [@id = 'calculateBtn']")));

            //browser.FindElement(By.XPath("//button [@id = 'calculateBtn']")).Click();
            //Thread.Sleep(1000);

            //new WebDriverWait(browser, TimeSpan.FromSeconds(10))
            //.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//input [@id = 'interest']")));
        }

        [Test]
        public void PositiveTestCalculator360days()
        {
            new CalculatorPage(browser)
                .EnterCalculatorData360days("100", "10", "360");

            new CalculatorPage(browser).Calculate();

            string actualIncome = browser.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");
            Assert.AreEqual("110.00", actualIncome);

            string actualInterest = browser.FindElement(By.XPath("//input [@id = 'interest']")).GetAttribute("value");
            Assert.AreEqual("10.00", actualInterest);
        }

        [Test]
        public void PositiveTestDepositAmountIsMandatoryField()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);

            new CalculatorPage(browser)
                .EnterCalculatorData365days(" ", "10", "365");

            Assert.AreEqual(false, calculatorPage.CalculateBtn.Enabled);
        }

        [Test]
        public void PositiveTestRateOfInterestIsMandatoryField()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);

            new CalculatorPage(browser)
                .EnterCalculatorData365days("100", " ", "365");

            Assert.AreEqual(false, calculatorPage.CalculateBtn.Enabled);
        }

        [Test]
        public void PositiveTestInvestmentTermIsMandatoryField()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);

            new CalculatorPage(browser)
                .EnterCalculatorData365days("100", "10", " ");

            Assert.AreEqual(false, calculatorPage.CalculateBtn.Enabled);
        }

        [Test]
        public void TestStartDateIsToday()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);

            new CalculatorPage(browser)
                .EnterCalculatorData365days("100", "10", "365");

            SelectElement daySelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'day']")));
            string day = daySelect.SelectedOption.Text;
            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'month']")));
            string month = monthSelect.SelectedOption.Text;
            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'year']")));

            string year = yearSelect.SelectedOption.Text;
            string actualDate = day + "/" + month + "/" + year;
            string expectedDate = DateTime.Today.ToString("d/MMMM/yyyy", CultureInfo.InvariantCulture);

            Assert.AreEqual(expectedDate, actualDate);
        }

        [Test]
        public void TestSelectAnyStart()
        {
            SelectElement daySelect = new (element: browser.FindElement(By.XPath("//select [@id = 'day']")));

            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'month']")));

            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'year']")));
            daySelect.SelectByText("2");
            monthSelect.SelectByText("April");
            yearSelect.SelectByText("2022");
            string actualDate = daySelect.SelectedOption.Text + " " + monthSelect.SelectedOption.Text + " " + yearSelect.SelectedOption.Text;

            Assert.AreEqual("2 April 2022", actualDate);
        }

        [Test]
        public void TestFinancialYearIsMandatoryField()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);
            bool d365 = calculatorPage.FinancialYearRadio365.Selected;
            bool d360 = calculatorPage.FinancialYearRadio360.Selected;

            //LoginPage loginPage = new LoginPage(browser);
            //loginPage.Login(" test", "newyork1");
            //public IWebElement FinancialYearRadio365 => driver.FindElement(By.XPath("//input[@type][2]"));
            Assert.IsTrue(d365 || d360); //"At least one option should be selected."
            Assert.IsFalse(d365 && d360); //"Only one option should be selected."
        }

        [Test]
        public void TestIncomeIsDisplayed()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);
          
            string Income = calculatorPage.Income.GetAttribute("value");

            Assert.AreEqual("0.00", Income);
        }

        [Test]
        public void TestInterestEarnedIsDisplayed()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);
            string InterestEarned = calculatorPage.InterestEarned.GetAttribute("value");

            Assert.AreEqual("0.00", InterestEarned);
        }

        [Test]
        public void TestEndDateDataIsCorrect()

        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);

            new CalculatorPage(browser)
                .EnterCalculatorData365days("100", "10", "7");

            new CalculatorPage(browser).Calculate();

            Thread.Sleep(1000);

            DateTime expected = DateTime.Today.AddDays(+7);

            string EndDate = calculatorPage.EndDate.GetAttribute("value");

            Assert.AreEqual(expected.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture), EndDate);
        }

        [Test]
        public void TestInterestEarnedLayout()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);
            string actual = calculatorPage.InterestEarnedLayout.GetAttribute("align");

            Assert.AreEqual("left", actual);
        }

        [Test]
        public void PositiveTestMaxDepositAmount100000()
        {
            new CalculatorPage(browser)
                .EnterCalculatorData365days("100000", "10", "365");

            new CalculatorPage(browser).Calculate();

            Thread.Sleep(1000);

            CalculatorPage calculatorPage = new CalculatorPage(browser);
            string actualIncome = calculatorPage.ActualIncome.GetAttribute("value");
            Assert.AreEqual("110 000.00", actualIncome);

            string actualInterest = calculatorPage.ActualInterest.GetAttribute("value");
            Assert.AreEqual("10 000.00", actualInterest);
        }

        [Test]
        public void PositiveTestMaxInterestRate100()
        {
            new CalculatorPage(browser)
                .EnterCalculatorData365days("100000", "100", "365");

            new CalculatorPage(browser).Calculate();

            Thread.Sleep(1000);

            CalculatorPage calculatorPage = new CalculatorPage(browser);
            string actualIncome = calculatorPage.ActualIncome.GetAttribute("value");
            Assert.AreEqual("200 000.00", actualIncome);

            string actualInterest = calculatorPage.ActualInterest.GetAttribute("value");
            Assert.AreEqual("100 000.00", actualInterest);
        }



    }
}
