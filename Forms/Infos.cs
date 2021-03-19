using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ApmDbBackupManager.Forms
{
    public partial class Infos : Form
    {
        public Infos()
        {
            InitializeComponent();
            listing();
        }

        string pathCTemp;

        private void btnFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                pathCTemp = fbd.SelectedPath;
                pathCTemp += @"\";
            }
            if (pathCTemp == "")
            {
                lblAddress.Visible = false;
            }
            else
            {
                lblAddress.Visible = true;
                lblAddress.Text = pathCTemp;
            }
        }

        private void btnSQLSave_Click(object sender, EventArgs e)
        {   
            Properties.Settings.Default.SqlAddress = txtSql.Text;
            Properties.Settings.Default.Save();
            listing();
        }

        private void btnSaveMail_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.From = txtMailFrom.Text;            
            Properties.Settings.Default.Pass = txtMailFromPass.Text;            
            Properties.Settings.Default.To = txtMailTo.Text;
            Properties.Settings.Default.Port = Convert.ToInt32(txtPort.Text);
            Properties.Settings.Default.Host = txtHost.Text;
            Properties.Settings.Default.Save();
            listing();
        }

        private void btnSaveTmp_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.pathCTemp = pathCTemp;
            Properties.Settings.Default.Save();
            listing();
        }
        public void listing()
        {
            if (Properties.Settings.Default.From != null)
            {
                lblAFromMail.Text = Properties.Settings.Default.From;
            }
            else
            {
                lblAFromMail.Text = "Yok lütfen giriş yapın.";
            }
            if (Properties.Settings.Default.To != null)
            {
                lblAToMail.Text = Properties.Settings.Default.To;
            }
            else
            {
                lblAToMail.Text = "Yok lütfen giriş yapın.";
            }
            if (Properties.Settings.Default.SqlAddress != null)
            {
                lblASqlServerName.Text = Properties.Settings.Default.SqlAddress;
            }
            else
            {
                lblASqlServerName.Text = "Yok lütfen giriş yapın.";
            }
            if (Properties.Settings.Default.pathCTemp != null)
            {
                lblATmpFolder.Text = Properties.Settings.Default.pathCTemp;
            }
            else
            {
                lblATmpFolder.Text = "Yok lütfen giriş yapın.";
            }
            if (Properties.Settings.Default.Port != 0)
            {
                lblAPort.Text = Properties.Settings.Default.Port.ToString();
            }
            else
            {
                lblAPort.Text = "Yok lütfen giriş yapın.";
            }
            if (Properties.Settings.Default.Host != null)
            {
                lblAHost.Text = Properties.Settings.Default.Host;
            }
            else
            {
                lblAHost.Text = "Yok lütfen giriş yapın.";
            }
        }
    }
}
