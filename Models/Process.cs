using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ApmDbBackupManager.Models
{
    public class Process
    {
        DatabaseContext context = new DatabaseContext();
        List<string> names = new List<string>();
        BackupSchedule lastBackup = new BackupSchedule();
        string selectedPath, DriveUserName, FtpAddress;
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
            catch (Exception error)
            {
                MessageBox.Show("Backup alma başarısız" + error.Message);
                TxtLog("Hata : " + error.Message + " Backup alma başarısız.");
                return false;
            }
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

       /* public void oldRar (BackupSchedule backup)
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

        }*/
    }
}
