namespace ApmDbBackupManager
{
    partial class Ftp
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFtpPass = new System.Windows.Forms.TextBox();
            this.txtFtpUserName = new System.Windows.Forms.TextBox();
            this.txtFtpLoc = new System.Windows.Forms.TextBox();
            this.btnFtpThings = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(221, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 20);
            this.label6.TabIndex = 31;
            this.label6.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(221, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 20);
            this.label5.TabIndex = 30;
            this.label5.Text = "Username";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(221, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 20);
            this.label4.TabIndex = 29;
            this.label4.Text = "Ftp Location";
            // 
            // txtFtpPass
            // 
            this.txtFtpPass.Location = new System.Drawing.Point(190, 154);
            this.txtFtpPass.Name = "txtFtpPass";
            this.txtFtpPass.Size = new System.Drawing.Size(166, 27);
            this.txtFtpPass.TabIndex = 28;
            // 
            // txtFtpUserName
            // 
            this.txtFtpUserName.Location = new System.Drawing.Point(190, 103);
            this.txtFtpUserName.Name = "txtFtpUserName";
            this.txtFtpUserName.Size = new System.Drawing.Size(166, 27);
            this.txtFtpUserName.TabIndex = 27;
            // 
            // txtFtpLoc
            // 
            this.txtFtpLoc.Location = new System.Drawing.Point(190, 50);
            this.txtFtpLoc.Name = "txtFtpLoc";
            this.txtFtpLoc.Size = new System.Drawing.Size(166, 27);
            this.txtFtpLoc.TabIndex = 26;
            // 
            // btnFtpThings
            // 
            this.btnFtpThings.Location = new System.Drawing.Point(190, 187);
            this.btnFtpThings.Name = "btnFtpThings";
            this.btnFtpThings.Size = new System.Drawing.Size(166, 29);
            this.btnFtpThings.TabIndex = 25;
            this.btnFtpThings.Text = "Ftp Kaydet";
            this.btnFtpThings.UseVisualStyleBackColor = true;
            this.btnFtpThings.Click += new System.EventHandler(this.btnFtpThings_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(52, 250);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(618, 188);
            this.dataGridView1.TabIndex = 32;
            // 
            // Ftp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFtpPass);
            this.Controls.Add(this.txtFtpUserName);
            this.Controls.Add(this.txtFtpLoc);
            this.Controls.Add(this.btnFtpThings);
            this.Name = "Ftp";
            this.Text = "Ftp";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFtpPass;
        private System.Windows.Forms.TextBox txtFtpUserName;
        private System.Windows.Forms.TextBox txtFtpLoc;
        private System.Windows.Forms.Button btnFtpThings;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}