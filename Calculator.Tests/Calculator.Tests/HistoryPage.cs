using System;
using OpenQA.Selenium;

namespace Calculator.Tests.Pages
{
    public class HistoryPage
    {
        private IWebDriver driver;
        public HistoryPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement CalculatorBtn => driver.FindElement(By.XPath("//button[text()='Calculator']"));

        public IWebElement ClearBtn => driver.FindElement(By.XPath("//button[@id = 'clear']"));
        public IWebElement CalculatedAmount => driver.FindElement(By.XPath(".//td[1]"));

        //public IWebElement CalculatedData => driver.FindElement(By.XPath(".//tr[@class = 'data']"));

        //public string CalculatedDataCleared => driver.FindElement(By.ClassName("data")).GetAttribute("value");
    }
        
}
