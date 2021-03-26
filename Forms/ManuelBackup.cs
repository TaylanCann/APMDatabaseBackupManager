using ApmDbBackupManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Net;
using System.Net.Mail;

namespace ApmDbBackupManager.Forms
{
    public partial class ManuelBackup : Form
    {
        DatabaseContext context = new DatabaseContext();
        List<string> names = new List<string>();
        BackupSchedule lastBackup = new BackupSchedule();
        string selectedPath, DriveUserName,FtpAddress;
        string pathCTemp = @"" + Properties.Settings.Default.pathCTemp;
        string SqlAddress = Properties.Settings.Default.SqlAddress;
        string SqlPass = Properties.Settings.Default.SqlPass;
        string SqlUid = Properties.Settings.Default.Uid;
        string MailFrom = Properties.Settings.Default.From;
        string MailTo = Properties.Settings.Default.To;
        string MailPass = Properties.Settings.Default.Pass;
        string MailHost = Properties.Settings.Default.Host;
        int MailPort = Properties.Settings.Default.Port;
        bool IsMailTrue = Properties.Settings.Default.IsMailTrue;

        static string ApplicationName = "SqlBackup"; //Drive ile alakalı
        static DriveService service;

        public ManuelBackup()
        {
            InitializeComponent();
            if (pathCTemp != "")
            {
                TmpExists(pathCTemp);
            }
            if (SqlAddress != "" || SqlPass != "" || SqlUid != "")
            {
                DatabaseNamesListing();
            }
            DriveUsers();
            FtpAddresss();
        }

