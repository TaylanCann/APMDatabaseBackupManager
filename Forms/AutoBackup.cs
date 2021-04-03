using ApmDbBackupManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace ApmDbBackupManager.Forms
{
    public partial class AutoBackup : Form
    {
        DatabaseContext context = new DatabaseContext();
        BackupSchedule lastBackup = new BackupSchedule();
        List<string> names = new List<string>();
        string selectedPath, DriveUserName, FtpLoc;
        string pathCTemp = @"" + Properties.Settings.Default.pathCTemp;
        string SqlAddress = Properties.Settings.Default.SqlAddress;
        string SqlPass = Properties.Settings.Default.SqlPass;
        string SqlUid = Properties.Settings.Default.Uid;

        public AutoBackup()
        {
            InitializeComponent();
            #region Controls
            if (SqlAddress == "" || SqlPass == "" || SqlUid == "" || pathCTemp == "")
            {
                lblInfos.Visible = true;
            }
            if (SqlAddress != "" || SqlPass != "" || SqlUid != "")
            {
                DatabaseNamesListing();
            }
            listing();
            if (pathCTemp != "")
            {
                TmpExists(pathCTemp);
            }
            if (CheckClient())
            {
                chbGoogle.Enabled = true;
            }
            else
            {
                chbGoogle.Enabled = false;
            }
            #endregion
            DriveUsers();
            FtpAddress();
        }
        public bool CheckClient()
        {
            var CheckClientJson = Directory.GetFiles(Application.StartupPath).Any(e => e.Contains("client_secret.json"));
            return CheckClientJson;
        }
        public void TxtLog(string writeText)
        {
            try
            {
                string fileName = @"Log.txt";

                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Close();
                File.AppendAllText(fileName, Environment.NewLine + DateTime.Now.ToString() + "=>" + writeText);
            }
            catch (Exception)
            {
                MessageBox.Show("Txt oluştururken hata yapıldı.");
            }
        }
        public void TmpExists(string pathCTemp)
        {
            try
            {
                if (Directory.Exists(pathCTemp))
                {
                    return;
                }
                DirectoryInfo di = Directory.CreateDirectory(pathCTemp);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(pathCTemp));
            }
            catch (Exception e)
            {
                MessageBox.Show("Tmp oluştururken hata yapıldı.");
                TxtLog("Hata : " + e.Message);
                using (StreamReader sr = File.OpenText(@"Log.txt"))
                {
                    string s = e.Message+ "Tmp oluştururken hata yapıldı.";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                }
            }
        }
        public void DatabaseNamesListing()
        {
            try
            {
                //"Server=192.168.1.248; Database =Cisa; Uid=sa; password=136213Ata!!; MultipleActiveResultSets=True;"
                string connetionString = null;
                SqlConnection conn;
                connetionString = @"Server=" + SqlAddress + "; Uid" + "=" + SqlUid + "; password=" + SqlPass + "; MultipleActiveResultSets = True; ";
                conn = new SqlConnection(connetionString);
                try
                {
                    conn.Open();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Can not open connection ! ");
                    TxtLog("Hata : " + e.Message);
                }

                SqlCommand com1 = new SqlCommand("SELECT name, database_id, create_date  FROM sys.databases ; ", conn);
                if (com1.Connection.State != ConnectionState.Open)
                {
                    com1.Connection.Open();
                }
                SqlDataReader dr = com1.ExecuteReader();
                while (dr.Read())
                {
                    names.Add(dr["name"].ToString());
                }
                dr.Close();

                names.Remove("master");
                names.Remove("tempdb");
                names.Remove("model");
                names.Remove("msdb");

                foreach (var item in names)
                {
                    cbDatabaseName.Items.Add(item);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Database isimleri listelenirken hata yaşandı.");
                TxtLog("Hata : " + e.Message + " Database isimleri listelenirken hata yaşandı." + " AutoBackup");
            }

        }
        public void DriveUsers()
        {
            var records = context.DriveUsers.Where(e=>e.IsActive)
               .ToList();

            foreach (var item in records)
            {
                if (item.User != null)
                {
                    cbDriveUsers.Items.Add(item.User.ToString());
                }
            }
        }
        public void FtpAddress()
        {
            var records = context.FtpThings.Where(e => e.IsActive)
              .ToList();

            foreach (var item in records)
            {
                if (item.FtpLocation != null)
                {
                    cbFtp.Items.Add(item.FtpLocation.ToString());
                }
            }
        }
        public void listing()
        {
            try
            {
                var db = context.BackupSchedules.Where(f => f.IsActive == true && f.IsAuto == true).ToList();
                dgBackupSchedule.DataSource = db.Select(e => new
                {
                    Name = e.JustName,
                    Google = e.IsDrive,
                    Sunucu = e.IsFtp,
                    Local = e.IsLocal
                }).ToList();
                DataGridViewColumn column = dgBackupSchedule.Columns[0];
                column.Width = 400;
            }
            catch (Exception e)
            {
                MessageBox.Show("Auto Backup Listelerken hata oluştu");
                TxtLog("Hata : " + e.Message + " Auto Backup Listelerken hata oluştu." + " AutoBackup");
            }
        }
        public void Save()
        {
            try
            {
                BackupSchedule backupSchedule = new BackupSchedule();

                backupSchedule.IsAuto = true;
                backupSchedule.IsActive = true;
                #region Schame
                if (rbDaily.Checked == true)
                {

                    backupSchedule.Time = dtpSetUp.Value;
                    backupSchedule.BackupScheme = 1;
                    backupSchedule.DaysAdd = 1;
                    backupSchedule.DaysAddTerm = 7;
                    backupSchedule.MonthAdd = 0;
                    backupSchedule.MonthAddTerm = 0;
                    backupSchedule.DiffTime = backupSchedule.Time.AddDays((int)backupSchedule.DaysAdd);
                }
                else if (rbWeekly.Checked == true)
                {
                    backupSchedule.Time = dtpSetUp.Value;
                    backupSchedule.BackupScheme = 2;
                    backupSchedule.DaysAdd = 7;
                    backupSchedule.DaysAddTerm = 28;
                    backupSchedule.MonthAdd = 0;
                    backupSchedule.MonthAddTerm = 0;
                    backupSchedule.DiffTime = backupSchedule.Time.AddDays((int)backupSchedule.DaysAdd);
                }
                else if (rbMonthly.Checked == true)
                {
                    backupSchedule.Time = dtpSetUp.Value;
                    backupSchedule.BackupScheme = 3;
                    backupSchedule.DaysAdd = 0;
                    backupSchedule.DaysAddTerm = 0;
                    backupSchedule.MonthAdd = 1;
                    backupSchedule.MonthAddTerm = 12;
                    backupSchedule.DiffTime = backupSchedule.Time.AddMonths((int)backupSchedule.MonthAdd);
                }
                else if (rbYearly.Checked == true)
                {
                    backupSchedule.Time = dtpSetUp.Value;
                    backupSchedule.BackupScheme = 4;
                    backupSchedule.DaysAdd = 0;
                    backupSchedule.DaysAddTerm = 365;
                    backupSchedule.MonthAdd = 0;
                    backupSchedule.MonthAddTerm = 0;
                }

                #endregion

                #region AutoSaveArea
                if (chbFtp.Checked == true)
                {
                    backupSchedule.IsFtp = true;
                    foreach (var item in context.FtpThings.ToList())
                    {
                        if (FtpLoc == item.FtpLocation)
                        {
                            backupSchedule.FtpThingId = item.Id;
                        }
                    }
                }
                else
                {
                    backupSchedule.IsFtp = false;
                }
                if (chbGoogle.Checked == true)
                {
                    backupSchedule.IsDrive = true;
                    foreach (var item in context.DriveUsers.ToList())
                    {
                        if (DriveUserName == item.User)
                        {
                            backupSchedule.DriveUserId = item.Id;
                        }
                    }
                }
                else
                {
                    backupSchedule.IsDrive = false;
                }
                if (chbLocal.Checked == true)
                {
                    backupSchedule.IsLocal = true;
                    backupSchedule.LocalLocation = selectedPath;
                }
                else
                {
                    backupSchedule.IsLocal = false;
                }
                #endregion

                backupSchedule.HaveIt = false;
                backupSchedule.BackupName = txtName.Text.ToString();
                backupSchedule.DbName = cbDatabaseName.SelectedItem.ToString();
                backupSchedule.IsDiffBackup = false;
                //Database'e gömülmesi muhtemel Backup bilgileri kontrol için tamamen toplandı.

                #region time4NameFullB
                string D;
                if (backupSchedule.BackupScheme == 1)
                {
                    D = "Günlük";
                }
                else if (backupSchedule.BackupScheme == 2)
                {
                    D = "Haftalık";
                }
                else if (backupSchedule.BackupScheme == 3)
                {
                    D = "Aylık";
                }
                else
                {
                    D = "Yıllık";
                }
                string time4Name = D + "-" + backupSchedule.Time.Date.ToShortDateString() + "-" + backupSchedule.Time.Hour + "." + backupSchedule.Time.Minute + "-";
                #endregion

                backupSchedule.JustName = time4Name + backupSchedule.BackupName + "-" + backupSchedule.DbName;
                backupSchedule.DayOfMonth = backupSchedule.Time.Day;

                if (backupSchedule.BackupScheme != 4)
                {
                    #region time4NameDiffB
                    string BS;
                    if (backupSchedule.BackupScheme == 1)
                    {
                        BS = "Günlük";
                    }
                    else if (backupSchedule.BackupScheme == 2)
                    {
                        BS = "Haftalık";
                    }
                    else if (backupSchedule.BackupScheme == 3)
                    {
                        BS = "Aylık";
                    }
                    else
                    {
                        BS = "Yıllık";
                    }
                    string time4NameDiff = BS + "-" + backupSchedule.DiffTime.Value.Date.ToShortDateString() + "-" + backupSchedule.DiffTime.Value.Hour + "." + backupSchedule.DiffTime.Value.Minute + "-";
                    #endregion
                    backupSchedule.JustDiffName = time4NameDiff + backupSchedule.BackupName + "-" + backupSchedule.DbName;

                }

                //İsim ayarları

                lastBackup = backupSchedule;

                //Form1.cs'de kullanmak için son eklenmesi gereken backup bi değişkene BackupSchedule cinsinde tanımlanır.

                #region Backup Tarih kontrol
                int count = context.BackupSchedules.ToList().Count, i = 0;
                if (count == 0)
                {
                    context.BackupSchedules.Add(backupSchedule);
                    context.SaveChanges();
                }
                else
                {
                    foreach (var item in context.BackupSchedules.ToList())
                    {
                        i++;

                        if (item.IsActive == true && item.IsAuto == true && item.BackupScheme == lastBackup.BackupScheme && item.Time.Hour == lastBackup.Time.Hour && item.Time.Minute == lastBackup.Time.Minute && item.DbName == lastBackup.DbName)
                        {
                            MessageBox.Show("Bu aralıkta bu backup zaten alınıyor. Lütfen kontrol ediniz. Kayıt sağlanamadı.");
                            TxtLog("Hata :  Bu aralıkta bu backup zaten alınıyor.Lütfen kontrol ediniz.Kayıt sağlanamadı. AutoBackup");
                            lastBackup = null;
                            break;
                        }
                        else if (i == count)
                        {
                            context.BackupSchedules.Add(backupSchedule);
                            context.SaveChanges();
                            break;
                        }

                    }
                }
                #endregion
                //Eklenmeye çalışılan backup saatine ve aralığına göre kontrol edilip, eğer aynı saatte ve aralıkta alınan bir backup varsa 
                //tekrar kaydedilmesi engelleniyor.

            }
            catch (Exception e)
            {
                MessageBox.Show("Otomatik Backup alınırken bir hata oluştu lütfen kontrol edin.");
                TxtLog("Hata : " + e.Message + " Otomatik Backup alınırken bir hata oluştu lütfen kontrol edin." + " AutoBackup");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cbAllDatabases.Checked)
            {
                for (int i = 0; i < cbDatabaseName.Items.Count; i++)
                {
                    cbDatabaseName.SelectedItem = cbDatabaseName.Items[i];
                    try
                    {
                        #region Adres kontrolleri
                        if (chbLocal.Checked == true && selectedPath == null)
                        {
                            MessageBox.Show("Lütfen Lokalde Backup alınacak yeri belirtin.");
                            return;
                        }
                        if (cbDriveUsers.SelectedItem != null)
                        {
                            DriveUserName = cbDriveUsers.SelectedItem.ToString();
                        }
                        if (chbGoogle.Checked == true && DriveUserName == null)
                        {
                            MessageBox.Show("Lütfen Google Drive'a giriş yapın.");
                            return;
                        }
                        if (cbFtp.SelectedItem != null)
                        {
                            FtpLoc = cbFtp.SelectedItem.ToString();
                        }
                        if (chbFtp.Checked == true && FtpLoc == null)
                        {
                            MessageBox.Show("Lütfen Ftp adresini girin.");
                            return;
                        }
                        #endregion

                        #region Save kontolü

                        if (chbFtp.Checked == false && chbGoogle.Checked == false && chbLocal.Checked == false)
                        {
                            MessageBox.Show("Lütfen kaydedileceği alanı seçin");
                        }
                        else if (txtName.Text == "")
                        {
                            MessageBox.Show("Lütfen backup ismini verin.");
                        }
                        else if (cbDatabaseName.SelectedIndex < 0)
                        {
                            MessageBox.Show("Lütfen backup alınacak veritabanını seçin");
                        }
                        else
                        {
                            Save();
                            #region Last Backup Control
                            if (lastBackup == null)
                            {
                                return;
                            }
                            #endregion
                        }
                        #endregion

                        listing();

                    }
                    catch (Exception es)
                    {
                        MessageBox.Show("Save alırken hata oluştu.");
                        TxtLog("Hata : " + es.Message + " Save alırken hata oluştu." + " AutoBackup");
                        lastBackup.IsActive = false;
                        context.Update(lastBackup);
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                try
                {
                    #region Adres kontrolleri
                    if (chbLocal.Checked == true && selectedPath == null)
                    {
                        MessageBox.Show("Lütfen Lokalde Backup alınacak yeri belirtin.");
                        return;
                    }
                    if (cbDriveUsers.SelectedItem != null)
                    {
                        DriveUserName = cbDriveUsers.SelectedItem.ToString();
                    }
                    if (chbGoogle.Checked == true && DriveUserName == null)
                    {
                        MessageBox.Show("Lütfen Google Drive'a giriş yapın.");
                        return;
                    }
                    if (cbFtp.SelectedItem != null)
                    {
                        FtpLoc = cbFtp.SelectedItem.ToString();
                    }
                    if (chbFtp.Checked == true && FtpLoc == null)
                    {
                        MessageBox.Show("Lütfen Ftp adresini girin.");
                        return;
                    }
                    #endregion

                    #region Save kontolü

                    if (chbFtp.Checked == false && chbGoogle.Checked == false && chbLocal.Checked == false)
                    {
                        MessageBox.Show("Lütfen kaydedileceği alanı seçin");
                    }
                    else if (txtName.Text == "")
                    {
                        MessageBox.Show("Lütfen backup ismini verin.");
                    }
                    else if (cbDatabaseName.SelectedIndex < 0)
                    {
                        MessageBox.Show("Lütfen backup alınacak veritabanını seçin");
                    }
                    else
                    {
                        Save();
                        #region Last Backup Control
                        if (lastBackup == null)
                        {
                            return;
                        }
                        #endregion
                    }
                    #endregion

                    listing();

                }
                catch (Exception es)
                {
                    MessageBox.Show("Save alırken hata oluştu.");
                    TxtLog("Hata : " + es.Message + " Save alırken hata oluştu." + " AutoBackup");
                    lastBackup.IsActive = false;
                    context.Update(lastBackup);
                    context.SaveChanges();
                }
            }
            
        }
        #region RadioButtons
        private void rbDaily_CheckedChanged(object sender, EventArgs e)
        {
            dtpSetUp.Format = DateTimePickerFormat.Time;
        }

        private void rbWeekly_CheckedChanged(object sender, EventArgs e)
        {
            dtpSetUp.Format = DateTimePickerFormat.Custom;
            dtpSetUp.CustomFormat = "'Haftanın günü :'dddd HH:mm";
        }

        private void rbMonthly_CheckedChanged(object sender, EventArgs e)
        {
            dtpSetUp.Format = DateTimePickerFormat.Custom;
            dtpSetUp.CustomFormat = "'Ayın ' dd '. günü :'   HH:mm";
        }

        private void rbYearly_CheckedChanged(object sender, EventArgs e)
        {
            dtpSetUp.Format = DateTimePickerFormat.Custom;
            dtpSetUp.CustomFormat = "'Yılın ' MMMMM ' ayı ' dd '. günü :'   HH:mm";
        }

        #endregion
        #region Checks
        private void chbGoogle_CheckedChanged(object sender, EventArgs e)
        {
            if (chbGoogle.Checked == true)
            {
                lblGoogle.Visible = true;
                cbDriveUsers.Visible = true;
            }
            else
            {
                lblGoogle.Visible = false;
                cbDriveUsers.Visible = false;
            }
        }

        private void chbFtp_CheckedChanged(object sender, EventArgs e)
        {
            if (chbFtp.Checked == true)
            {
                lblFtp.Visible = true;
                cbFtp.Visible = true;
            }
            else
            {
                lblFtp.Visible = false;
                cbFtp.Visible = false;
            }
        }

        private void cbAllDatabases_CheckedChanged(object sender, EventArgs e)
        {
            if(cbAllDatabases.Checked)
            {
                cbDatabaseName.SelectedItem = null;
                cbDatabaseName.Enabled = false;
            }
            else
            {
                cbDatabaseName.Enabled = true;
            }

        }   

        private void chbLocal_CheckedChanged(object sender, EventArgs e)
        {
            if (chbLocal.Checked == true)
            {
                using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();
                    selectedPath = fbd.SelectedPath;
                }
                if (selectedPath == "")
                {
                    chbLocal.Checked = false;
                    lblAddress.Visible = false;
                }
                else
                {
                    lblAddress.Text = selectedPath;
                    lblAddress.Visible = true;
                }
            }
            else
            {
                lblAddress.Visible = false;
            }
        }
        #endregion
        private void dgBackupSchedule_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgBackupSchedule.CurrentRow.Selected = true;
            foreach (DataGridViewRow dr in dgBackupSchedule.SelectedRows)
            {
                DialogResult dialog = new DialogResult();
                dialog = MessageBox.Show(dr.Cells[0].Value.ToString() + " Backup'ını silmek istediğinize emin misiniz?", "SİL", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    foreach (var item in context.BackupSchedules.ToList())
                    {
                        if (item.JustName == dr.Cells[0].Value.ToString())
                        {
                            item.IsActive = false;
                            context.Update(item);
                            context.SaveChanges();
                        }
                    }
                    MessageBox.Show(dr.Cells[0].Value.ToString() + " Silindi");
                }
                else
                {
                    MessageBox.Show(dr.Cells[0].Value.ToString() + " Silinmedi");
                }
                listing();
            }

        }

    }
}
