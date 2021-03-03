using OpenQA.Selenium;

namespace Automation_HW1.Pages
{
    public class InboxPage
    {
        private IWebDriver _driver;
        private readonly By _accountButtonSelector = By.CssSelector("div[class='legouser legouser_fetch-accounts_yes legouser_hidden_yes i-bem']");
        private readonly By _logoutButtonSelector = By.XPath("//a[contains(@data-count, 'exit')]");
        private By _actualUserNameSelector = By.XPath("//div[@class='legouser__menu-header']/div/span");

        public InboxPage (IWebDriver driver)
        {
            _driver = driver;
        }

        public void Logout()
        {
            _driver.FindElement(_accountButtonSelector).Click();
            _driver.FindElement(_logoutButtonSelector).Click();
        }
    }
}
