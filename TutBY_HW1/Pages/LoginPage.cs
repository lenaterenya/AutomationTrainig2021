using OpenQA.Selenium;

namespace Automation_HW1.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private string url = "http://tut.by";

        private readonly By _openMailButtonSelector = By.CssSelector("a[data-target-popup='authorize-form']");
        private readonly By _usernameInputSelector = By.Name("login");
        private readonly By _passwordInputSelector = By.Name("password");
        private readonly By _loginButtonSelector = By.CssSelector("input[type='submit']");
        private readonly By _userButtonSelector = By.CssSelector("span[class='uname']");
        private readonly By _mailButtonSelector = By.XPath("//a[contains(@href, 'https://profile.tut.by/mail.html')]");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(url);
        }

        public InboxPage LoginEmail(string username, string password)
        {
            var windowsCountBeforeClick = _driver.WindowHandles.Count;

            _driver.FindElement(_openMailButtonSelector).Click();
            _driver.FindElement(_usernameInputSelector).SendKeys(username);
            _driver.FindElement(_passwordInputSelector).SendKeys(password);
            _driver.FindElement(_loginButtonSelector).Click();

            _driver.FindElement(_userButtonSelector).Click();
            _driver.FindElement(_mailButtonSelector).Click();

            var windowsCountAfterClick = _driver.WindowHandles.Count;

            if (windowsCountBeforeClick < windowsCountAfterClick)
            {
                _driver.SwitchTo().Window(_driver.WindowHandles[windowsCountAfterClick - 1]);
            }

            return new InboxPage(_driver);
        }
    }
}
