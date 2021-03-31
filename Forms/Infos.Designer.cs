
namespace ApmDbBackupManager.Forms
{
    partial class Infos
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtMailFrom = new System.Windows.Forms.TextBox();
            this.txtMailFromPass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMailTo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSaveMail = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFolder = new System.Windows.Forms.Button();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtSql = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSQLSave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblATmpFolder = new System.Windows.Forms.Label();
            this.lblAToMail = new System.Windows.Forms.Label();
            this.lblAFromMail = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblASqlServerName = new System.Windows.Forms.Label();
            this.btnSaveTmp = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblAHost = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblAPort = new System.Windows.Forms.Label();
            this.txtSqlUid = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSqlPass = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.lblASqlServerUid = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.cbMailCheck = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gönderici Mail";
            // 
            // txtMailFrom
            // 
            this.txtMailFrom.Location = new System.Drawing.Point(124, 38);
            this.txtMailFrom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMailFrom.Name = "txtMailFrom";
            this.txtMailFrom.Size = new System.Drawing.Size(110, 23);
            this.txtMailFrom.TabIndex = 1;
            // 
            // txtMailFromPass
            // 
            this.txtMailFromPass.Location = new System.Drawing.Point(124, 73);
            this.txtMailFromPass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMailFromPass.Name = "txtMailFromPass";
            this.txtMailFromPass.Size = new System.Drawing.Size(110, 23);
            this.txtMailFromPass.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Parola";
            // 
            // txtMailTo
            // 
            this.txtMailTo.Location = new System.Drawing.Point(124, 106);
            this.txtMailTo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMailTo.Name = "txtMailTo";
            this.txtMailTo.Size = new System.Drawing.Size(110, 23);
            this.txtMailTo.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Alıcı Mail";
            // 
            // btnSaveMail
            // 
            this.btnSaveMail.Location = new System.Drawing.Point(124, 205);
            this.btnSaveMail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveMail.Name = "btnSaveMail";
            this.btnSaveMail.Size = new System.Drawing.Size(82, 22);
            this.btnSaveMail.TabIndex = 6;
            this.btnSaveMail.Text = "Kaydet";
            this.btnSaveMail.UseVisualStyleBackColor = true;
            this.btnSaveMail.Click += new System.EventHandler(this.btnSaveMail_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(274, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Geçici Kayıt Klasörü";
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(401, 40);
            this.btnFolder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(82, 22);
            this.btnFolder.TabIndex = 8;
            this.btnFolder.Text = "Seç";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(285, 66);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(49, 15);
            this.lblAddress.TabIndex = 9;
            this.lblAddress.Text = "Address";
            this.lblAddress.Visible = false;
            // 
            // txtSql
            // 
            this.txtSql.Location = new System.Drawing.Point(618, 38);
            this.txtSql.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(110, 23);
            this.txtSql.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(516, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "SQL Sunucu İsmi";
            // 
            // btnSQLSave
            // 
            this.btnSQLSave.Location = new System.Drawing.Point(646, 122);
            this.btnSQLSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSQLSave.Name = "btnSQLSave";
            this.btnSQLSave.Size = new System.Drawing.Size(82, 22);
            this.btnSQLSave.TabIndex = 12;
            this.btnSQLSave.Text = "Kaydet";
            this.btnSQLSave.UseVisualStyleBackColor = true;
            this.btnSQLSave.Click += new System.EventHandler(this.btnSQLSave_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 15);
            this.label7.TabIndex = 13;
            this.label7.Text = "Aktif Gönderici Mail";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(285, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(138, 15);
            this.label8.TabIndex = 14;
            this.label8.Text = "Aktif Geçici Kayıt Klasörü";
            // 
            // lblATmpFolder
            // 
            this.lblATmpFolder.AutoSize = true;
            this.lblATmpFolder.Location = new System.Drawing.Point(285, 143);
            this.lblATmpFolder.Name = "lblATmpFolder";
            this.lblATmpFolder.Size = new System.Drawing.Size(38, 15);
            this.lblATmpFolder.TabIndex = 15;
            this.lblATmpFolder.Text = "label9";
            // 
            // lblAToMail
            // 
            this.lblAToMail.AutoSize = true;
            this.lblAToMail.Location = new System.Drawing.Point(177, 271);
            this.lblAToMail.Name = "lblAToMail";
            this.lblAToMail.Size = new System.Drawing.Size(44, 15);
            this.lblAToMail.TabIndex = 16;
            this.lblAToMail.Text = "label10";
            // 
            // lblAFromMail
            // 
            this.lblAFromMail.AutoSize = true;
            this.lblAFromMail.Location = new System.Drawing.Point(26, 271);
            this.lblAFromMail.Name = "lblAFromMail";
            this.lblAFromMail.Size = new System.Drawing.Size(44, 15);
            this.lblAFromMail.TabIndex = 17;
            this.lblAFromMail.Text = "label11";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(177, 245);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(84, 15);
            this.label12.TabIndex = 18;
            this.label12.Text = "Aktif Alıcı Mail";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(549, 233);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(124, 15);
            this.label13.TabIndex = 19;
            this.label13.Text = "Aktif SQL Sunucu İsmi";
            // 
            // lblASqlServerName
            // 
            this.lblASqlServerName.AutoSize = true;
            this.lblASqlServerName.Location = new System.Drawing.Point(549, 258);
            this.lblASqlServerName.Name = "lblASqlServerName";
            this.lblASqlServerName.Size = new System.Drawing.Size(44, 15);
            this.lblASqlServerName.TabIndex = 20;
            this.lblASqlServerName.Text = "label14";
            // 
            // btnSaveTmp
            // 
            this.btnSaveTmp.Location = new System.Drawing.Point(401, 91);
            this.btnSaveTmp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSaveTmp.Name = "btnSaveTmp";
            this.btnSaveTmp.Size = new System.Drawing.Size(82, 22);
            this.btnSaveTmp.TabIndex = 21;
            this.btnSaveTmp.Text = "Kaydet";
            this.btnSaveTmp.UseVisualStyleBackColor = true;
            this.btnSaveTmp.Click += new System.EventHandler(this.btnSaveTmp_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(124, 172);
            this.txtPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(110, 23);
            this.txtPort.TabIndex = 25;
            this.txtPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPort_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 15);
            this.label5.TabIndex = 24;
            this.label5.Text = "Port";
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(124, 140);
            this.txtHost.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(110, 23);
            this.txtHost.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 140);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 15);
            this.label9.TabIndex = 22;
            this.label9.Text = "Host";
            // 
            // lblAHost
            // 
            this.lblAHost.AutoSize = true;
            this.lblAHost.Location = new System.Drawing.Point(21, 332);
            this.lblAHost.Name = "lblAHost";
            this.lblAHost.Size = new System.Drawing.Size(32, 15);
            this.lblAHost.TabIndex = 26;
            this.lblAHost.Text = "Host";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(21, 303);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(60, 15);
            this.label11.TabIndex = 27;
            this.label11.Text = "Aktif Host";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(177, 303);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 15);
            this.label14.TabIndex = 29;
            this.label14.Text = "Aktif Port";
            // 
            // lblAPort
            // 
            this.lblAPort.AutoSize = true;
            this.lblAPort.Location = new System.Drawing.Point(177, 332);
            this.lblAPort.Name = "lblAPort";
            this.lblAPort.Size = new System.Drawing.Size(32, 15);
            this.lblAPort.TabIndex = 28;
            this.lblAPort.Text = "Host";
            // 
            // txtSqlUid
            // 
            this.txtSqlUid.Location = new System.Drawing.Point(618, 66);
            this.txtSqlUid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSqlUid.Name = "txtSqlUid";
            this.txtSqlUid.Size = new System.Drawing.Size(110, 23);
            this.txtSqlUid.TabIndex = 33;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(515, 73);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(97, 15);
            this.label15.TabIndex = 32;
            this.label15.Text = "SQL Kullanıcı Adı";
            // 
            // txtSqlPass
            // 
            this.txtSqlPass.Location = new System.Drawing.Point(618, 95);
            this.txtSqlPass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSqlPass.Name = "txtSqlPass";
            this.txtSqlPass.Size = new System.Drawing.Size(110, 23);
            this.txtSqlPass.TabIndex = 35;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(507, 98);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(105, 15);
            this.label16.TabIndex = 34;
            this.label16.Text = "SQL Sunucu Şifresi";
            // 
            // lblASqlServerUid
            // 
            this.lblASqlServerUid.AutoSize = true;
            this.lblASqlServerUid.Location = new System.Drawing.Point(549, 322);
            this.lblASqlServerUid.Name = "lblASqlServerUid";
            this.lblASqlServerUid.Size = new System.Drawing.Size(44, 15);
            this.lblASqlServerUid.TabIndex = 41;
            this.lblASqlServerUid.Text = "label14";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(549, 296);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(101, 15);
            this.label21.TabIndex = 40;
            this.label21.Text = "Aktif Kullanıcı Adı";
            // 
            // cbMailCheck
            // 
            this.cbMailCheck.AutoSize = true;
            this.cbMailCheck.Enabled = false;
            this.cbMailCheck.Location = new System.Drawing.Point(21, 358);
            this.cbMailCheck.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbMailCheck.Name = "cbMailCheck";
            this.cbMailCheck.Size = new System.Drawing.Size(106, 19);
            this.cbMailCheck.TabIndex = 42;
            this.cbMailCheck.Text = "Mail Onaylandı";
            this.cbMailCheck.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(382, 9);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(364, 15);
            this.label10.TabIndex = 43;
            this.label10.Text = "Bilgileri kaydettikten sonra programı yeniden başlatmanız gereklidir.";
            // 
            // Infos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 386);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cbMailCheck);
            this.Controls.Add(this.lblASqlServerUid);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtSqlPass);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtSqlUid);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.lblAPort);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblAHost);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnSaveTmp);
            this.Controls.Add(this.lblASqlServerName);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lblAFromMail);
            this.Controls.Add(this.lblAToMail);
            this.Controls.Add(this.lblATmpFolder);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnSQLSave);
            this.Controls.Add(this.txtSql);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnSaveMail);
            this.Controls.Add(this.txtMailTo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMailFromPass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMailFrom);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Infos";
            this.Text = "Infos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMailFrom;
        private System.Windows.Forms.TextBox txtMailFromPass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMailTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSaveMail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtSql;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSQLSave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblATmpFolder;
        private System.Windows.Forms.Label lblAToMail;
        private System.Windows.Forms.Label lblAFromMail;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblASqlServerName;
        private System.Windows.Forms.Button btnSaveTmp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblAHost;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblAPort;
        private System.Windows.Forms.TextBox txtSqlUid;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSqlPass;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lblASqlServerUid;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.CheckBox cbMailCheck;
        private System.Windows.Forms.Label label10;
    }
}