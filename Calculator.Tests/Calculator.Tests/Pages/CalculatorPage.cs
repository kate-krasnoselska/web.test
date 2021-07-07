using System;

using System.Globalization;

using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Calculator.Tests.Pages
{
    public class CalculatorPage
    {
        private IWebDriver driver;

        public CalculatorPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        private By CalculateBtnLocator = By.XPath("//button [@id = 'calculateBtn']");

        public IWebElement CalculateBtn => driver.FindElement(CalculateBtnLocator);

        //public IWebElement CalculateBtn => driver.FindElement(By.XPath("//button [@id = 'calculateBtn']"));

        public IWebElement SettingsBtn => driver.FindElement(By.XPath("//button[text()='Settings']"));


        public SettingsPage OpenSettingsPage()
        {
            SettingsBtn.Click();
            return new SettingsPage(driver);
        }


        public void Calculate()
        {
            CalculateBtn.Click();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(CalculateBtnLocator));

            //Thread.Sleep(1000);
        }

        public string CalcBtnDisabled => driver.FindElement(By.XPath("//button [@id = 'calculateBtn']")).GetAttribute("value");

        //{
           // new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            //.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(CalculateBtnLocator));
          //  Thread.Sleep(1000);
        //}


        private SelectElement DaySelect => new SelectElement (element: driver.FindElement(By.XPath("//select [@id = 'day']")));
        private SelectElement MonthSelect => new SelectElement(element: driver.FindElement(By.XPath("//select [@id = 'month']")));
        private SelectElement YearSelect => new SelectElement(element: driver.FindElement(By.XPath("//select [@id = 'year']")));

        public DateTime StartDate
        {
            get
            {
                int day = int.Parse(DaySelect.SelectedOption.Text);
                int month = DateTime.ParseExact(MonthSelect.SelectedOption.Text, "MMMM", CultureInfo.CurrentCulture).Month;
                int year = int.Parse(YearSelect.SelectedOption.Text);
                return new DateTime(year, month, day);
            }
            set
            {
                DaySelect.SelectByIndex(value.Day-1);
                MonthSelect.SelectByIndex(value.Month-1);
                YearSelect.SelectByText(value.Year.ToString());
            }
        }

        public IWebElement DepositAmountFld => driver.FindElement(By.XPath("//input[@id = 'amount']"));

        public IWebElement RateOfInterestFld => driver.FindElement(By.XPath("//input [@id = 'percent']"));

        public IWebElement InvestmentTermFld => driver.FindElement(By.XPath("//input [@id = 'term']"));

        public IWebElement FinancialYearRadio365 => driver.FindElement(By.XPath("//input[@type][2]"));

        public IWebElement FinancialYearRadio360 => driver.FindElement(By.XPath("//input[@type][1]"));

        public IWebElement Income => driver.FindElement(By.XPath("//input [@id = 'income']"));

        public IWebElement InterestEarned => driver.FindElement(By.XPath("//input [@id = 'interest']"));

        public IWebElement EndDate => driver.FindElement(By.XPath("//input [@id = 'endDate']"));

        public IWebElement InterestEarnedLayout => driver.FindElement(By.XPath("//th [contains(text(), 'Interest earned')]"));

        public string ActualIncome => driver.FindElement(By.XPath("//input [@id = 'income']")).GetAttribute("value");

        public IWebElement ActualInterest => driver.FindElement(By.XPath("//input [@id = 'interest']"));

        /*public void EnterCalculatorData365days(string deposit, string rate, string term)
        {
            DepositAmountFld.SendKeys(deposit);
            RateOfInterestFld.SendKeys(rate);
            InvestmentTermFld.SendKeys(term);
            FinancialYearRadio365.Click();
        }

        public void EnterCalculatorData360days(string deposit, string rate, string term)
        {
            DepositAmountFld.SendKeys(deposit);
            RateOfInterestFld.SendKeys(rate);
            InvestmentTermFld.SendKeys(term);
            FinancialYearRadio360.Click();
        }*/

        public void EnterCalculatorData(string deposit, string rate, string term, bool year365)
        {
            DepositAmountFld.SendKeys(deposit);
            RateOfInterestFld.SendKeys(rate);
            InvestmentTermFld.SendKeys(term);
            if (year365)
            {
                FinancialYearRadio365.Click();
            }
            else
            {
                FinancialYearRadio360.Click();
            }
        

        }

    }
}


