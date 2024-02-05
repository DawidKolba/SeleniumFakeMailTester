
namespace SeleniumFakeMailTester.SeleniumFakeMailTester.Core.Models
{
    public class MailItem
    {
        public Guid? Id { get; set; }
        public string Sender { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime ReceivedDate { get; set; }

        public MailItem() { }

        public MailItem(Guid id, string sender, string subject, string body, DateTime receivedDate)
        {
            Id = id;
            Sender = sender;
            Subject = subject;
            Body = body;
            ReceivedDate = receivedDate;
        }
    }
}
