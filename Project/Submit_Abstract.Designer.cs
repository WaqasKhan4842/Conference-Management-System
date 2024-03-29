namespace Project
{
    partial class Submit_Abstract
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
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.Title = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Link = new System.Windows.Forms.Label();
            this.Submit_Abstratc = new System.Windows.Forms.Button();
            this.closebtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(4, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 16);
            this.label4.TabIndex = 69;
            this.label4.Text = "*";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(94, 102);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(199, 20);
            this.textBox3.TabIndex = 68;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(22, 103);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(32, 17);
            this.Title.TabIndex = 67;
            this.Title.Text = "Title";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(4, 141);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 16);
            this.label9.TabIndex = 66;
            this.label9.Text = "*";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(94, 139);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(199, 20);
            this.textBox1.TabIndex = 65;
            // 
            // Link
            // 
            this.Link.AutoSize = true;
            this.Link.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Link.Location = new System.Drawing.Point(22, 140);
            this.Link.Name = "Link";
            this.Link.Size = new System.Drawing.Size(32, 17);
            this.Link.TabIndex = 64;
            this.Link.Text = "Link";
            // 
            // Submit_Abstratc
            // 
            this.Submit_Abstratc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(184)))));
            this.Submit_Abstratc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Submit_Abstratc.ForeColor = System.Drawing.Color.White;
            this.Submit_Abstratc.Location = new System.Drawing.Point(127, 197);
            this.Submit_Abstratc.Name = "Submit_Abstratc";
            this.Submit_Abstratc.Size = new System.Drawing.Size(126, 35);
            this.Submit_Abstratc.TabIndex = 72;
            this.Submit_Abstratc.Text = "Submit Abstract";
            this.Submit_Abstratc.UseVisualStyleBackColor = false;
            this.Submit_Abstratc.Click += new System.EventHandler(this.Submit_Abstratc_Click);
            // 
            // closebtn
            // 
            this.closebtn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closebtn.FlatAppearance.BorderSize = 0;
            this.closebtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closebtn.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closebtn.ForeColor = System.Drawing.Color.Red;
            this.closebtn.Location = new System.Drawing.Point(317, -2);
            this.closebtn.Name = "closebtn";
            this.closebtn.Size = new System.Drawing.Size(45, 33);
            this.closebtn.TabIndex = 73;
            this.closebtn.Text = "X";
            this.closebtn.UseVisualStyleBackColor = true;
            this.closebtn.Click += new System.EventHandler(this.closebtn_Click);
            // 
            // Submit_Abstract
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 361);
            this.Controls.Add(this.closebtn);
            this.Controls.Add(this.Submit_Abstratc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Link);
            this.Name = "Submit_Abstract";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label Link;
        private System.Windows.Forms.Button Submit_Abstratc;
        private System.Windows.Forms.Button closebtn;
    }
}