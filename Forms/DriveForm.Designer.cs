
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
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(143, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 15);
            this.label3.TabIndex = 28;
            this.label3.Text = "Google User Name";
            // 
            // txtGoogleUser
            // 
            this.txtGoogleUser.Location = new System.Drawing.Point(133, 53);
            this.txtGoogleUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtGoogleUser.Name = "txtGoogleUser";
            this.txtGoogleUser.Size = new System.Drawing.Size(146, 23);
            this.txtGoogleUser.TabIndex = 27;
            // 
            // btnGoogleUser
            // 
            this.btnGoogleUser.Location = new System.Drawing.Point(133, 78);
            this.btnGoogleUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnGoogleUser.Name = "btnGoogleUser";
            this.btnGoogleUser.Size = new System.Drawing.Size(145, 22);
            this.btnGoogleUser.TabIndex = 26;
            this.btnGoogleUser.Text = "Google Con.";
            this.btnGoogleUser.UseVisualStyleBackColor = true;
            this.btnGoogleUser.Click += new System.EventHandler(this.btnGoogleUser_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 187);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(724, 196);
            this.dataGridView1.TabIndex = 29;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 15);
            this.label1.TabIndex = 30;
            this.label1.Text = "Silmek için çift tıklayın.";
            // 
            // DriveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 383);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGoogleUser);
            this.Controls.Add(this.btnGoogleUser);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Label label1;
    }
}