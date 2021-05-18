using System;
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
            Thread.Sleep(200);
        }
        public IWebElement DepositAmountFld => driver.FindElement(By.XPath("//td[2]//input[@id = 'amount']"));

        public IWebElement RateOfInterestFld => driver.FindElement(By.XPath("//input [@id = 'percent']"));

        public IWebElement InvestmentTermFld => driver.FindElement(By.XPath("//input [@id = 'term']"));

        public IWebElement IFinancialYearRadio365 => driver.FindElement(By.XPath("//input[@type][2]"));

        public IWebElement IFinancialYearRadio360 => driver.FindElement(By.XPath("//input[@type][1]"));

        



        

    }
}


