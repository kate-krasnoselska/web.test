using System;
using Calculator.Tests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Calculator.Tests
{
    public class LoginPageTests
    {
        IWebDriver browser;

        [SetUp]
        public void BeforeEachTest()
        {
            browser = new ChromeDriver();
            browser.Url = "http://127.0.0.1:8080";
            browser.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(60);
            browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void AfterEachTest()
        {
            browser.Quit();
        }


        [Test]
        public void PositiveTest()
        {
            new LoginPage(browser).Login();
            
            //browser.FindElement(By.Id("login")).SendKeys("test");
            //browser.FindElement(By.Id("password")).SendKeys("newyork1");
            //browser.FindElement(By.Id("loginBtn")).Click();

            Assert.AreEqual("http://127.0.0.1:8080/Deposit", browser.Url);
        }

        [Test]
        public void IncorrectLoginTest()
        {
            LoginPage loginPage = new LoginPage(browser);
            loginPage.Login("test1", "newy ork1");

            Assert.AreEqual("'test1' user doesn't exist!", loginPage.ErrorMessage);
        }

        [Test]
        public void IncorrectPasswordTest()
        {
            LoginPage loginPage = new LoginPage(browser);
            loginPage.Login("test", "newyork11");

            Assert.AreEqual("Incorrect password!", loginPage.ErrorMessage);
        }

        [Test]
        public void IncorrectLoginPasswordTest()
        {

            LoginPage loginPage = new LoginPage(browser);
            loginPage.Login("test1", "newyork11");

            Assert.AreEqual("'test1' user doesn't exist!", loginPage.ErrorMessage);
        }

        [Test]
        public void EmptyLoginPasswordTest()
        {

            LoginPage loginPage = new LoginPage(browser);
            loginPage.Login(" ", " ");

            Assert.AreEqual("User name and password cannot be empty!", loginPage.ErrorMessage);
        }

        [Test]
        public void EmptyLoginTest()
        {

            LoginPage loginPage = new LoginPage(browser);
            loginPage.Login(" ", "newyork1");

            Assert.AreEqual("User name and password cannot be empty!", loginPage.ErrorMessage);
        }

        [Test]
        public void EmptyPasswordTest()
        {

            LoginPage loginPage = new LoginPage(browser);
            loginPage.Login(" ", "newyork1");

            Assert.AreEqual("User name and password cannot be empty!", loginPage.ErrorMessage);
        }

        [Test]
        public void UpperCaseLoginTest()
        {

            LoginPage loginPage = new LoginPage(browser);
            loginPage.Login("TEST", "newyork1");

            Assert.AreEqual("Incorrect user name!", loginPage.ErrorMessage);
        }

        [Test]
        public void UpperCasePasswordTest()
        {

            LoginPage loginPage = new LoginPage(browser);
            loginPage.Login("test", "NEWYORK1");

            Assert.AreEqual("Incorrect password!", loginPage.ErrorMessage);
        }

        [Test]
        public void SpaceInLoginTest()
        {

            LoginPage loginPage = new LoginPage(browser);
            loginPage.Login(" test", "newyork1");

            Assert.AreEqual("Incorrect user name!", loginPage.ErrorMessage);
        }

        [Test]
        public void RemindPassBtnIsDisplayedTest()
        {
            new WebDriverWait(browser, TimeSpan.FromSeconds(10))
            .Until(SeleniumExtras.WaitHelpers.ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//button[text() = 'Remind password']")));
            IWebElement btn = browser.FindElement(By.XPath("//button[text() = 'Remind password']"));
            string actual = btn.Text;

            Assert.AreEqual("Remind password", actual);
        }

        [Test]
        public void LoginFieldName()
        {

            LoginPage loginPage = new LoginPage(browser);
            loginPage.LoginFld.GetAttribute("innerText");

            Assert.AreEqual("User", loginPage.LoginName);
        }

        [Test]
        public void PasswordFieldName()
        {

            LoginPage loginPage = new LoginPage(browser);
            loginPage.PasswordFld.GetAttribute("innerText");

            Assert.AreEqual("Password", loginPage.PasswordName);
        }


    }
    

    }
