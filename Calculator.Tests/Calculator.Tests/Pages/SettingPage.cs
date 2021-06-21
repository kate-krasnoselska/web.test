using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

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

        public IWebElement DepositPageCurrencyFld => driver.FindElement(By.XPath("//td[@id = 'currency']"));

        public void SelectCurrency(string currencyName)
        {
            SelectElement currencyFormatSelect = new SelectElement(element: driver.FindElement(By.XPath("//select[@id = 'currency']")));
            currencyFormatSelect.SelectByText(currencyName);
        }

        public void SelectNumberFormat(string format)
        {
            SelectElement numberFormatSelect = new SelectElement(element: driver.FindElement(By.XPath("//select[@id = 'numberFormat']")));
            numberFormatSelect.SelectByText(format);
        }

        public void SelectDateFormat(string format)
        {
            SelectElement dateFormatSelect = new SelectElement(element: driver.FindElement(By.XPath("//select[@id = 'dateFormat']")));
            dateFormatSelect.SelectByText(format);
        }

        public void Logout()
        {
            LogoutBtn.Click();
            driver.SwitchTo().Alert().Accept();
        }

        public void Save()
        {
            SaveBtn.Click();
            driver.SwitchTo().Alert().Accept();
            Thread.Sleep(1000);
        }

        public void SetSettingsData(string dateFormat, string numberFormat, string currency)
        {
            SelectDateFormat(dateFormat);
            SelectNumberFormat(numberFormat);
            SelectCurrency(currency);
            Save();
        }
    }

}