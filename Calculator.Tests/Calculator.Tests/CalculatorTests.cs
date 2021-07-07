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

        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();
        }

        [Test]
        public void PositiveTestCalculator365days()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);


            calculatorPage
                .EnterCalculatorData("100", "10", "365", true);


            calculatorPage
                .Calculate();

            string actualIncome = calculatorPage.ActualIncome;
            Assert.AreEqual("110.00", actualIncome);


            
        }

        [Test]
        public void PositiveTestCalculator360days()
        {

            CalculatorPage calculatorPage = new CalculatorPage(browser);
            calculatorPage
                .EnterCalculatorData("100", "10", "360", false);

            calculatorPage
                .Calculate();

            string actualIncome = calculatorPage.ActualIncome;

            Assert.AreEqual("110.00", actualIncome);
        }

        [Test]
        public void PositiveTestDepositAmountIsMandatoryField()
        {

            CalculatorPage calculatorPage = new CalculatorPage(browser);

            new CalculatorPage(browser)
                .EnterCalculatorData(" ", "10", "365", true);

            Assert.IsFalse(calculatorPage.CalculateBtn.Enabled);

        }

        [Test]
        public void PositiveTestRateOfInterestIsMandatoryField()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);

      
            new CalculatorPage(browser)
                .EnterCalculatorData ("100", " ", "365", true);

            Assert.IsFalse(calculatorPage.CalculateBtn.Enabled);

        }

        [Test]
        public void PositiveTestInvestmentTermIsMandatoryField()
        {

            CalculatorPage calculatorPage = new CalculatorPage(browser);

            calculatorPage.EnterCalculatorData("100", "10", " ", true);

            Assert.IsFalse(calculatorPage.CalculateBtn.Enabled);

        }

        [Test]
        public void TestStartDateIsToday()
        {

            browser.FindElement(By.XPath("//td[2]//input[@id = 'amount']")).SendKeys("100");
            browser.FindElement(By.XPath("//input [@id = 'percent']")).SendKeys("10");
            browser.FindElement(By.XPath("//input [@id = 'term']")).SendKeys("365");

            SelectElement daySelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'day']")));
            string day = daySelect.SelectedOption.Text;
            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'month']")));
            string month = monthSelect.SelectedOption.Text;
            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'year']")));

            string year = yearSelect.SelectedOption.Text;
            string actualDate = day + "/" + month + "/" + year;
            string expectedDate = DateTime.Today.ToString("d/MMMM/yyyy", CultureInfo.InvariantCulture);

            Assert.AreEqual(expectedDate, actualDate);


            Assert.AreEqual(DateTime.Today, new CalculatorPage(browser).StartDate);

        }

        [Test]
        public void TestSelectAnyStart()
        {
            CalculatorPage calculatorPage = new CalculatorPage(browser);
            calculatorPage.StartDate = new DateTime(2022, 4, 2);

            string actualDate = calculatorPage.StartDate.ToString("d MMMM yyyy", CultureInfo.InvariantCulture);

            Assert.AreEqual("2 April 2022", actualDate);
            /*SelectElement daySelect = new (element: browser.FindElement(By.XPath("//select [@id = 'day']")));

           SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'month']")));

           SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.XPath("//select [@id = 'year']")));
           daySelect.SelectByText("2");
           monthSelect.SelectByText("April");
           yearSelect.SelectByText("2022");
           string actualDate = daySelect.SelectedOption.Text + " " + monthSelect.SelectedOption.Text + " " + yearSelect.SelectedOption.Text;*/
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
                .EnterCalculatorData ("100", "10", "7", true);

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
                .EnterCalculatorData ("100000", "10", "365", true);

            new CalculatorPage(browser).Calculate();

            Thread.Sleep(1000);

            CalculatorPage calculatorPage = new CalculatorPage(browser);
            string actualIncome = calculatorPage.ActualIncome;
            Assert.AreEqual("110 000.00", actualIncome);

            string actualInterest = calculatorPage.ActualInterest.GetAttribute("value");
            Assert.AreEqual("10 000.00", actualInterest);
        }

        [Test]
        public void PositiveTestMaxInterestRate100()
        {

            new CalculatorPage(browser)
                .EnterCalculatorData ("100000", "100", "365", true);

            new CalculatorPage(browser).Calculate();


            Thread.Sleep(1000);

            string actualInterest = calculatorPage.ActualInterest.GetAttribute("value");
            Assert.AreEqual("100 000.00", actualInterest);
        }



    }
}
