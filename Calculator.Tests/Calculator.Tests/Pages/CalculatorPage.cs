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

        public IWebElement CalculateBtn => driver.FindElement(By.XPath("//button [@id = 'calculateBtn']"));

        public void Calculate()
        {
            CalculateBtn.Click();
            Thread.Sleep(1000);
        }

        public string CalcBtnDisabled => driver.FindElement(By.XPath("//button [@id = 'calculateBtn']")).GetAttribute("value");
    }
}


