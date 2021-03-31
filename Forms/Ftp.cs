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
            var db = context.FtpThings.Where(f => f.IsActive == true).ToList();
            dataGridView1.DataSource = db.Select(e => new
            {
                Address = e.FtpLocation,
                UserName = e.FtpUserName,
            }).ToList();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
            {
                DialogResult dialog = new DialogResult();
                dialog = MessageBox.Show(dr.Cells[0].Value.ToString() + " Kaydını silmek istediğinize emin misiniz?", "SİL", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    foreach (var item in context.FtpThings.ToList())
                    {
                        if (item.FtpLocation == dr.Cells[0].Value.ToString() && item.IsActive==true)
                        {
                            item.IsActive = false;
                            context.Update(item);
                            context.SaveChanges();
                            break;
                        }
                    }
                    MessageBox.Show(dr.Cells[0].Value.ToString() + " Silindi");
                }
                else
                {
                    MessageBox.Show(dr.Cells[0].Value.ToString() + " Silinmedi");
                }
                Listing();
            }
        }

        public string ftpAddressCheck(string address)
        {
            //ftp://
            if (!address.Contains("ftp://"))
            {
                address = "ftp://" + address;
            }
            if (!address.EndsWith("/"))
            {
                address = address + "/";
            }
            return address;
        }

        private void btnFtpThings_Click(object sender, EventArgs e)
        {
            FtpLoc = txtFtpLoc.Text;
            FtpUser = txtFtpUserName.Text;
            FtpPass = txtFtpPass.Text;
            FtpLoc = ftpAddressCheck(FtpLoc);
            FtpThing ftpThing = new FtpThing();
            ftpThing.FtpLocation = FtpLoc;
            ftpThing.FtpPassword = FtpPass;
            ftpThing.FtpUserName = FtpUser;
            ftpThing.IsActive = true;
            context.FtpThings.Add(ftpThing);
            context.SaveChanges();
            Listing();

        }
    }
}
