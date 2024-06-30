using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SeleniumFakeMailTester.PageObjects
{
    public class EmailPage : PageObject
    {
        private static readonly By MessageHeaderSelector = By.XPath("//h2[text()='Treść wiadomości: ']");
        private static readonly By IFrameSelector = By.XPath("//iframe[@id='message-container']");

        public EmailPage(IWebDriver driver) : base(driver)
        {
           
        }

        public void NavigateTo(string mailUrl)
        {
            driver.Navigate().GoToUrl(mailUrl);
            wait.Until(ExpectedConditions.ElementIsVisible(MessageHeaderSelector));
        }

        public string GetIframeSrc()
        {
            var iframeElement = wait.Until(ExpectedConditions.ElementIsVisible(IFrameSelector));
            return iframeElement.GetAttribute("src");
        }
    }

}
