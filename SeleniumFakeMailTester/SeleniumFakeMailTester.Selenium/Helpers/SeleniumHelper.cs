using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;

namespace SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium.Helpers
{
    public abstract class SeleniumHelper : IDisposable
    {
        protected IWebDriver driver;
        public SeleniumHelper()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                    driver = null;
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
