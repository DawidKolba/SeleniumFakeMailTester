namespace SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium.Models
{
    public class MailItem
    {
        public int? Id { get; set; }
        public string? Sender { get; set; }
        public string? Subject { get; set; }
        public string? Url { get; set; }
        public DateTime ReceivedDate { get; set; }

        public MailItem() { }

        public MailItem(int id, string sender, string subject, DateTime receivedDate, string url)
        {
            Id = id;
            Sender = sender;
            Subject = subject;
            ReceivedDate = receivedDate;
            Url = url;
        }
    }
}
