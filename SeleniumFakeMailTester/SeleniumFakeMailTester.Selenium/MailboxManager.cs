using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumFakeMailTester.SeleniumFakeMailTester.Core.Models;
using SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium.Helpers;
using System.Runtime.InteropServices;

namespace SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium
{
    public class MailboxManager : SeleniumHelper, IDisposable
    {
        public List<MailItem> MailItems { get; set; } = new List<MailItem>();
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private string _username;
        private static System.ComponentModel.IContainer _components = null;
        private static SafeHandle _resource;
        private SemaphoreSlim maxDegreeOfParallelism;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {     
                if (_resource != null)
                {
                    _resource.Dispose();
                    _resource = null;
                }
                if (_components != null)
                {
                    _components.Dispose();
                    _components = null;
                }
            }
            base.Dispose(disposing);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public MailboxManager() : base()
        {
        }

        public async Task StartTest(string userName)
        {
            _logger.Info($"Start getting all emails for user: {userName}");
            maxDegreeOfParallelism = new SemaphoreSlim(ConfigManager.MaxParalel);
            OpenUserMailbox(userName);
            _logger.Info("Mailbox opened");
          //  await GetMailItemsAsync();
            IProgress<double> progressIndicator = new Progress<double>(percentComplete =>
            {
                Console.WriteLine($"Progress: {percentComplete:P2}");
            });
            await GetMailItemsAsync(progressIndicator);

            _logger.Info($"Found {MailItems.Count} emails on the account, start copying");
            await GetAllEmailsCopyFromMailbox();
            _logger.Info("Finished");
        }

        private async Task GetAllEmailsCopyFromMailbox()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            var httpClient = new HttpClient();
            var downloadTasks = new List<Task>();

            foreach (var mail in MailItems)
            {
                downloadTasks.Add(ProcessEmail(mail, wait, httpClient));
            }

            await Task.WhenAll(downloadTasks);
        }

        private async Task ProcessEmail(MailItem mail, WebDriverWait wait, HttpClient httpClient)
        {
            try
            {
                mail.Id = SystemHelper.ExtractNumberFromURL(mail.Url);
                _logger.Info($"Getting mail from: {mail.Sender} received: {mail.ReceivedDate} subject: {mail.Subject} id: {mail.Id}");

                driver.Navigate().GoToUrl(mail.Url);
                wait.Until(driver => driver.FindElement(By.XPath("//h2[text()='Treść wiadomości: ']")));
                var iframeElement = driver.FindElement(By.XPath("//iframe[@id='message-container']"));
                var iframeSrc = iframeElement.GetAttribute("src");
                var iframeUrl = $"{iframeSrc}";

                var response = await httpClient.GetAsync(iframeUrl);
                var pageContent = await response.Content.ReadAsStringAsync();

                await SystemHelper.SaveOutput(_username, pageContent, $"[{mail.Id} {mail.Subject}].html");
            }
            catch (NoSuchElementException e)
            {
                _logger.Warn($"Element not found for mail: {mail.Id}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void OpenUserMailbox(string userName)
        {
            var url = $"https://niepodam.pl/users/{userName}";
            driver.Navigate().GoToUrl(url);
            _username = userName;
        }


        public async Task GetMailItemsAsync(IProgress<double> progress = null)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
            var mailElements = wait.Until(driver => driver.FindElements(By.XPath("//div[contains(@class,'mail')]")));
            _logger.Info($"Found {mailElements.Count} e-mails");
            int totalMails = mailElements.Count;
            int processedMails = 0;

            var tasks = mailElements.Select(async mailElement =>
            {
                await maxDegreeOfParallelism.WaitAsync();
                try
                {
                    var subjectElement = mailElement.FindElement(By.XPath(".//div[@class='mail_tytul']//a"));
                    var fromElement = mailElement.FindElement(By.XPath(".//div[@class='mail_from']"));
                    var receivedTimestampElement = mailElement.FindElement(By.XPath(".//div[@class='mail_data']"));
                    var urlElement = mailElement.FindElement(By.XPath(".//a[contains(@href, '/wiadomosci/')]"));
                    var url = urlElement.GetAttribute("href");

                    var mailItem = new MailItem
                    {
                        Subject = subjectElement.Text,
                        Sender = fromElement.Text,
                        ReceivedDate = DateTime.Parse(receivedTimestampElement.Text),
                        Url = url
                    };

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
            MailItems = mailItems.Where(item => item != null).ToList();
        }
    }
}
