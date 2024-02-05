using OpenQA.Selenium;
using SeleniumFakeMailTester.SeleniumFakeMailTester.Core.Models;
using SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium.Helpers;

namespace SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium
{
    public class MailboxManager : SeleniumHelper
    {
        public List<MailItem> MailItems { get; set; } = new List<MailItem>();

        public MailboxManager() : base()
        {
        }

        public void StartTest(string userName)
        {
            OpenUserMailbox(userName);
            GetMailItems();
        }

        private void OpenUserMailbox(string userName)
        {
            var url = $"https://niepodam.pl/users/{userName}";
            driver.Navigate().GoToUrl(url);
        }

        private void GetMailItems()
        {
            var mailElements = driver.FindElements(By.XPath("//div[contains(@class,'mail')]"));

            var mailItems = new List<MailItem>();

            foreach (var mailElement in mailElements)
            {            
                var subjectElement = mailElement.FindElement(By.XPath(".//div[@class='mail_tytul']//a"));
                var fromElement = mailElement.FindElement(By.XPath(".//div[@class='mail_from']"));
                var receivedTimestampElement = mailElement.FindElement(By.XPath(".//div[@class='mail_data']"));

                var mailItem = new MailItem
                {
                    Subject = subjectElement.Text,
                    Sender = fromElement.Text,
                    ReceivedDate = DateTime.Parse(receivedTimestampElement.Text)
                };

                mailItems.Add(mailItem);
            }

            MailItems = mailItems;
        }
    }
}
