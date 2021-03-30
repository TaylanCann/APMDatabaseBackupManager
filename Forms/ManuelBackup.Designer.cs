
namespace ApmDbBackupManager.Forms
{
    partial class ManuelBackup
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
            this.cbDatabaseName = new System.Windows.Forms.ComboBox();
            this.chbLocal = new System.Windows.Forms.CheckBox();
            this.chbFtp = new System.Windows.Forms.CheckBox();
            this.chbGoogle = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cbDriveUsers = new System.Windows.Forms.ComboBox();
            this.lblGoogle = new System.Windows.Forms.Label();
            this.cbFtp = new System.Windows.Forms.ComboBox();
            this.lblFtp = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblInfos = new System.Windows.Forms.Label();
            this.pbManuel = new System.Windows.Forms.ProgressBar();
            this.lblBackup = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // cbDatabaseName
            // 
            this.cbDatabaseName.FormattingEnabled = true;
            this.cbDatabaseName.Location = new System.Drawing.Point(501, 52);
            this.cbDatabaseName.Name = "cbDatabaseName";
            this.cbDatabaseName.Size = new System.Drawing.Size(125, 28);
            this.cbDatabaseName.TabIndex = 49;
            // 
            // chbLocal
            // 
            this.chbLocal.AutoSize = true;
            this.chbLocal.Location = new System.Drawing.Point(415, 86);
            this.chbLocal.Name = "chbLocal";
            this.chbLocal.Size = new System.Drawing.Size(134, 24);
            this.chbLocal.TabIndex = 53;
            this.chbLocal.Text = "Local Bilgisayar";
            this.chbLocal.UseVisualStyleBackColor = true;
            this.chbLocal.CheckedChanged += new System.EventHandler(this.chbLocal_CheckedChanged);
            // 
            // chbFtp
            // 
            this.chbFtp.AutoSize = true;
            this.chbFtp.Location = new System.Drawing.Point(336, 86);
            this.chbFtp.Name = "chbFtp";
            this.chbFtp.Size = new System.Drawing.Size(52, 24);
            this.chbFtp.TabIndex = 52;
            this.chbFtp.Text = "Ftp";
            this.chbFtp.UseVisualStyleBackColor = true;
            this.chbFtp.CheckedChanged += new System.EventHandler(this.chbFtp_CheckedChanged);
            // 
            // chbGoogle
            // 
            this.chbGoogle.AutoSize = true;
            this.chbGoogle.Location = new System.Drawing.Point(229, 86);
            this.chbGoogle.Name = "chbGoogle";
            this.chbGoogle.Size = new System.Drawing.Size(80, 24);
            this.chbGoogle.TabIndex = 51;
            this.chbGoogle.Text = "Google";
            this.chbGoogle.UseVisualStyleBackColor = true;
            this.chbGoogle.CheckedChanged += new System.EventHandler(this.chbGoogle_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(406, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 50;
            this.label2.Text = "Database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(139, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 48;
            this.label1.Text = "Yedek İsmi : ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(229, 121);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 45;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(235, 53);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(125, 27);
            this.txtName.TabIndex = 47;
            // 
            // cbDriveUsers
            // 
            this.cbDriveUsers.FormattingEnabled = true;
            this.cbDriveUsers.Location = new System.Drawing.Point(176, 203);
            this.cbDriveUsers.Name = "cbDriveUsers";
            this.cbDriveUsers.Size = new System.Drawing.Size(166, 28);
            this.cbDriveUsers.TabIndex = 56;
            this.cbDriveUsers.Visible = false;
            // 
            // lblGoogle
            // 
            this.lblGoogle.AutoSize = true;
            this.lblGoogle.Location = new System.Drawing.Point(187, 180);
            this.lblGoogle.Name = "lblGoogle";
            this.lblGoogle.Size = new System.Drawing.Size(135, 20);
            this.lblGoogle.TabIndex = 55;
            this.lblGoogle.Text = "Google User Name";
            this.lblGoogle.Visible = false;
            // 
            // cbFtp
            // 
            this.cbFtp.FormattingEnabled = true;
            this.cbFtp.Location = new System.Drawing.Point(383, 203);
            this.cbFtp.Name = "cbFtp";
            this.cbFtp.Size = new System.Drawing.Size(166, 28);
            this.cbFtp.TabIndex = 58;
            this.cbFtp.Visible = false;
            // 
            // lblFtp
            // 
            this.lblFtp.AutoSize = true;
            this.lblFtp.Location = new System.Drawing.Point(429, 180);
            this.lblFtp.Name = "lblFtp";
            this.lblFtp.Size = new System.Drawing.Size(72, 20);
            this.lblFtp.TabIndex = 57;
            this.lblFtp.Text = "Ftp Adres";
            this.lblFtp.Visible = false;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(415, 121);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(47, 20);
            this.lblAddress.TabIndex = 59;
            this.lblAddress.Text = "Adres";
            this.lblAddress.Visible = false;
            // 
            // lblInfos
            // 
            this.lblInfos.AutoSize = true;
            this.lblInfos.Location = new System.Drawing.Point(12, 9);
            this.lblInfos.Name = "lblInfos";
            this.lblInfos.Size = new System.Drawing.Size(184, 20);
            this.lblInfos.TabIndex = 71;
            this.lblInfos.Text = "Lütfen bütün bilgileri girin.";
            this.lblInfos.Visible = false;
            // 
            // pbManuel
            // 
            this.pbManuel.Location = new System.Drawing.Point(176, 284);
            this.pbManuel.Name = "pbManuel";
            this.pbManuel.Size = new System.Drawing.Size(436, 29);
            this.pbManuel.TabIndex = 73;
            this.pbManuel.Visible = false;
            // 
            // lblBackup
            // 
            this.lblBackup.AutoSize = true;
            this.lblBackup.Location = new System.Drawing.Point(229, 261);
            this.lblBackup.Name = "lblBackup";
            this.lblBackup.Size = new System.Drawing.Size(313, 20);
            this.lblBackup.TabIndex = 74;
            this.lblBackup.Text = "Backup Alınıyor Lütfen Sayfayı Değiştirmeyin...";
            this.lblBackup.Visible = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // ManuelBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblBackup);
            this.Controls.Add(this.pbManuel);
            this.Controls.Add(this.lblInfos);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.cbFtp);
            this.Controls.Add(this.lblFtp);
            this.Controls.Add(this.cbDriveUsers);
            this.Controls.Add(this.lblGoogle);
            this.Controls.Add(this.cbDatabaseName);
            this.Controls.Add(this.chbLocal);
            this.Controls.Add(this.chbFtp);
            this.Controls.Add(this.chbGoogle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtName);
            this.Name = "ManuelBackup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cbDatabaseName;
        private System.Windows.Forms.CheckBox chbLocal;
        private System.Windows.Forms.CheckBox chbFtp;
        private System.Windows.Forms.CheckBox chbGoogle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cbDriveUsers;
        private System.Windows.Forms.Label lblGoogle;
        private System.Windows.Forms.ComboBox cbFtp;
        private System.Windows.Forms.Label lblFtp;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblInfos;
        private System.Windows.Forms.ProgressBar pbManuel;
        private System.Windows.Forms.Label lblBackup;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}