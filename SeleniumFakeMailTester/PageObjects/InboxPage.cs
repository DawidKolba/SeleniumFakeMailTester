using OpenQA.Selenium;
using SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium.Models;

namespace SeleniumFakeMailTester.PageObjects
{
    public class InboxPage : PageObject
    {
        private static readonly By MailElementSelector = By.XPath("//div[contains(@class,'mail')]");
        private static readonly By TitleSelector = By.XPath(".//div[@class='mail_tytul']//a");
        private static readonly By SenderSelector = By.XPath(".//div[@class='mail_from']");
        private static readonly By DateSelector = By.XPath(".//div[@class='mail_data']");
        private static readonly By UrlSelector = By.XPath(".//a[contains(@href, '/wiadomosci/')]");

        public InboxPage(IWebDriver driver) : base(driver)
        {
         
        }

        public List<IWebElement> GetMailElements()
        {
            return wait.Until(driver => driver.FindElements(MailElementSelector)).ToList();
        }

        public MailItem CreateMailItemFromElement(IWebElement mailElement)
        {
            var subjectElement = mailElement.FindElement(TitleSelector);
            var fromElement = mailElement.FindElement(SenderSelector);
            var receivedTimestampElement = mailElement.FindElement(DateSelector);
            var urlElement = mailElement.FindElement(UrlSelector);


            var received = new DateTime();
            if (!DateTime.TryParse(receivedTimestampElement.Text, out received)) 

            received = DateTime.Now;

            return new MailItem
            {
                Subject = subjectElement.Text,
                Sender = fromElement.Text,
                ReceivedDate = received,
                Url = urlElement.GetAttribute("href")
            };
        }
    }
}
