﻿
namespace ApmDbBackupManager
{
    partial class DriveForm
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
            this.label3 = new System.Windows.Forms.Label();
            this.txtGoogleUser = new System.Windows.Forms.TextBox();
            this.btnGoogleUser = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 20);
            this.label3.TabIndex = 28;
            this.label3.Text = "Google User Name";
            // 
            // txtGoogleUser
            // 
            this.txtGoogleUser.Location = new System.Drawing.Point(152, 71);
            this.txtGoogleUser.Name = "txtGoogleUser";
            this.txtGoogleUser.Size = new System.Drawing.Size(166, 27);
            this.txtGoogleUser.TabIndex = 27;
            // 
            // btnGoogleUser
            // 
            this.btnGoogleUser.Location = new System.Drawing.Point(152, 104);
            this.btnGoogleUser.Name = "btnGoogleUser";
            this.btnGoogleUser.Size = new System.Drawing.Size(166, 29);
            this.btnGoogleUser.TabIndex = 26;
            this.btnGoogleUser.Text = "Google Con.";
            this.btnGoogleUser.UseVisualStyleBackColor = true;
            this.btnGoogleUser.Click += new System.EventHandler(this.btnGoogleUser_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(34, 160);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(296, 262);
            this.dataGridView1.TabIndex = 29;
            // 
            // DriveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 481);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGoogleUser);
            this.Controls.Add(this.btnGoogleUser);
            this.Name = "DriveForm";
            this.Text = "DriveForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGoogleUser;
        private System.Windows.Forms.Button btnGoogleUser;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}