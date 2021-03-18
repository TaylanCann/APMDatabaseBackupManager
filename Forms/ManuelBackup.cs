using ApmDbBackupManager.Models;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Net;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ApmDbBackupManager.Forms
{
    public partial class ManuelBackup : Form
    {
        DatabaseContext context = new DatabaseContext();
        List<string> names = new List<string>();
        BackupSchedule lastBackup = new BackupSchedule();
        // private static System.Timers.Timer aTimer;
        string pathCTemp = @"C:\TempBackup\";
        string selectedPath, DriveUserName,FtpAddress;
        string SqlAddress = "LAPTOP-9VG06RAO";


        static string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
        static string ApplicationName = "SqlBackup"; //Drive ile alakalı
        static DriveService service;

        public ManuelBackup()
        {
            InitializeComponent();
            TmpExists(pathCTemp);
            DatabaseNamesListing();
            DriveUsers();
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
                connetionString = @"Data Source=" + SqlAddress + ";Integrated Security=True";
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

        public void Backup(BackupSchedule backup)
        {
            try
            {
                string connetionString = null;
                SqlConnection cnn;
                connetionString = @"Data Source=" + SqlAddress + ";Integrated Security=True;MultipleActiveResultSets=True;Connection Timeout=900;";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                string cmdText = "backup database " + backup.DbName + " to disk = '" + pathCTemp + backup.JustName + "Backup.bak';";
                //string cmd = @"BACKUP DATABASE [ Identity1 ] TO  DISK = N' YedekIdentity1.bak'" + " WITH NOFORMAT, NOINIT,  NAME = N'" + value.backup_day_value_database + value.time_backup_mono + ".bak' ,SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
                using (SqlCommand RetrieveOrderCommand = new SqlCommand(cmdText, cnn))
                {
                    RetrieveOrderCommand.CommandTimeout = 150;
                    RetrieveOrderCommand.ExecuteNonQuery();
                }
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Backup alma başarısız");
            }
        }
        public void Rar(BackupSchedule backup)
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
            }
            catch (Exception)
            {
                MessageBox.Show("Rar Başarısız");
            }
        }
        public void sendFile(string pathCTemp, string selectedPath)
        {
            try
            {
                string[] files = Directory.GetFiles(pathCTemp);

                foreach (var item in files)
                {
                    string[] name = item.Split('\\');
                    string sourceFile = Path.Combine(pathCTemp + name[2]);
                    string destFile = Path.Combine(selectedPath + "\\" + name[2]);
                    if (!Directory.Exists(selectedPath + "\\"))
                    {
                        Directory.CreateDirectory(selectedPath + "\\");
                    }

                    File.Move(sourceFile, destFile);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Send File Başarısız");
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

                MessageBox.Show("Login Olundu");
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
        public bool CreateDirectory(string folderName)
        {
            var body = new Google.Apis.Drive.v3.Data.File();
            body.Name = folderName;
            body.MimeType = "application/vnd.google-apps.folder";
            try
            {
                var request = service.Files.Create(body);
                request.Fields = "id";
                var _FF = request.Execute();
            }
            catch (Exception e)
            {
                MessageBox.Show("Hata Oluştu. : " + e.Message);
            }
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
        public void DeleteFiles(string fileId)
        {
            FilesResource.DeleteRequest deleteRequest = service.Files.Delete(fileId);
            deleteRequest.Execute();
        }
        public IList<Google.Apis.Drive.v3.Data.File> GetFilesToDrive()
        {
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 100;
            listRequest.Fields = "nextPageToken, files(id, name)";

            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute().Files;

            return files;
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

        public void DeleteFullBackupsFromFolder(string pathCTemp)
        {
            try
            {
                var deleteFull = Directory.GetFiles(pathCTemp).ToList().Where(f => f.Contains("Backup.bak") || f.Contains("Backup.zip")).ToList();
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

        public void Save()
        {
            try
            {
                BackupSchedule backupSchedule = new BackupSchedule();

                backupSchedule.IsAuto = false;
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
                MessageBox.Show("Otomatik Backup alınırken bir hata oluştu lütfen kontrol edin.");
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
                        Backup(lastBackup);
                        Rar(lastBackup);
                        UploadFiles(lastBackup, false);
                        DeleteFullBackupsFromFolder(pathCTemp);
                    }
                }
                if (chbFtp.Checked == true)
                {
                    Backup(lastBackup);
                    Rar(lastBackup);
                    var ftpRecord = context.FtpThings.Where(f => f.Id == lastBackup.FtpThingId).FirstOrDefault();
                    if (ftpRecord != null)
                    {
                        Ftp(pathCTemp + lastBackup.JustName + "Backup.zip", ftpRecord);
                    }
                    DeleteFullBackupsFromFolder(pathCTemp);
                }
                if (chbLocal.Checked == true)
                {
                    Backup(lastBackup);
                    Rar(lastBackup);
                    sendFile(pathCTemp, lastBackup.LocalLocation);
                }

                #endregion
            }
            catch (Exception)
            {
                MessageBox.Show("Save alırken hata oluştu.");
            }
        }
    }
}
