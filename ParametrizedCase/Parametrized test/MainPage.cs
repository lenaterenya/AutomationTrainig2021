using OpenQA.Selenium;

namespace Parametrized_test
{
    public class MainPage
    {
        private readonly IWebDriver _driver;
        private string url = "http://tut.by";

        private readonly By _openAccountButtonSelector = By.CssSelector("a[data-target-popup='authorize-form']");
        private readonly By _usernameInputSelector = By.Name("login");
        private readonly By _passwordInputSelector = By.Name("password");
        private readonly By _loginButtonSelector = By.CssSelector("input[type='submit']");
        
        public MainPage(IWebDriver driver)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(url);
        }

        public MainPage LoginAccount(string username, string password)
        {
            _driver.FindElement(_openAccountButtonSelector).Click();  
            _driver.FindElement(_usernameInputSelector).SendKeys(username);
            _driver.FindElement(_passwordInputSelector).SendKeys(password);
            _driver.FindElement(_loginButtonSelector).Click();

           return this;
        }

    }
}