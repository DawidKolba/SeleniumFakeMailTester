namespace SeleniumFakeMailTester
{
    partial class MainAppForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            emailTB = new TextBox();
            GetAllMailsBtn = new Button();
            label1 = new Label();
            progressLB = new Label();
            SuspendLayout();
            // 
            // emailTB
            // 
            emailTB.Location = new Point(23, 67);
            emailTB.Name = "emailTB";
            emailTB.Size = new Size(256, 27);
            emailTB.TabIndex = 2;
            // 
            // GetAllMailsBtn
            // 
            GetAllMailsBtn.Location = new Point(356, 40);
            GetAllMailsBtn.Name = "GetAllMailsBtn";
            GetAllMailsBtn.Size = new Size(140, 54);
            GetAllMailsBtn.TabIndex = 3;
            GetAllMailsBtn.Text = "Get all emails from the mailbox";
            GetAllMailsBtn.UseVisualStyleBackColor = true;
            GetAllMailsBtn.Click += getAllEmailsFromMailbox_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 29);
            label1.Name = "label1";
            label1.Size = new Size(265, 20);
            label1.TabIndex = 4;
            label1.Text = "niepodam.pl mailbox to get all emails:";
            // 
            // progressLB
            // 
            progressLB.AutoSize = true;
            progressLB.Location = new Point(23, 97);
            progressLB.Name = "progressLB";
            progressLB.Size = new Size(0, 20);
            progressLB.TabIndex = 5;
            // 
            // MainAppForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(542, 144);
            Controls.Add(progressLB);
            Controls.Add(label1);
            Controls.Add(GetAllMailsBtn);
            Controls.Add(emailTB);
            Name = "MainAppForm";
            Text = "Main Fake Mail Tester Niepodam.pl";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox emailTB;
        private Button GetAllMailsBtn;
        private Label label1;
        private Label progressLB;
    }
}