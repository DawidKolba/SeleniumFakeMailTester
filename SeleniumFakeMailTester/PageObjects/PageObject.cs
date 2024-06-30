using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace SeleniumFakeMailTester.PageObjects
{
    public abstract class PageObject
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        protected PageObject(IWebDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

    }
}
