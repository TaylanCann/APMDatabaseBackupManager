
namespace ApmDbBackupManager
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pnLeft = new System.Windows.Forms.Panel();
            this.btnInfos = new System.Windows.Forms.Button();
            this.btnFtp = new System.Windows.Forms.Button();
            this.btnDrive = new System.Windows.Forms.Button();
            this.btnManuelBackup = new System.Windows.Forms.Button();
            this.btnAutoBackup = new System.Windows.Forms.Button();
            this.pnLogo = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.pnTitle = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnForms = new System.Windows.Forms.Panel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.pnLeft.SuspendLayout();
            this.pnLogo.SuspendLayout();
            this.pnTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnLeft
            // 
            this.pnLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.pnLeft.Controls.Add(this.btnInfos);
            this.pnLeft.Controls.Add(this.btnFtp);
            this.pnLeft.Controls.Add(this.btnDrive);
            this.pnLeft.Controls.Add(this.btnManuelBackup);
            this.pnLeft.Controls.Add(this.btnAutoBackup);
            this.pnLeft.Controls.Add(this.pnLogo);
            this.pnLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnLeft.Location = new System.Drawing.Point(0, 0);
            this.pnLeft.Margin = new System.Windows.Forms.Padding(0);
            this.pnLeft.Name = "pnLeft";
            this.pnLeft.Size = new System.Drawing.Size(220, 681);
            this.pnLeft.TabIndex = 26;
            // 
            // btnInfos
            // 
            this.btnInfos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInfos.FlatAppearance.BorderSize = 0;
            this.btnInfos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInfos.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnInfos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInfos.Location = new System.Drawing.Point(0, 320);
            this.btnInfos.Name = "btnInfos";
            this.btnInfos.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnInfos.Size = new System.Drawing.Size(220, 60);
            this.btnInfos.TabIndex = 10;
            this.btnInfos.Text = "Bilgiler";
            this.btnInfos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInfos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInfos.UseVisualStyleBackColor = true;
            this.btnInfos.Click += new System.EventHandler(this.btnInfos_Click);
            // 
            // btnFtp
            // 
            this.btnFtp.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFtp.FlatAppearance.BorderSize = 0;
            this.btnFtp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFtp.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnFtp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFtp.Location = new System.Drawing.Point(0, 260);
            this.btnFtp.Name = "btnFtp";
            this.btnFtp.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnFtp.Size = new System.Drawing.Size(220, 60);
            this.btnFtp.TabIndex = 9;
            this.btnFtp.Text = "File Transfer Protocol";
            this.btnFtp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFtp.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFtp.UseVisualStyleBackColor = true;
            this.btnFtp.Click += new System.EventHandler(this.btnFtp_Click);
            // 
            // btnDrive
            // 
            this.btnDrive.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDrive.FlatAppearance.BorderSize = 0;
            this.btnDrive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDrive.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDrive.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDrive.Location = new System.Drawing.Point(0, 200);
            this.btnDrive.Name = "btnDrive";
            this.btnDrive.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnDrive.Size = new System.Drawing.Size(220, 60);
            this.btnDrive.TabIndex = 8;
            this.btnDrive.Text = "Drive";
            this.btnDrive.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDrive.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDrive.UseVisualStyleBackColor = true;
            this.btnDrive.Click += new System.EventHandler(this.btnDrive_Click);
            // 
            // btnManuelBackup
            // 
            this.btnManuelBackup.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnManuelBackup.FlatAppearance.BorderSize = 0;
            this.btnManuelBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManuelBackup.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnManuelBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManuelBackup.Location = new System.Drawing.Point(0, 140);
            this.btnManuelBackup.Name = "btnManuelBackup";
            this.btnManuelBackup.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnManuelBackup.Size = new System.Drawing.Size(220, 60);
            this.btnManuelBackup.TabIndex = 7;
            this.btnManuelBackup.Text = "Manuel Yedekleme";
            this.btnManuelBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManuelBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnManuelBackup.UseVisualStyleBackColor = true;
            this.btnManuelBackup.Click += new System.EventHandler(this.btnManuelBackup_Click);
            // 
            // btnAutoBackup
            // 
            this.btnAutoBackup.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAutoBackup.FlatAppearance.BorderSize = 0;
            this.btnAutoBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoBackup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnAutoBackup.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnAutoBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAutoBackup.Location = new System.Drawing.Point(0, 80);
            this.btnAutoBackup.Name = "btnAutoBackup";
            this.btnAutoBackup.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnAutoBackup.Size = new System.Drawing.Size(220, 60);
            this.btnAutoBackup.TabIndex = 6;
            this.btnAutoBackup.Text = "Otomatik Yedekleme";
            this.btnAutoBackup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAutoBackup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAutoBackup.UseVisualStyleBackColor = true;
            this.btnAutoBackup.Click += new System.EventHandler(this.btnAutoBackup_Click);
            // 
            // pnLogo
            // 
            this.pnLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.pnLogo.Controls.Add(this.label3);
            this.pnLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnLogo.Location = new System.Drawing.Point(0, 0);
            this.pnLogo.Name = "pnLogo";
            this.pnLogo.Size = new System.Drawing.Size(220, 80);
            this.pnLogo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(24, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 37);
            this.label3.TabIndex = 0;
            this.label3.Text = "APM BACKUP";
            // 
            // pnTitle
            // 
            this.pnTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.pnTitle.Controls.Add(this.lblTitle);
            this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitle.Location = new System.Drawing.Point(220, 0);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(1002, 80);
            this.pnTitle.TabIndex = 28;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Perpetua Titling MT", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(422, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(190, 33);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Ana Sayfa";
            // 
            // pnForms
            // 
            this.pnForms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnForms.Location = new System.Drawing.Point(220, 80);
            this.pnForms.Name = "pnForms";
            this.pnForms.Size = new System.Drawing.Size(1002, 601);
            this.pnForms.TabIndex = 29;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Program Arka Planda Çalışmayı Sürdürüyor";
            this.notifyIcon1.BalloonTipTitle = "APM DB Manager";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 681);
            this.Controls.Add(this.pnForms);
            this.Controls.Add(this.pnTitle);
            this.Controls.Add(this.pnLeft);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "APM DataBase Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.pnLeft.ResumeLayout(false);
            this.pnLogo.ResumeLayout(false);
            this.pnLogo.PerformLayout();
            this.pnTitle.ResumeLayout(false);
            this.pnTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnLeft;
        private System.Windows.Forms.Button btnFtp;
        private System.Windows.Forms.Button btnDrive;
        private System.Windows.Forms.Button btnManuelBackup;
        private System.Windows.Forms.Button btnAutoBackup;
        private System.Windows.Forms.Panel pnLogo;
        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnForms;
        private System.Windows.Forms.Button btnInfos;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

