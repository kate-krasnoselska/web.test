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
    }
}
