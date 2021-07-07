using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Calculator.Tests
{
    public static class WebDriver
    {
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                if (_driver == null)
                {
                    _driver = new ChromeDriver();

                    _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                    _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                    _driver.Url = "http://127.0.0.1:8080/"; 
                }

                return _driver;
            }
        }

        public static void Quit()
        {
            _driver.Quit();
            _driver.Dispose();
            _driver = null;
        }
    }
}
