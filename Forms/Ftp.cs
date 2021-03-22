using ApmDbBackupManager.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ApmDbBackupManager
{
    public partial class Ftp : Form
    {
        public Ftp()
        {
            InitializeComponent();
            Listing();
        }
        DatabaseContext context = new DatabaseContext();
        string FtpLoc,FtpUser,FtpPass;

        public void Listing()
        {
            var list = context.FtpThings.ToList();
            dataGridView1.DataSource = list;
        }

        private void btnFtpThings_Click(object sender, EventArgs e)
        {
            FtpLoc = txtFtpLoc.Text;
            FtpUser = txtFtpUserName.Text;
            FtpPass = txtFtpPass.Text;
            FtpThing ftpThing = new FtpThing();
            ftpThing.FtpLocation = FtpLoc;
            ftpThing.FtpPassword = FtpPass;
            ftpThing.FtpUserName = FtpUser;
            context.FtpThings.Add(ftpThing);
            context.SaveChanges();
            Listing();

        }
    }
}
