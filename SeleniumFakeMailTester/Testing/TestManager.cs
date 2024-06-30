using SeleniumFakeMailTester.Configuration;
using SeleniumFakeMailTester.PageObjects;
using SeleniumFakeMailTester.SeleniumFakeMailTester.Core;
using SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium.Models;

namespace SeleniumFakeMailTester.Testing
{
    public class TestManager : SeleniumBase, IDisposable
    {
        private InboxPage inboxPage;
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private SemaphoreSlim maxDegreeOfParallelism;
        private List<MailItem> MailItems { get; set; }

        public TestManager() : base()
        {
            inboxPage = new InboxPage(driver);
            MailItems = new List<MailItem>();
            maxDegreeOfParallelism = new SemaphoreSlim(ConfigManager.MaxParallel);
        }

        public async Task StartTest(string userName)
        {
            _logger.Info($"Start getting all emails for user: {userName}");
            var UserMailboxURL = UrlAddresses.GetUserMailboxUrl(userName);
            NavigateToURL(UserMailboxURL);
            _logger.Info("Mailbox opened");

            await TestHelperMethods.GetMailItemsAsync(driver, maxDegreeOfParallelism);
            _logger.Info($"Found {MailItems.Count} emails on the account, start copying");
            await TestHelperMethods.GetAllEmailsCopyFromMailbox(driver, userName);
            _logger.Info("Finished");
        }
    }
}