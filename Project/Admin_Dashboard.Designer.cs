namespace Project
{
    partial class Admin_Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Admin_Dashboard));
            this.Manage_Reviewer = new System.Windows.Forms.Button();
            this.View_Conference = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.closebtn = new System.Windows.Forms.Button();
            this.pictureBox15 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DesktopPage = new System.Windows.Forms.Panel();
            this.Logout = new System.Windows.Forms.Button();
            this.Statistics = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.DesktopPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // Manage_Reviewer
            // 
            this.Manage_Reviewer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Manage_Reviewer.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Manage_Reviewer.Location = new System.Drawing.Point(-13, 311);
            this.Manage_Reviewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Manage_Reviewer.Name = "Manage_Reviewer";
            this.Manage_Reviewer.Size = new System.Drawing.Size(266, 53);
            this.Manage_Reviewer.TabIndex = 0;
            this.Manage_Reviewer.Text = "ManageReviewer";
            this.Manage_Reviewer.UseVisualStyleBackColor = false;
            this.Manage_Reviewer.Click += new System.EventHandler(this.Manage_Reviewer_Click);
            // 
            // View_Conference
            // 
            this.View_Conference.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.View_Conference.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.View_Conference.Location = new System.Drawing.Point(0, 215);
            this.View_Conference.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.View_Conference.Name = "View_Conference";
            this.View_Conference.Size = new System.Drawing.Size(253, 52);
            this.View_Conference.TabIndex = 1;
            this.View_Conference.Text = "View Conferernce";
            this.View_Conference.UseVisualStyleBackColor = false;
            this.View_Conference.Click += new System.EventHandler(this.View_Conference_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panel1.Controls.Add(this.Statistics);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.closebtn);
            this.panel1.Controls.Add(this.pictureBox15);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.Manage_Reviewer);
            this.panel1.Controls.Add(this.View_Conference);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 940);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(84, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 23);
            this.label1.TabIndex = 63;
            this.label1.Text = "Logout";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // closebtn
            // 
            this.closebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.Red;
            this.closebtn.Location = new System.Drawing.Point(0, -2);
            this.closebtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(36, 51);
            this.closebtn.TabIndex = 62;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // pictureBox15
            // 
            this.pictureBox15.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox15.Image")));
            this.pictureBox15.Location = new System.Drawing.Point(45, 0);
            this.pictureBox15.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox15.Name = "pictureBox15";
            this.pictureBox15.Size = new System.Drawing.Size(134, 46);
            this.pictureBox15.TabIndex = 59;
            this.pictureBox15.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(0, 62);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(244, 82);
            this.panel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(84, 26);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Dashboard";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(45, 26);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 31);
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(84, 18);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 22);
            this.label2.TabIndex = 3;
            this.label2.Text = "Dashboard";
            // 
            // DesktopPage
            // 
            this.DesktopPage.Controls.Add(this.Logout);
            this.DesktopPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DesktopPage.Location = new System.Drawing.Point(244, 0);
            this.DesktopPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DesktopPage.Name = "DesktopPage";
            this.DesktopPage.Size = new System.Drawing.Size(1232, 940);
            this.DesktopPage.TabIndex = 4;
            // 
            // Logout
            // 
            this.Logout.BackColor = System.Drawing.Color.Teal;
            this.Logout.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Logout.ForeColor = System.Drawing.Color.White;
            this.Logout.Location = new System.Drawing.Point(1102, 0);
            this.Logout.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Logout.Name = "Logout";
            this.Logout.Size = new System.Drawing.Size(124, 51);
            this.Logout.TabIndex = 58;
            this.Logout.Text = "Logout";
            this.Logout.UseVisualStyleBackColor = false;
            this.Logout.Click += new System.EventHandler(this.Logout_Click);
            // 
            // Statistics
            // 
            this.Statistics.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Statistics.Font = new System.Drawing.Font("Century Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Statistics.Location = new System.Drawing.Point(-13, 412);
            this.Statistics.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Statistics.Name = "Statistics";
            this.Statistics.Size = new System.Drawing.Size(266, 53);
            this.Statistics.TabIndex = 64;
            this.Statistics.Text = "Statistics";
            this.Statistics.UseVisualStyleBackColor = false;
            this.Statistics.Click += new System.EventHandler(this.Statistics_Click);
            // 
            // Admin_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1476, 940);
            this.Controls.Add(this.DesktopPage);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Admin_Dashboard";
            this.Text = "View_Conference_Admin";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox15)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.DesktopPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Manage_Reviewer;
        private System.Windows.Forms.Button View_Conference;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel DesktopPage;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Logout;
        private System.Windows.Forms.Button closebtn;
        private System.Windows.Forms.PictureBox pictureBox15;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Statistics;
    }
}