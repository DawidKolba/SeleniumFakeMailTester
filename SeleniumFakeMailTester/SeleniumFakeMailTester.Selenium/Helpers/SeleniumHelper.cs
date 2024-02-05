using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using WebDriverManager;

namespace SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium.Helpers
{
    public abstract class SeleniumHelper
    {
        protected IWebDriver driver;
        public SeleniumHelper() 
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            driver = new ChromeDriver();
        }    
    }
}
