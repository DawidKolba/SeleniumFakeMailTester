using System.Diagnostics;
using SeleniumFakeMailTester.Testing;

namespace SeleniumFakeMailTester
{
    public partial class MainAppForm : Form
    {
        public MainAppForm()
        {
            InitializeComponent();
            emailTB.Text = "temp";
        }

        private void WorkInProgress()
        {
            progressLB.Text = "Please wait, work in progress...";
            GetAllMailsBtn.Enabled = false;
            emailTB.Enabled = false;
        }
        
        private void Done()
        {
            progressLB.Text = "";
            GetAllMailsBtn.Enabled = true;
            emailTB.Enabled = true;
        }

        private async void getAllEmailsFromMailbox_Click(object sender, EventArgs e)
        {
            WorkInProgress();
            using (var mailboxManager = new TestManager())
            {
                await mailboxManager.StartTest(emailTB.Text);
            }
            
            Process.Start("explorer.exe", ConfigManager.outputDirectory);
            Done();
        }
    }
}