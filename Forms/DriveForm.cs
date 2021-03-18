using ApmDbBackupManager.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace ApmDbBackupManager
{
    public partial class DriveForm : Form
    {
        public DriveForm()
        {
            InitializeComponent();
            Listing();
        }

        string DriveUserName;
        DatabaseContext context = new DatabaseContext();

        public void Listing()
        {
            var list = context.DriveUsers.ToList();            
            dataGridView1.DataSource = list;
        }

        private void btnGoogleUser_Click(object sender, EventArgs e)
        {
            DriveUserName = txtGoogleUser.Text;
            DriveUser driveUser = new DriveUser();
            driveUser.User = DriveUserName;
            context.DriveUsers.Add(driveUser);
            context.SaveChanges();
            Listing();
        }
    }
}
