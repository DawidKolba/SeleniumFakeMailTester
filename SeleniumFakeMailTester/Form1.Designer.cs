namespace SeleniumFakeMailTester
{
    partial class Form1
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
            mailListLB = new ListView();
            addBtn = new Button();
            addNewAliasTB = new TextBox();
            startBtn = new Button();
            SuspendLayout();
            // 
            // mailListLB
            // 
            mailListLB.Location = new Point(84, 171);
            mailListLB.Name = "mailListLB";
            mailListLB.Size = new Size(199, 112);
            mailListLB.TabIndex = 0;
            mailListLB.UseCompatibleStateImageBehavior = false;
            // 
            // addBtn
            // 
            addBtn.Location = new Point(258, 56);
            addBtn.Name = "addBtn";
            addBtn.Size = new Size(129, 48);
            addBtn.TabIndex = 1;
            addBtn.Text = "add";
            addBtn.UseVisualStyleBackColor = true;
            // 
            // addNewAliasTB
            // 
            addNewAliasTB.Location = new Point(84, 67);
            addNewAliasTB.Name = "addNewAliasTB";
            addNewAliasTB.Size = new Size(113, 27);
            addNewAliasTB.TabIndex = 2;
            // 
            // startBtn
            // 
            startBtn.Location = new Point(576, 192);
            startBtn.Name = "startBtn";
            startBtn.Size = new Size(121, 45);
            startBtn.TabIndex = 3;
            startBtn.Text = "Start";
            startBtn.UseVisualStyleBackColor = true;
            startBtn.Click += startBtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(startBtn);
            Controls.Add(addNewAliasTB);
            Controls.Add(addBtn);
            Controls.Add(mailListLB);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView mailListLB;
        private Button addBtn;
        private TextBox addNewAliasTB;
        private Button startBtn;
    }
}