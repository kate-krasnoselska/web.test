using System;
using OpenQA.Selenium;

namespace Calculator.Tests.Pages
{
    public class SettingsPage
    {
        private IWebDriver driver;

        public SettingsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement LogoutBtn => driver.FindElement(By.XPath("//button[text()='Logout']"));

        public IWebElement SaveBtn => driver.FindElement(By.XPath("//button[text()='Save']"));

        public IWebElement CancelBtn => driver.FindElement(By.XPath("//button[@id = 'cancel']"));

        public void SelectCurrency(string currencyName, string currencyCode) => driver.FindElement(By.XPath("//select[@id = 'currency']"));

        public void SelectNumberFormat(string format, string expectedIncome, string expectedInterest) => driver.FindElement(By.XPath("//select[@id = 'numberFormat']"));

        public void SelectDateFormat (string format) => driver.FindElement(By.XPath("//select[@id = 'dateFormat']"));

        public void Logout()
        {
            LogoutBtn.Click();
            driver.SwitchTo().Alert().Accept();
        }

        public void Save()
        {
            SaveBtn.Click();
            driver.SwitchTo().Alert().Accept();
        }
    }
}
