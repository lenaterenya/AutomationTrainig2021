using System;
using System.Threading;
using Automation_HW1.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automation_HW1
{
    class AutoTests
    {
        [TestFixture]
        public class WebDriverTests
        {
            private IWebDriver _driver;
            const string username = "automationtraining2021@tut.by";
            const string password = "20automationtraini";
            //private By _writeMailButtonSelector = By.XPath("//span[@class='mail-ComposeButton-Text']");

            private By actualUserNameSelector = 
                By.XPath("//div[@class='legouser legouser_fetch-accounts_yes legouser_hidden_yes i-bem']/a/span[@class='user-account__name']");

            [SetUp]
            public void Initialize()
            {
                _driver = new ChromeDriver();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                _driver.Manage().Window.Maximize();
            }

            [Test]
            public void LoginEmailTest()
            {
                var loginPage = new LoginPage(_driver);

                loginPage
                    .LoginEmail(username, password);

                //var writeMailButton = _driver.FindElement(_writeMailButtonSelector);
                Thread.Sleep(5000);
                var actualUserName = _driver.FindElement(actualUserNameSelector).Text;
               

                Assert.That(actualUserName, Is.EqualTo(username), "You are not logged in");
            }

            [TearDown]
            public void FinishTest()
            {
                var inboxPage = new InboxPage(_driver);
                inboxPage.Logout();
                _driver.Close();
            }
        }
    }
}
