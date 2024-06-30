using OpenQA.Selenium;
using SeleniumFakeMailTester.PageObjects;
using SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium.Helpers;
using SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium.Models;

namespace SeleniumFakeMailTester.Testing
{
    public static class TestHelperMethods
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private static List<MailItem> ?MailItems { get; set; }

        public static async Task GetMailItemsAsync(IWebDriver driver, SemaphoreSlim maxDegreeOfParallelism, IProgress<double>? progress = null)
        {
            var inboxPage = new InboxPage(driver);
            var mailElements = inboxPage.GetMailElements();
            int totalMails = mailElements.Count;
            int processedMails = 0;

            var tasks = mailElements.Select(async mailElement =>
            {
                await maxDegreeOfParallelism.WaitAsync();
                try
                {
                    var mailItem = inboxPage.CreateMailItemFromElement(mailElement);
                    Interlocked.Increment(ref processedMails);
                    progress?.Report((double)processedMails / totalMails);
                    return mailItem;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    return null;
                }
                finally
                {
                    maxDegreeOfParallelism.Release();
                }
            });

            var mailItems = await Task.WhenAll(tasks);
            MailItems = mailItems.Where(item => item != null)
                     .Select(item => item!) 
                     .ToList();

        }

        public static async Task GetAllEmailsCopyFromMailbox(IWebDriver driver, string username)
        {
            if (MailItems == null) throw new ArgumentNullException();
            
            var httpClient = new HttpClient();
            var downloadTasks = new List<Task>();

            foreach (var mail in MailItems)
            {
                downloadTasks.Add(ProcessEmail(driver, mail, httpClient, username));
            }

            await Task.WhenAll(downloadTasks);
        }

        private static async Task ProcessEmail(IWebDriver driver, MailItem mail, HttpClient httpClient, string username)
        {
            try
            {
                if (mail.Url is null)
                {
                    _logger.Error("Url is null, cannot continue");
                    return;
                }

                mail.Id = SystemHelper.ExtractNumberFromURL(mail.Url);
                _logger.Info($"Getting mail from: {mail.Sender} received: {mail.ReceivedDate} subject: {mail.Subject} id: {mail.Id}");

                var emailPage = new EmailPage(driver);
                emailPage.NavigateTo(mail.Url);
                var iframeUrl = emailPage.GetIframeSrc();

                var response = await httpClient.GetAsync(iframeUrl);
                var pageContent = await response.Content.ReadAsStringAsync();

                await SystemHelper.SaveOutput(username, pageContent, $"[{mail.Id} {mail.Subject}].html");
            }
            catch (NoSuchElementException e)
            {
                _logger.Warn($"Element not found for mail: {mail.Id}", e);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
