
namespace ApmDbBackupManager.Forms
{
    partial class AutoBackup
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
            this.rbDaily = new System.Windows.Forms.RadioButton();
            this.rbWeekly = new System.Windows.Forms.RadioButton();
            this.rbMonthly = new System.Windows.Forms.RadioButton();
            this.dtpSetUp = new System.Windows.Forms.DateTimePicker();
            this.rbYearly = new System.Windows.Forms.RadioButton();
            this.cbDriveUsers = new System.Windows.Forms.ComboBox();
            this.lblGoogle = new System.Windows.Forms.Label();
            this.lblFtp = new System.Windows.Forms.Label();
            this.cbFtp = new System.Windows.Forms.ComboBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.dgBackupSchedule = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.lblInfos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgBackupSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDatabaseName
            // 
            this.cbDatabaseName.FormattingEnabled = true;
            this.cbDatabaseName.Location = new System.Drawing.Point(601, 123);
            this.cbDatabaseName.Name = "cbDatabaseName";
            this.cbDatabaseName.Size = new System.Drawing.Size(107, 28);
            this.cbDatabaseName.TabIndex = 54;
            // 
            // chbLocal
            // 
            this.chbLocal.AutoSize = true;
            this.chbLocal.Location = new System.Drawing.Point(441, 174);
            this.chbLocal.Name = "chbLocal";
            this.chbLocal.Size = new System.Drawing.Size(134, 24);
            this.chbLocal.TabIndex = 58;
            this.chbLocal.Text = "Local Bilgisayar";
            this.chbLocal.UseVisualStyleBackColor = true;
            this.chbLocal.CheckedChanged += new System.EventHandler(this.chbLocal_CheckedChanged);
            // 
            // chbFtp
            // 
            this.chbFtp.AutoSize = true;
            this.chbFtp.Location = new System.Drawing.Point(362, 174);
            this.chbFtp.Name = "chbFtp";
            this.chbFtp.Size = new System.Drawing.Size(52, 24);
            this.chbFtp.TabIndex = 57;
            this.chbFtp.Text = "Ftp";
            this.chbFtp.UseVisualStyleBackColor = true;
            this.chbFtp.CheckedChanged += new System.EventHandler(this.chbFtp_CheckedChanged);
            // 
            // chbGoogle
            // 
            this.chbGoogle.AutoSize = true;
            this.chbGoogle.Location = new System.Drawing.Point(255, 174);
            this.chbGoogle.Name = "chbGoogle";
            this.chbGoogle.Size = new System.Drawing.Size(80, 24);
            this.chbGoogle.TabIndex = 56;
            this.chbGoogle.Text = "Google";
            this.chbGoogle.UseVisualStyleBackColor = true;
            this.chbGoogle.CheckedChanged += new System.EventHandler(this.chbGoogle_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(523, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 55;
            this.label2.Text = "Database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 53;
            this.label1.Text = "Yedek İsmi : ";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(255, 244);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(94, 29);
            this.btnSave.TabIndex = 49;
            this.btnSave.Text = "Kaydet";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(374, 123);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(125, 27);
            this.txtName.TabIndex = 52;
            // 
            // rbDaily
            // 
            this.rbDaily.AutoSize = true;
            this.rbDaily.Location = new System.Drawing.Point(268, 32);
            this.rbDaily.Name = "rbDaily";
            this.rbDaily.Size = new System.Drawing.Size(75, 24);
            this.rbDaily.TabIndex = 45;
            this.rbDaily.Text = "Günlük";
            this.rbDaily.UseVisualStyleBackColor = true;
            this.rbDaily.CheckedChanged += new System.EventHandler(this.rbDaily_CheckedChanged);
            // 
            // rbWeekly
            // 
            this.rbWeekly.AutoSize = true;
            this.rbWeekly.Location = new System.Drawing.Point(349, 32);
            this.rbWeekly.Name = "rbWeekly";
            this.rbWeekly.Size = new System.Drawing.Size(82, 24);
            this.rbWeekly.TabIndex = 50;
            this.rbWeekly.Text = "Haftalık";
            this.rbWeekly.UseVisualStyleBackColor = true;
            this.rbWeekly.CheckedChanged += new System.EventHandler(this.rbWeekly_CheckedChanged);
            // 
            // rbMonthly
            // 
            this.rbMonthly.AutoSize = true;
            this.rbMonthly.Location = new System.Drawing.Point(437, 32);
            this.rbMonthly.Name = "rbMonthly";
            this.rbMonthly.Size = new System.Drawing.Size(62, 24);
            this.rbMonthly.TabIndex = 46;
            this.rbMonthly.Text = "Aylık";
            this.rbMonthly.UseVisualStyleBackColor = true;
            this.rbMonthly.CheckedChanged += new System.EventHandler(this.rbMonthly_CheckedChanged);
            // 
            // dtpSetUp
            // 
            this.dtpSetUp.Location = new System.Drawing.Point(265, 77);
            this.dtpSetUp.Name = "dtpSetUp";
            this.dtpSetUp.Size = new System.Drawing.Size(310, 27);
            this.dtpSetUp.TabIndex = 48;
            // 
            // rbYearly
            // 
            this.rbYearly.AutoSize = true;
            this.rbYearly.Location = new System.Drawing.Point(505, 32);
            this.rbYearly.Name = "rbYearly";
            this.rbYearly.Size = new System.Drawing.Size(61, 24);
            this.rbYearly.TabIndex = 47;
            this.rbYearly.Text = "Yıllık";
            this.rbYearly.UseVisualStyleBackColor = true;
            this.rbYearly.CheckedChanged += new System.EventHandler(this.rbYearly_CheckedChanged);
            // 
            // cbDriveUsers
            // 
            this.cbDriveUsers.FormattingEnabled = true;
            this.cbDriveUsers.Location = new System.Drawing.Point(42, 116);
            this.cbDriveUsers.Name = "cbDriveUsers";
            this.cbDriveUsers.Size = new System.Drawing.Size(166, 28);
            this.cbDriveUsers.TabIndex = 61;
            this.cbDriveUsers.Visible = false;
            // 
            // lblGoogle
            // 
            this.lblGoogle.AutoSize = true;
            this.lblGoogle.Location = new System.Drawing.Point(53, 93);
            this.lblGoogle.Name = "lblGoogle";
            this.lblGoogle.Size = new System.Drawing.Size(135, 20);
            this.lblGoogle.TabIndex = 60;
            this.lblGoogle.Text = "Google User Name";
            this.lblGoogle.Visible = false;
            // 
            // lblFtp
            // 
            this.lblFtp.AutoSize = true;
            this.lblFtp.Location = new System.Drawing.Point(84, 180);
            this.lblFtp.Name = "lblFtp";
            this.lblFtp.Size = new System.Drawing.Size(72, 20);
            this.lblFtp.TabIndex = 65;
            this.lblFtp.Text = "Ftp Adres";
            this.lblFtp.Visible = false;
            // 
            // cbFtp
            // 
            this.cbFtp.FormattingEnabled = true;
            this.cbFtp.Location = new System.Drawing.Point(42, 215);
            this.cbFtp.Name = "cbFtp";
            this.cbFtp.Size = new System.Drawing.Size(166, 28);
            this.cbFtp.TabIndex = 66;
            this.cbFtp.Visible = false;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(441, 205);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(47, 20);
            this.lblAddress.TabIndex = 67;
            this.lblAddress.Text = "Adres";
            this.lblAddress.Visible = false;
            // 
            // dgBackupSchedule
            // 
            this.dgBackupSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBackupSchedule.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgBackupSchedule.Location = new System.Drawing.Point(0, 337);
            this.dgBackupSchedule.Name = "dgBackupSchedule";
            this.dgBackupSchedule.RowHeadersWidth = 51;
            this.dgBackupSchedule.RowTemplate.Height = 29;
            this.dgBackupSchedule.Size = new System.Drawing.Size(1184, 307);
            this.dgBackupSchedule.TabIndex = 68;
            this.dgBackupSchedule.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgBackupSchedule_CellDoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 317);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 20);
            this.label3.TabIndex = 69;
            this.label3.Text = "Silmek için çift tıklayın";
            // 
            // lblInfos
            // 
            this.lblInfos.AutoSize = true;
            this.lblInfos.Location = new System.Drawing.Point(42, 32);
            this.lblInfos.Name = "lblInfos";
            this.lblInfos.Size = new System.Drawing.Size(184, 20);
            this.lblInfos.TabIndex = 70;
            this.lblInfos.Text = "Lütfen bütün bilgileri girin.";
            this.lblInfos.Visible = false;
            // 
            // AutoBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 644);
            this.Controls.Add(this.lblInfos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgBackupSchedule);
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
            this.Controls.Add(this.rbDaily);
            this.Controls.Add(this.rbWeekly);
            this.Controls.Add(this.rbMonthly);
            this.Controls.Add(this.dtpSetUp);
            this.Controls.Add(this.rbYearly);
            this.Name = "AutoBackup";
            this.Text = "AutoBackup";
            ((System.ComponentModel.ISupportInitialize)(this.dgBackupSchedule)).EndInit();
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
        private System.Windows.Forms.RadioButton rbDaily;
        private System.Windows.Forms.RadioButton rbWeekly;
        private System.Windows.Forms.RadioButton rbMonthly;
        private System.Windows.Forms.DateTimePicker dtpSetUp;
        private System.Windows.Forms.RadioButton rbYearly;
        private System.Windows.Forms.ComboBox cbDriveUsers;
        private System.Windows.Forms.Label lblGoogle;
        private System.Windows.Forms.Label lblFtp;
        private System.Windows.Forms.ComboBox cbFtp;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.DataGridView dgBackupSchedule;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblInfos;
    }
}