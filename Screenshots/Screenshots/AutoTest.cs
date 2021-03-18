using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace Screenshots
{
    public class AutoTests
    {
        [TestFixture]
        public class WebDriverTests
        {
            private IWebDriver _driver;
            const string username = "automationtraining2021@tut.by";
            const string password = "20automationtraini";
            private readonly By _actualUsernameButtonSelector = By.CssSelector("span[class='uname']");
      
            

            [SetUp]
            public void Initialize()
            {
                _driver = new ChromeDriver();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                _driver.Manage().Window.Maximize();
            }

            [TestCase("seleniumtests@tut.by", "123456789zxcvbn")]
            [TestCase("seleniumtests2@tut.by", "123456789zxcvbn")]
            public void LoginAccount(string name, string pass)
            {
                var mainPage = new MainPage(_driver);

                mainPage
                    .LoginAccount(name, pass);

                var screenshotMaker = new ScreenshotMaker(_driver);
                screenshotMaker.TakeScreenshot(_driver);

                Thread.Sleep(5000);//Thread.Sleep is a static method which suspends the thread which called it for the number of milliseconds specified in the parameter.

                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(12));
                wait.PollingInterval = TimeSpan.FromMilliseconds(200);


                var element = wait.Until(condition =>
                {
                    try
                    {
                        var nameToBeDisplayed = _driver.FindElement(By.CssSelector("span[class='uname']"));
                        return nameToBeDisplayed.Displayed;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }

                });
                Assert.That(element, Is.True, "You are not logged in");
            }

            [TearDown]
            public void FinishTest()
            {
                _driver.Close();
            }
        }
    }
}