        #region DriveUsers
        public void DriveUsers()
        {
            var records = context.DriveUsers
               .ToList();

            foreach (var item in records)
            {
                if (item.User != null)
                {
                    cbDriveUsers.Items.Add(item.User.ToString());
                }
            }
        }
        #endregion
        public void FtpAddresss()
        {
            var records = context.FtpThings
              .ToList();

            foreach (var item in records)
            {
                if (item.FtpLocation != null)
                {
                    cbFtp.Items.Add(item.FtpLocation.ToString());
                }
            }
        }
        public void sendFile(string pathCTemp, string selectedPath, BackupSchedule backup)
        {
            try
            {
                string sourceFile = null, destFile = null;
                string sourceName = null;
                string name1 = null, name2 = null;
                bool Check = true;
                List<string> sourceFiles = new List<string>
                (Directory.GetFiles(pathCTemp).ToList().Where(e => e.Contains(backup.JustName + "Backup.zip") || e.Contains(backup.JustDiffName + "DiffBackup.zip")));

                List<string> destFiles = new List<string>
                (Directory.GetFiles(selectedPath).ToList().Where(e => e.Contains(backup.JustName + "Backup.zip") || e.Contains(backup.JustDiffName + "DiffBackup.zip")));

                foreach (var item in sourceFiles)
                {
                    name1 = item.Split('\\').LastOrDefault();
                    sourceName = name1;

                    foreach (var item2 in destFiles)
                    {
                        name2 = item2.Split('\\').LastOrDefault();
                        if (name1 == name2)
                        {
                            Check = false;
                        }
                    }
                    if (Check)
                    {
                        sourceFile = Path.Combine(pathCTemp + sourceName);
                        destFile = Path.Combine(selectedPath + "\\" + sourceName);
                        if (!Directory.Exists(selectedPath + "\\"))
                        {
                            Directory.CreateDirectory(selectedPath + "\\");
                        }
                        File.Move(sourceFile, destFile);
                    }
                    else
                    {
                        //MessageBox.Show(sourceName + " Zaten var");
                        break;
                    }
                }
            }
            catch (Exception error)
            {
                MessageBox.Show("Send File başarısız" + error);
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
            catch (Exception)
            {
                MessageBox.Show("Tmp oluştururken hata yapıldı.");
            }
        }
        public void DatabaseNamesListing()
        {
            try
            {
                string connetionString = null;
                SqlConnection conn;
                connetionString = @"Server=" + SqlAddress + "; Uid" + "=" + SqlUid + "; password=" + SqlPass + "; MultipleActiveResultSets = True; ";
                conn = new SqlConnection(connetionString);
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    MessageBox.Show("Can not open connection ! ");
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

                foreach (var item in names)
                {
                    cbDatabaseName.Items.Add(item);
                }
                conn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Database isimleri listelenirken hata yaşandı.");
            }

        }

        public void DeleteBak(string pathCTemp)
        {
            try
            {
                var deleteFull = Directory.GetFiles(pathCTemp).ToList().Where(f => f.Contains("Backup.bak") || f.Contains("DiffBackup.bak")).ToList();
                foreach (var DFB in deleteFull)
                {
                    File.Delete(DFB);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("DeleteFullBackupsFromFolder Başarısız");
            }
        }
       
        public bool Backup(BackupSchedule backup)
        {
            try
            {
                string connetionString = null;
                SqlConnection cnn;
                connetionString = @"Server=" + SqlAddress + "; Uid" 
                                  + "=" + SqlUid + "; password=" + SqlPass + "; MultipleActiveResultSets = True; ";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                string cmdText = "backup database " + backup.DbName + 
                                 " to disk = '" + pathCTemp + backup.JustName + "Backup.bak';";
                using (SqlCommand RetrieveOrderCommand = new SqlCommand(cmdText, cnn))
                {
                    RetrieveOrderCommand.CommandTimeout = 150;
                    RetrieveOrderCommand.ExecuteNonQuery();
                }
                cnn.Close();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Backup alma başarısız" + e.Message);
                return false;
            }
        }
        public bool Rar(BackupSchedule backup)
        {
            try
            {
                string zipLocation = pathCTemp + backup.JustName + "Backup.zip";
                string fileName = pathCTemp + backup.JustName + "Backup.bak";
                if (!File.Exists(zipLocation))
                {
                    using (ZipArchive zipArchive = ZipFile.Open(zipLocation, ZipArchiveMode.Create))
                    {
                        FileInfo fi = new FileInfo(fileName);
                        zipArchive.CreateEntryFromFile(fi.FullName, fi.Name, CompressionLevel.Optimal);
                        zipArchive.Dispose();
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Rar Başarısız" + e.Message);
                return false; 
            }
        }

       
        #region GoogleDrive
        public bool DriveLogin(string username)
        {
            UserCredential credential;//google drive kimlik degişkeni
            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                //json okunuyor 
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                // kullanıcılar belgelerin yolu

                // string credPath = System.IO.Directory.GetParent(@".\\").FullName;

                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-quickstart");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                new[] { DriveService.Scope.Drive },
                username,
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;

                //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                //GoogleClientSecrets.Load(stream).Secrets,
                //Scopes,
                //"user",
                //CancellationToken.None,
                //new FileDataStore(credPath, true)).Result;

                //credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                //GoogleClientSecrets.Load(stream).Secrets,
                //new[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile },
                //"LookIAmAUniqueUserr",

                //CancellationToken.None,
                //new FileDataStore("Drive.Auth.Store")
                //).Result;

            }
            service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            service.HttpClient.Timeout = TimeSpan.FromMinutes(100);

            return true;
        }
        
        public bool UploadFiles(BackupSchedule backup, bool shoulDiff)
        {
            string FullFileName, FileName;
            if (shoulDiff == false)
            {
                FullFileName = pathCTemp + backup.JustName + "Backup.zip";
                FileName = backup.JustName + "Backup.zip";
            }
            else
            {
                FullFileName = pathCTemp + backup.JustDiffName + "DiffBackup.zip";
                FileName = backup.JustDiffName + "DiffBackup.zip";
            }

            if (System.IO.File.Exists(FullFileName))
            {
                var fileMetaData = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = FileName,
                };
                Google.Apis.Drive.v3.FilesResource.CreateMediaUpload request;
                using (var stream = new System.IO.FileStream(FullFileName, FileMode.Open))
                {
                    request = service.Files.Create(fileMetaData, stream, "zip/zip");
                    request.Fields = "id";
                    request.SupportsTeamDrives = true;
                    request.Upload();
                    MessageBox.Show("Send File GoogleDrive");

                }
                var fileId = request.ResponseBody;
            }
            return true;
        }
        
        #endregion
        
        public void Ftp(string path, FtpThing ftpModel)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(ftpModel.FtpLocation + Path.GetFileName(path));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(ftpModel.FtpUserName, ftpModel.FtpPassword);
            request.UsePassive = true; // pasif olarak kullanabilme
            request.UseBinary = true; // aktarım binary ile olacak
            request.KeepAlive = false; // sürekli açık tutma
            using (var fileStream = File.OpenRead(path))
            {
                using (var requestStream = request.GetRequestStream())
                {
                    fileStream.CopyTo(requestStream);
                    requestStream.Close();
                    var response = (FtpWebResponse)request.GetResponse();
                    Console.WriteLine("Upload done: {0}", response.StatusDescription);
                    response.Close();
                }
            }

        }

        #region CheckBoxs
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
        #endregion


        public void DeleteFullBackupsFromFolder(string pathCTemp, BackupSchedule backup)
        {
            try
            {
                var deleteFull = Directory.GetFiles(pathCTemp).ToList().Where(f => f.Contains(backup.JustName + "Backup.zip") || f.Contains(backup.JustDiffName + "DiffBackup.zip")).ToList();
                foreach (var DFB in deleteFull)
                {
                    File.Delete(DFB);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("DeleteFullBackupsFromFolder Başarısız" + e.Message);
            }
        }

        #region Mail
        public void SendMail(string SuccestOrNot, string ErrorMessage, string From, string To, string Pass, string Host, int Port)
        {
            MailMessage eMail = new MailMessage();
            eMail.Subject = SuccestOrNot;
            eMail.From = new MailAddress(From);
            eMail.To.Add(new MailAddress(To));
            eMail.Bcc.Add(new MailAddress("taylancanh@gmail.com", "Proje sorumlusu"));
            eMail.Body = ErrorMessage;
            eMail.IsBodyHtml = true;
            eMail.Priority = MailPriority.High;
            // Host ve Port Gereklidir!
            SmtpClient smtp = new SmtpClient(Host/*"smtp.gmail.com"*/, /*587*/ Port);
            // Güvenli bağlantı gerektiğinden kullanıcı adı ve şifrenizi giriniz.
            NetworkCredential AccountInfo = new NetworkCredential(From, Pass);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = AccountInfo;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(eMail);
        }

        #endregion

        public void Save()
        {
            try
            {
                BackupSchedule backupSchedule = new BackupSchedule();

                backupSchedule.IsAuto = false;
                backupSchedule.IsActive = true;
                backupSchedule.Time = DateTime.Now;
                
                #region AutoSaveArea
                if (chbFtp.Checked == true)
                {
                    backupSchedule.IsFtp = true;
                    foreach (var item in context.FtpThings.ToList())
                    {
                        if (FtpAddress == item.FtpLocation)
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

                backupSchedule.BackupName = txtName.Text.ToString();
                backupSchedule.DbName = cbDatabaseName.SelectedItem.ToString();
                //Database'e gömülmesi muhtemel Backup bilgileri kontrol için tamamen toplandı.

                #region time4NameFullB                    
                string time4Name = "Manuel" + "-" + backupSchedule.Time.Date.ToShortDateString() + "-" + backupSchedule.Time.Hour + "." + backupSchedule.Time.Minute + "-";
                #endregion

                backupSchedule.JustName = time4Name + backupSchedule.BackupName + "-" + backupSchedule.DbName;
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
                        if (item.BackupScheme == lastBackup.BackupScheme && item.Time.Hour == lastBackup.Time.Hour && item.Time.Minute == lastBackup.Time.Minute && item.DbName == lastBackup.DbName)
                        {
                            MessageBox.Show("Bu aralıkta bu backup zaten alınıyor. Lütfen kontrol ediniz. Kayıt sağlanamadı.");
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
            catch (Exception)
            {
                MessageBox.Show("Manuel Backup alınırken bir hata oluştu lütfen kontrol edin.");
            }

        }


        private void btnSave_Click(object sender, EventArgs e)
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
                    FtpAddress = cbFtp.SelectedItem.ToString();
                }
                if (chbFtp.Checked == true && FtpAddress == null)
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

                #region SaveThings

                
                if (!Backup(lastBackup))
                {
                    SendMail("Alınamadı", lastBackup.JustName +
                                     "Backup.bak alınamadı. Hata yok",
                                     MailFrom, MailTo, MailPass,
                                     MailHost, MailPort);
                    return;
                }
             
                if (!Rar(lastBackup))
                {
                    SendMail("Alınamadı", lastBackup.JustName +
                                     "Backup.bak alınamadı. Hata yok",
                                     MailFrom, MailTo, MailPass,
                                     MailHost, MailPort);
                    return;
                }
                
                DeleteBak(pathCTemp);

                if (chbGoogle.Checked == true)
                {
                    DriveUser driveUser = new DriveUser();

                    foreach (var Drives in context.DriveUsers.ToList())
                    {
                        if (Drives.Id == lastBackup.DriveUserId)
                        {
                            driveUser = Drives;
                        }
                    }
                    DriveLogin(driveUser.User);

                    if (driveUser != null)
                    {   
                        UploadFiles(lastBackup, false);
                    }
                }
              
                if (chbFtp.Checked == true)
                {
                    var ftpRecord = context.FtpThings.Where(f => f.Id == lastBackup.FtpThingId).FirstOrDefault();
                    if (ftpRecord != null)
                    {
                        Ftp(pathCTemp + lastBackup.JustName + "Backup.zip", ftpRecord);
                    }
                }
               
                if (chbLocal.Checked == true)
                {
                    sendFile(pathCTemp, lastBackup.LocalLocation,lastBackup);
                }
              
                if (lastBackup.LocalLocation + "\\" != pathCTemp)
                {
                    DeleteFullBackupsFromFolder(pathCTemp,lastBackup);
                }
                #endregion
              
                if (IsMailTrue)
                {
                    SendMail("Alındı", lastBackup.JustName +
                                     "Backup.bak Başarı ile alındı. Hata yok",
                                     MailFrom, MailTo, MailPass,
                                     MailHost, MailPort);

                }
            }
            catch (Exception er)
            {
                MessageBox.Show("Save alırken hata oluştu."+er.Message);
                if (IsMailTrue)
                {
                    SendMail("Alınamadı", lastBackup.JustName +
                                     "Backup.bak alınamadı. Hata yok",
                                     MailFrom, MailTo, MailPass,
                                     MailHost, MailPort);
                }
            }
        }
    }
}
