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
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");
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
            // NEED HELP couldn't manage this Test, unfortunately

            SelectElement daySelect = new SelectElement(element:browser.FindElement(By.Id("day")));
            string day = daySelect.SelectedOption.Text;
            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.Id("month")));
            string month = monthSelect.SelectedOption.Text;
            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.Id("year")));
            string year = yearSelect.SelectedOption.Text;
            string actualDate = day + "/" + month + "/" + year;
            string expectedDate = DateTime.Today.ToString("d/MMMM/yyyy");
            Assert.AreEqual(expectedDate, actualDate);

        }

        [Test]
        public void TestSelectAnyStart()
        { 
            SelectElement daySelect = new SelectElement(element: browser.FindElement(By.Id("day")));
            
            SelectElement monthSelect = new SelectElement(element: browser.FindElement(By.Id("month")));
            
            SelectElement yearSelect = new SelectElement(element: browser.FindElement(By.Id("year")));
            // 2 April 2022

            daySelect.SelectByText("2");
            monthSelect.SelectByText("April");
            yearSelect.SelectByText("2022");

            string actualDate = daySelect.SelectedOption.Text + " " + monthSelect.SelectedOption.Text + " " + yearSelect.SelectedOption.Text;
            Assert.AreEqual("2 April 2022", actualDate);

        }
        [Test]
        public void TestFinancialYearIsMandatoryField()
        {
            
            // NEED HELP how to skip required field properly? 
            bool d365 = browser.FindElement(By.Id("d365")).Selected;
            bool d360 = browser.FindElement(By.Id("d360")).Selected;

            Assert.IsTrue(d365 || d360);
            Assert.IsFalse(d365 && d360);

            

        }

        [Test]
        // NEED HELP what is Obsolete, program asks to add it
        [Obsolete]
        public void TestIncomeIsDisplayed()
        {
            // NEED HELP maybe I don't need to write here next 2 lines? This condition is is Setup, or not?
            new WebDriverWait(browser, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("income")));
            string Income = browser.FindElement(By.Id("income")).GetAttribute("value");
            Assert.AreEqual("0.00", Income);

        }

        [Test]
        [Obsolete]
        public void TestInterestEarnedIsDisplayed()
        {
            string InterestEarned = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("0.00", InterestEarned);

        }
        [Test]
        [Obsolete]
        public void TestEndDateDataIsCorrect()
        {
            string EndDate = browser.FindElement(By.Id("endDate")).GetAttribute("value");
            // NEED HELP it works for today only, I guess. How to make it work for any day of any year?
            Assert.AreEqual("28/02/2021", EndDate);

        }

        [Test]
        [Obsolete]
        public void TestInterestEarnedLayout()
        {
            // NEED HELP how to find aalign typo to make test fail?
            string actual = browser.FindElement(By.TagName("interest earned:")).GetAttribute("aalign");
            Assert.AreEqual("align", actual);

        }
        [Test]
        public void PositiveTestMaxDepositAmount100000()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100000");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");
            Assert.AreEqual("110000.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("10000.00", actualInterest);

        }
        [Test]
        public void PositiveTestMaxInterestRate100()
        {
            browser.FindElement(By.Id("amount")).SendKeys("100000");
            browser.FindElement(By.Id("percent")).SendKeys("100");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();
            string actualIncome = browser.FindElement(By.Id("income")).GetAttribute("value");
            Assert.AreEqual("200000.00", actualIncome);
            string actualInterest = browser.FindElement(By.Id("interest")).GetAttribute("value");
            Assert.AreEqual("100000.00", actualInterest);

        }
        /* [Test]
         public void IncomeIsSumOfDepositAmountAndInterestEarned()
         {
            browser.FindElement(By.Id("amount")).SendKeys("100");
            browser.FindElement(By.Id("percent")).SendKeys("10");
            browser.FindElement(By.Id("term")).SendKeys("365");
            browser.FindElement(By.Id("d365")).Click();

            NEED HELP what is the function to get sum of two values

            string actualIncome = browser.FindElement(By.Id("amount")).GetAttribute("value") ;
            Assert.AreEqual("110.00", actualIncome);

         } */

        /*[Test]
        public void DepositAmountFieldName()
        {

            NEED HELP: how should I find Deposit  Ammount in DOM

            string DepositAmountName = browser.FindElement(By.ClassName("Deposit Ammount *")).GetAttribute("outerText");
            Assert.AreEqual("Deposit Amount", DepositAmountName);
        } */
    }
}