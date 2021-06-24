using System;
using Calculator.Tests.Pages;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace Calculator.Tests.Features
{
    [Binding]
    public class LoginPageSteps
    {
        [Given("I have entered '(.*)' into User field")]
        public void EnterLogin(string login)
        {
            new LoginPage(WebDriver.Driver).LoginFld.SendKeys(login);
        }

        [Given("I have entered '(.*)' into Password field")]
        public void EnterPassword(string password)
        {
            new LoginPage(WebDriver.Driver).PasswordFld.SendKeys(password);
        }

        [When("I click Login button")]
        public void ClickLgnBtn() => new LoginPage(WebDriver.Driver).LoginBtn.Click();

        [Then("I am redirected to Calculator page")]
        public void RedirectToCalcPage()
        {
            Assert.AreEqual("http://127.0.0.1:8080/Deposit", WebDriver.Driver.Url);
        }

        [Then("I see error message '(.*)'")]
        public void VerifyErrormessage(string message)
        {
            Assert.AreEqual(message, new LoginPage(WebDriver.Driver).ErrorMessage);
        }

        [AfterScenario]
        public void TearDown()
        {
            WebDriver.Quit();
        }
    }
}

