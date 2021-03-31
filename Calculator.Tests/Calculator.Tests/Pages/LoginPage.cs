using System;
using OpenQA.Selenium;

namespace Calculator.Tests.Pages
{
    public class LoginPage
    {
        private IWebDriver driver;
        public LoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public IWebElement LoginFld
        {
            get
            {
                return driver.FindElement(By.Id("login"));
            }
        }

        public IWebElement PasswordFld => driver.FindElement(By.Id("password"));

        public IWebElement LoginBtn => driver.FindElement(By.Id("loginBtn"));

        public void Login(string login, string password)
        {
            LoginFld.SendKeys(login);
            PasswordFld.SendKeys(password);
            LoginBtn.Click();
        }

        public string ErrorMessage => driver.FindElement(By.Id("errorMessage")).Text;
    }
}
        