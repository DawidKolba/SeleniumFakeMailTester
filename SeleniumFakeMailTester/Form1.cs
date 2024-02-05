using SeleniumFakeMailTester.SeleniumFakeMailTester.Selenium;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SeleniumFakeMailTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            addNewAliasTB.Text = "temp";
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            var mailboxManager = new MailboxManager();
            mailboxManager.StartTest(addNewAliasTB.Text);
        }
    }
}