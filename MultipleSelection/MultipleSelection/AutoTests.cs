using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MultipleSelection
{
    public class AutoTests
    {
        [TestFixture]
        public class WebDriverTests
        {
            public  IWebDriver _driver;

            private string url1 = "https://www.seleniumeasy.com/test/basic-select-dropdown-demo.html";
            private string url2 = "https://www.seleniumeasy.com/test/javascript-alert-box-demo.html";
            private string url3 = "https://www.seleniumeasy.com/test/javascript-alert-box-demo.html";
            private string url4 = "https://www.seleniumeasy.com/test/dynamic-data-loading-demo.html";
            private string url5 = "https://www.seleniumeasy.com/test/bootstrap-download-progress-demo.html";
            private string url6 = "https://www.seleniumeasy.com/test/table-sort-search-demo.html";

            private readonly By _selectId = By.Id("multi-select");
            private readonly By _alertClickMeButtonCss = By.CssSelector("button[onclick='myAlertFunction()']");
            private readonly By _confirmBoxClickMeButtonCss = By.CssSelector("button[onclick='myConfirmFunction()']");
            private readonly By _getNewUserButtonId = By.Id("save");
            private readonly By _downloadButtonId = By.Id("cricle-btn");

            [SetUp]
            public void Initialize()
            {
                _driver = new ChromeDriver();
                _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                _driver.Manage().Window.Maximize();
            }

            [Test]
            public void ChooseTheValues()
            {
                _driver.Navigate().GoToUrl(url1);
                var select = _driver.FindElement(_selectId);
                SelectElement selectElement = new SelectElement(select);

                selectElement.SelectByValue("Ohio");
                selectElement.SelectByValue("Washington");
                selectElement.SelectByValue("California");

                var selectedElements = selectElement.AllSelectedOptions;

                Assert.That(selectedElements.Count, Is.EqualTo(3), "You failed)");
            }

            [Test]
            public void AlertBoxTest()
            {
                _driver.Navigate().GoToUrl(url2);
                var expectedAlertText = "I am an alert box!";
                var clickMeButton = _driver.FindElement(_alertClickMeButtonCss);
                clickMeButton.Click();

                var alert = _driver.SwitchTo().Alert();
                var text = alert.Text;
                alert.Accept();

                Assert.That(text, Is.EqualTo(expectedAlertText));
            }

            [Test]
            public void ConfirmBoxTest1()
            {
                _driver.Navigate().GoToUrl(url3);
                var clickResultText = _driver.FindElement(By.Id("confirm-demo"));
                var expectedResult = "You pressed OK!";
                var clickmebutton = _driver.FindElement(_confirmBoxClickMeButtonCss);
                clickmebutton.Click();


                var alert = _driver.SwitchTo().Alert();
                alert.Accept();

                Assert.That(clickResultText.Text, Is.EqualTo(expectedResult));
            }

            [Test]
            public void ConfirmBoxTest2()
            {
                _driver.Navigate().GoToUrl(url3);
                var clickResultText = _driver.FindElement(By.Id("confirm-demo"));
                var expectedResult = "You pressed Cancel!";
                var clickmebutton = _driver.FindElement(_confirmBoxClickMeButtonCss);
                clickmebutton.Click();

                var alert = _driver.SwitchTo().Alert();
                alert.Dismiss();

                Assert.That(clickResultText.Text, Is.EqualTo(expectedResult));
            }

            [Test]
            public void LoadingTheDataTest()
            {
                _driver.Navigate().GoToUrl(url4);
                var getNewUserButton = _driver.FindElement(_getNewUserButtonId);
                getNewUserButton.Click();
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));

                var element = wait.Until(condition =>
                {
                    try
                    {
                        var loadingToBeDisplayed =
                            _driver.FindElement(By.CssSelector("#loading > img"));
                        return !(loadingToBeDisplayed.GetAttribute("src").Contains("loader-image"));
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

                Assert.That(element, Is.True);
            }

            [Test]
            public void ProgressBarTest()
            {
                _driver.Navigate().GoToUrl(url5);
                var downloadButton = _driver.FindElement(_downloadButtonId);
                downloadButton.Click();
                var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
                wait.PollingInterval = TimeSpan.FromMilliseconds(200);


                var element = wait.Until(condition =>
                {

                    var loadingPercentage = _driver.FindElement(By.CssSelector("div[class='percenttext']")).Text;

                    var loading = loadingPercentage.Replace("%", "");
                    var loadingParsed = int.Parse(loading);

                    return loadingParsed > 50;

                });

                if (element == true)
                {
                    _driver.Navigate().Refresh();

                }

                var loadingPercentage = _driver.FindElement(By.CssSelector("div[class='percenttext']")).Text;

                Assert.That("0%", Is.EqualTo(loadingPercentage));

            }

            [Test]
            public void TableTest()
            {
                _driver.Navigate().GoToUrl(url6);

                var table = new Table(_driver);
                table.TableShowEntriesSetup("10");
                var employees = table.FetchEmployeesByAgeAndSalary(45, 100000);
                Assert.That(employees.Count, Is.EqualTo(1));

            }
        }
    }
}