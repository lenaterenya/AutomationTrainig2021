using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Screenshots
{
    public class ScreenshotMaker
    {
        IWebDriver _driver;


        public ScreenshotMaker(IWebDriver driver)
        {
            _driver = driver;
        }
        
        public void TakeScreenshot(IWebDriver driver)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string timeStamp = DateTime.Now.ToString("fff");
            ss.SaveAsFile(Environment.CurrentDirectory + timeStamp + ".png",
                ScreenshotImageFormat.Png);
        }
    }
}
