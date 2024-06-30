using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;
using OpenQA.Selenium.Support.UI;

namespace SeleniumFakeMailTester.SeleniumFakeMailTester.Core
{
    public abstract class SeleniumBase : IDisposable
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public SeleniumBase()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected void NavigateToURL(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
