﻿using ApmDbBackupManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Windows.Forms;
using System.Timers;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using System.Net;
using System.Drawing;
using System.Net.Mail;


//İki kere aynı kayıdı yapınca patlıyor bak

namespace ApmDbBackupManager
{
    public partial class Form1 : Form
    {
        DatabaseContext context = new DatabaseContext();
        private static System.Timers.Timer aTimer;
        string pathCTemp = @"C:\TempBackup\";
        string SqlAddress = "LAPTOP-9VG06RAO";

        static string[] Scopes = { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
        static string ApplicationName = "SqlBackup"; //Drive ile alakalı
        static DriveService service;

        public Form1()
        {
            InitializeComponent();
            setTimer();
            btnAutoBackup_Click(currentButton, EventArgs.Empty);
        }

        #region Buttons&Forms
        private Button currentButton;
        private void ActiveButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = Color.Blue;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }
            }
        }
        private void DisableButton()
        {
            foreach (Control previousBtn in pnLeft.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                }
            }
        }

        private void btnAutoBackup_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.AutoBackup(), sender);
        }
        private void btnManuelBackup_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.ManuelBackup(), sender);
        }
        private void btnDrive_Click(object sender, EventArgs e)
        {
            OpenChildForm(new DriveForm(), sender);
        }
        private void btnFtp_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Ftp(), sender);
        }
        private void btnInfos_Click(object sender, EventArgs e)
        {

        }
        private Form activeForm;
        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActiveButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.pnForms.Controls.Add(childForm);
            this.pnForms.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }
        #endregion

        #region Timer
        public void setTimer()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }
        public void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            aTimer.Stop();
            foreach (var item in context.BackupSchedules.ToList())
            {
                #region İlk kez FullBackup alıyoru

                if (DateTime.Now > item.Time && item.HaveIt == false)
                {
                    try
                    {
                        #region Edit Datas

                        item.HaveIt = true;
                        context.Update(item);
                        context.SaveChanges();

                        #endregion

                        #region SaveDbTo

                        if (item.IsDrive == true)
                        {
                            DriveUser driveUser = new DriveUser();
                            Backup(item);
                            Rar(item);
                            foreach (var Drives in context.DriveUsers.ToList())
                            {
                                if (Drives.Id == item.DriveUserId)
                                {
                                    driveUser = Drives;
                                }
                            }
                            if (driveUser != null)
                            {
                                DriveLogin(driveUser.User);
                                UploadFiles(item, false);
                                DeleteFullBackupsFromFolder(pathCTemp);
                            }
                        }

                        if (item.IsFtp == true)
                        {
                            Backup(item);
                            Rar(item);
                            var ftpRecord = context.FtpThings.Where(f => f.Id == item.FtpThingId).FirstOrDefault();
                            if (ftpRecord != null)
                            {
                                Ftp(pathCTemp + item.JustName + "Backup.zip", ftpRecord);
                            }
                            DeleteFullBackupsFromFolder(pathCTemp);
                        }

                        if (item.IsLocal == true)
                        {
                            Backup(item);
                            Rar(item);
                            sendFile(pathCTemp, item.LocalLocation);
                        }

                        #endregion
                        SendMail("Alındı", item.JustName + "Backup.bak Başarı ile alındı. Hata yok", "taylancanh@gmail.com", "taylancanh@gmail.com", "smtp.gmail.com",587);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("İlk Backup alınamadı (Have it)");
                        SendMail("Alınamadı", item.JustName + "Backup.bak alınamadı." + "Hata mesajı", "taylancanh@gmail.com", "taylancanh@gmail.com", "smtp.gmail.com", 587);
                    }

                }

                #endregion

                //-------------------------------------------------------------------------------------------------------------------------------------------------------

                #region Yıllık ve dönemi dolmuş backup 

                if (item.BackupScheme == 4 && DateTime.Now > item.Time.AddDays((int)item.DaysAddTerm))
                {
                    try
                    {
                        #region DeleteDiffs
                        if (item.IsDrive == true)
                        {
                            DriveUser driveUser = new DriveUser();

                            foreach (var Drives in context.DriveUsers.ToList())
                            {
                                if (Drives.Id == item.DriveUserId)
                                {
                                    driveUser = Drives;
                                }
                            }
                            DriveLogin(driveUser.User);

                            var deleteDrive = GetFilesToDrive().ToList().Where(f => f.Name.Contains(item.JustName + "Backup.zip"));
                            foreach (var itemDe in deleteDrive)
                            {
                                DeleteFiles(itemDe.Id);
                            }
                        }

                        if (item.IsLocal == true)
                        {
                            var deleteDiff = Directory.GetFiles(item.LocalLocation).ToList().Where
                                (f => f.Contains(item.JustName + "Backup.zip")
                                || f.Contains(item.JustName + "Backup.bak"))
                                .ToList();
                            foreach (var dic in deleteDiff)
                            {
                                File.Delete(dic);
                            }
                        }
                        if (item.IsFtp)
                        {
                            FtpThing ftp = new FtpThing();

                            foreach (var Ftps in context.FtpThings.ToList())
                            {
                                if (Ftps.Id == item.FtpThingId)
                                {
                                    ftp = Ftps;
                                }
                            }
                            string fileFName = item.JustName + "Backup.zip";
                            FtpDeleteFile(fileFName, ftp);
                        }
                        #endregion

                        #region Edit Datas

                        item.Time = DateTime.Now;

                        #region time4NameFullB
                        string D = "Yıllık";
                        string time4Name = D + "-" + item.Time.Date.ToShortDateString() + "-" + item.Time.Hour + "." + item.Time.Minute + "-";

                        #endregion


                        item.JustName = time4Name + item.BackupName + "-" + item.DbName;
                        item.IsDiffBackup = false;
                        context.Update(item);
                        context.SaveChanges();
                        #endregion

                        #region SaveDbTo
                        if (item.IsDrive == true)
                        {
                            DriveUser driveUser = new DriveUser();

                            foreach (var Drives in context.DriveUsers.ToList())
                            {
                                if (Drives.Id == item.DriveUserId)
                                {
                                    driveUser = Drives;
                                }
                            }
                            DriveLogin(driveUser.User);

                            if (driveUser != null)
                            {
                                Backup(item);
                                Rar(item);
                                UploadFiles(item, false);
                                DeleteFullBackupsFromFolder(pathCTemp);
                            }
                        }

                        if (item.IsFtp == true)
                        {
                            Backup(item);
                            Rar(item);
                            var ftpRecord = context.FtpThings.Where(f => f.Id == item.FtpThingId).FirstOrDefault();
                            if (ftpRecord != null)
                            {
                                Ftp(pathCTemp + item.JustName + "Backup.zip", ftpRecord);
                            }
                            DeleteFullBackupsFromFolder(pathCTemp);
                        }
                        if (item.IsLocal == true)
                        {
                            Backup(item);
                            Rar(item);
                            sendFile(pathCTemp, item.LocalLocation);
                        }
                        #endregion
                        SendMail("Alındı", item.JustName + "Backup.bak Başarı ile alındı. Hata yok", "taylancanh@gmail.com", "taylancanh@gmail.com", "smtp.gmail.com", 587);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Yıllık dönemi dolmuş Backup alınamadı.");
                        SendMail("Alınamadı", item.JustName + "Backup.bak alınamadı." + "Hata mesajı", "taylancanh@gmail.com", "taylancanh@gmail.com", "smtp.gmail.com", 587);
                    }
                }

                #endregion

                //-------------------------------------------------------------------------------------------------------------------------------------------------------

                #region Günlük veya haftalık backup

                if (item.DaysAdd != 0 /* Sadece günlükte ve haftalıkta 0'dan farklı */ || item.DaysAddTerm == 365)
                {

                    #region Dönemi dolmuş Full Backup'a dönülüyor 
                    if (item.IsDiffBackup == true && DateTime.Now > item.Time.AddDays((int)item.DaysAddTerm))
                    {
                        try
                        {
                            #region DeleteDiffs
                            if (item.IsDrive == true)
                            {
                                DriveUser driveUser = new DriveUser();

                                foreach (var Drives in context.DriveUsers.ToList())
                                {
                                    if (Drives.Id == item.DriveUserId)
                                    {
                                        driveUser = Drives;
                                    }
                                }
                                DriveLogin(driveUser.User);

                                var deleteDrive = GetFilesToDrive().ToList().Where(f => f.Name.Contains(item.JustDiffName + "DiffBackup.zip") || f.Name.Contains(item.JustName + "Backup.zip"));
                                foreach (var itemDe in deleteDrive)
                                {
                                    DeleteFiles(itemDe.Id);
                                }
                            }

                            if (item.IsLocal == true)
                            {
                                var deleteDiff = Directory.GetFiles(item.LocalLocation).ToList().Where
                                    (f => f.Contains(item.JustDiffName + "DiffBackup.zip")
                                    || f.Contains(item.JustName + "Backup.zip")
                                    || f.Contains(item.JustDiffName + "DiffBackup.bak")
                                    || f.Contains(item.JustName + "Backup.bak"))
                                    .ToList();
                                foreach (var dic in deleteDiff)
                                {
                                    File.Delete(dic);
                                }
                            }
                            if (item.IsFtp)
                            {
                                FtpThing ftp = new FtpThing();

                                foreach (var Ftps in context.FtpThings.ToList())
                                {
                                    if (Ftps.Id == item.FtpThingId)
                                    {
                                        ftp = Ftps;
                                    }
                                }
                                string fileDName = item.JustDiffName + "DiffBackup.zip";
                                FtpDeleteFile(fileDName, ftp);
                                string fileFName = item.JustName + "Backup.zip";
                                FtpDeleteFile(fileFName, ftp);
                            }
                            #endregion

                            #region Edit Datas

                            item.DiffTime = DateTime.Now.AddDays((int)item.DaysAdd);
                            item.Time = DateTime.Now;

                            #region time4NameFullB

                            string D;
                            if (item.BackupScheme == 1)
                            {
                                D = "Günlük";
                            }
                            else if (item.BackupScheme == 2)
                            {
                                D = "Haftalık";
                            }
                            else if (item.BackupScheme == 3)
                            {
                                D = "Aylık";
                            }
                            else
                            {
                                D = "Yıllık";
                            }
                            string time4Name = D + "-" + item.Time.Date.ToShortDateString() + "-" + item.Time.Hour + "." + item.Time.Minute + "-";

                            #endregion
                            #region time4NameDiffB

                            string BS;
                            if (item.BackupScheme == 1)
                            {
                                BS = "Günlük";
                            }
                            else if (item.BackupScheme == 2)
                            {
                                BS = "Haftalık";
                            }
                            else if (item.BackupScheme == 3)
                            {
                                BS = "Aylık";
                            }
                            else
                            {
                                BS = "Yıllık";
                            }
                            string time4NameDiff = BS + "-" + item.DiffTime.Value.Date.ToShortDateString() + "-" + item.DiffTime.Value.Hour + "." + item.DiffTime.Value.Minute + "-";

                            #endregion

                            item.JustName = time4Name + item.BackupName + "-" + item.DbName;
                            item.JustDiffName = time4NameDiff + item.BackupName + "-" + item.DbName;
                            item.IsDiffBackup = false;
                            context.Update(item);
                            context.SaveChanges();
                            #endregion

                            #region SaveDbTo
                            if (item.IsDrive == true)
                            {
                                DriveUser driveUser = new DriveUser();

                                foreach (var Drives in context.DriveUsers.ToList())
                                {
                                    if (Drives.Id == item.DriveUserId)
                                    {
                                        driveUser = Drives;
                                    }
                                }
                                DriveLogin(driveUser.User);

                                if (driveUser != null)
                                {
                                    Backup(item);
                                    Rar(item);
                                    UploadFiles(item, false);
                                    DeleteFullBackupsFromFolder(pathCTemp);
                                }
                            }

                            if (item.IsFtp == true)
                            {
                                Backup(item);
                                Rar(item);
                                var ftpRecord = context.FtpThings.Where(f => f.Id == item.FtpThingId).FirstOrDefault();
                                if (ftpRecord != null)
                                {
                                    Ftp(pathCTemp + item.JustName + "Backup.zip", ftpRecord);
                                }
                                DeleteFullBackupsFromFolder(pathCTemp);
                            }
                            if (item.IsLocal == true)
                            {
                                Backup(item);
                                Rar(item);
                                sendFile(pathCTemp, item.LocalLocation);
                            }
                            #endregion
                            SendMail("Alındı", item.JustName + "Backup.bak Başarı ile alındı. Hata yok", "taylancanh@gmail.com", "taylancanh@gmail.com", "smtp.gmail.com", 587);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Dönemi dolmuş günlük yada haftalık Full Backup alınamadı");
                            SendMail("Alınamadı", item.JustName + "Backup.bak alınamadı." + "Hata mesajı", "taylancanh@gmail.com", "taylancanh@gmail.com", "smtp.gmail.com", 587);
                        }
                    }

                    #endregion


                    #region DiffBackup dönemi dolmuş tekrar DiffBackup alınıyor

                    else if (item.IsDiffBackup == true && DateTime.Now > item.DiffTime)
                    {
                        try
                        {
                            #region DeleteDiffs

                            if (item.IsDrive == true)
                            {
                                DriveUser driveUser = new DriveUser();

                                foreach (var Drives in context.DriveUsers.ToList())
                                {
                                    if (Drives.Id == item.DriveUserId)
                                    {
                                        driveUser = Drives;
                                    }
                                }
                                DriveLogin(driveUser.User);

                                var deleteDrive = GetFilesToDrive().ToList().Where(f => f.Name.Contains(item.JustDiffName + "DiffBackup.zip"));
                                foreach (var itemDe in deleteDrive)
                                {
                                    DeleteFiles(itemDe.Id);
                                }
                            }

                            if (item.IsLocal)
                            {
                                var deleteDiff = Directory.GetFiles(item.LocalLocation).ToList().Where(f => f.Contains(item.JustDiffName + "DiffBackup.bak") || f.Contains(item.JustDiffName + "DiffBackup.zip")).ToList();
                                foreach (var dic in deleteDiff)
                                {
                                    File.Delete(dic);
                                }
                            }

                            if (item.IsFtp)
                            {
                                FtpThing ftp = new FtpThing();

                                foreach (var Ftps in context.FtpThings.ToList())
                                {
                                    if (Ftps.Id == item.FtpThingId)
                                    {
                                        ftp = Ftps;
                                    }
                                }
                                string filename = item.JustDiffName + "DiffBackup.zip";
                                FtpDeleteFile(filename, ftp);
                            }

                            #endregion

                            #region time4NameDiffB
                            string BS;
                            if (item.BackupScheme == 1)
                            {
                                BS = "Günlük";
                            }
                            else if (item.BackupScheme == 2)
                            {
                                BS = "Haftalık";
                            }
                            else if (item.BackupScheme == 3)
                            {
                                BS = "Aylık";
                            }
                            else
                            {
                                BS = "Yıllık";
                            }
                            string time4NameDiff = BS + "-" + item.DiffTime.Value.ToShortDateString() + "-" + item.DiffTime.Value.Hour + "." + item.DiffTime.Value.Minute + "-";
                            item.JustDiffName = time4NameDiff + item.BackupName + "-" + item.DbName;

                            context.Update(item);
                            context.SaveChanges();
                            #endregion

                            #region SaveDbTo
                            if (item.IsDrive == true)
                            {
                                DriveUser driveUser = new DriveUser();

                                foreach (var Drives in context.DriveUsers.ToList())
                                {
                                    if (Drives.Id == item.DriveUserId)
                                    {
                                        driveUser = Drives;
                                    }
                                }
                                DriveLogin(driveUser.User);

                                if (driveUser != null)
                                {
                                    DifferentialBackup(item);
                                    DiffRar(item);
                                    UploadFiles(item, true);
                                    DeleteFullBackupsFromFolder(pathCTemp);
                                }
                            }

                            if (item.IsFtp == true)
                            {
                                DifferentialBackup(item);
                                DiffRar(item);
                                var ftpRecord = context.FtpThings.Where(f => f.Id == item.FtpThingId).FirstOrDefault();
                                if (ftpRecord != null)
                                {
                                    Ftp(pathCTemp + item.JustDiffName + "DiffBackup.zip", ftpRecord);
                                }
                                DeleteFullBackupsFromFolder(pathCTemp);
                            }

                            if (item.IsLocal == true)
                            {
                                DifferentialBackup(item);
                                DiffRar(item);
                                sendFile(pathCTemp, item.LocalLocation);
                            }
                            #endregion

                            #region Edit Datas
                            item.DiffTime = DateTime.Now.AddDays((int)item.DaysAdd);
                            context.Update(item);
                            context.SaveChanges();
                            #endregion
                            SendMail("Alındı", item.JustName + 
                                     "DiffBackup.bak Başarı ile alındı. Hata yok", 
                                     "taylancanh@gmail.com", 
                                     "taylancanh@gmail.com", 
                                     "smtp.gmail.com", 
                                     587);

                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Dönemsel Günlük yada Haftalık Backup alınamadı. ");
                            SendMail("Alınamadı", item.JustName + 
                                "DiffBackup.bak alınamadı." + "Hata mesajı", 
                                "taylancanh@gmail.com", 
                                "taylancanh@gmail.com", 
                                "smtp.gmail.com", 
                                587);
                        }


                    }
                    #endregion


                    #region İlk kez FullBackup'tan Diff alınıyor 

                    else if (item.IsDiffBackup == false && DateTime.Now > item.DiffTime)
                    {
                        try
                        {
                            #region SaveFromDb

                            if (item.IsDrive == true)
                            {
                                DriveUser driveUser = new DriveUser();
                                DifferentialBackup(item);
                                DiffRar(item);

                                foreach (var Drives in context.DriveUsers.ToList())
                                {
                                    if (Drives.Id == item.DriveUserId)
                                    {
                                        driveUser = Drives;
                                    }
                                }
                                if (driveUser != null)
                                {
                                    DriveLogin(driveUser.User);
                                    UploadFiles(item, true);
                                    DeleteFullBackupsFromFolder(pathCTemp);
                                }

                            }

                            if (item.IsFtp == true)
                            {
                                FtpThing ObjectFtp = new FtpThing();
                                DifferentialBackup(item);
                                DiffRar(item);
                                foreach (var Ftps in context.FtpThings.ToList())
                                {
                                    if (Ftps.Id == item.FtpThingId)
                                    {
                                        ObjectFtp = Ftps;
                                    }
                                }
                                if (ObjectFtp != null)
                                {
                                    Ftp(pathCTemp + item.JustDiffName + "DiffBackup.zip", ObjectFtp);
                                }
                                DeleteFullBackupsFromFolder(pathCTemp);
                            }

                            if (item.IsLocal == true)
                            {
                                DifferentialBackup(item);
                                DiffRar(item);
                                sendFile(pathCTemp, item.LocalLocation);
                            }

                            #endregion

                            #region Edit Datas Diff
                            item.IsDiffBackup = true;
                            item.DiffTime = DateTime.Now.AddDays((int)item.DaysAdd);
                            context.Update(item);
                            context.SaveChanges();
                            #endregion
                            SendMail("Alındı", item.JustName + 
                                     "DiffBackup.bak Başarı ile alındı. Hata yok", 
                                     "taylancanh@gmail.com", 
                                     "taylancanh@gmail.com", 
                                     "smtp.gmail.com", 
                                     587);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Dönemsel Günlük yada Haftalık ilk Backup alınamadı. ");
                            SendMail("Alınamadı", item.JustName + 
                                "DiffBackup.bak alınamadı." + "Hata mesajı", 
                                "taylancanh@gmail.com", 
                                "taylancanh@gmail.com", 
                                "smtp.gmail.com", 
                                587);

                        }
                    }
                    #endregion
                }

                #endregion

                //---------------------------------------------------------------------------------------------------------------------------------------------------

                #region Aylık Backup

                else if (item.MonthAdd != 0)
                {
                    //Ayın 28-29-30-31. günleri sıkıntı çözzzzzz 
                    #region Dönemi dolmuş FullBackup'a dönülüyor

                    if (item.IsDiffBackup == true && DateTime.Now > item.Time.AddMonths((int)item.MonthAddTerm))
                    {
                        try
                        {
                            #region DeleteDiffs

                            if (item.IsDrive == true)
                            {
                                DriveUser driveUser = new DriveUser();

                                foreach (var Drives in context.DriveUsers.ToList())
                                {
                                    if (Drives.Id == item.DriveUserId)
                                    {
                                        driveUser = Drives;
                                    }
                                }
                                DriveLogin(driveUser.User);

                                var deleteDrive = GetFilesToDrive().ToList().Where(f => f.Name.Contains(item.JustDiffName + "DiffBackup.zip") || f.Name.Contains(item.JustName + "Backup.zip"));
                                foreach (var itemDe in deleteDrive)
                                {
                                    DeleteFiles(itemDe.Id);
                                }
                            }

                            if (item.IsLocal == true)
                            {
                                var deleteDiff = Directory.GetFiles(item.LocalLocation).ToList().Where
                                (f => f.Contains(item.JustDiffName + "DiffBackup.zip")
                                || f.Contains(item.JustName + "Backup.zip")
                                || f.Contains(item.JustDiffName + "DiffBackup.bak")
                                || f.Contains(item.JustName + "Backup.bak"))
                                .ToList();
                                foreach (var dic in deleteDiff)
                                {
                                    File.Delete(dic);
                                }
                            }

                            if (item.IsFtp)
                            {
                                FtpThing ftp = new FtpThing();

                                foreach (var Ftps in context.FtpThings.ToList())
                                {
                                    if (Ftps.Id == item.FtpThingId)
                                    {
                                        ftp = Ftps;
                                    }
                                }
                                string fileDName = item.JustDiffName + "DiffBackup.zip";
                                FtpDeleteFile(fileDName, ftp);
                                string fileFName = item.JustName + "Backup.zip";
                                FtpDeleteFile(fileFName, ftp);
                            }
                            #endregion

                            #region Edit Datas

                            if (item.DayOfMonth > DateTime.DaysInMonth(item.DiffTime.Value.AddMonths((int)item.MonthAdd).Year, item.DiffTime.Value.AddMonths((int)item.MonthAdd).Month))
                            {
                                item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);
                            }
                            else if (item.DayOfMonth == DateTime.DaysInMonth(item.DiffTime.Value.AddMonths((int)item.MonthAdd).Year, item.DiffTime.Value.AddMonths((int)item.MonthAdd).Month))
                            {
                                if (item.DiffTime.Value.AddMonths((int)item.MonthAdd).Day < item.DayOfMonth)
                                {
                                    item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);

                                    for (; ; )
                                    {
                                        item.DiffTime = item.DiffTime.Value.AddDays(1);
                                        if (item.DiffTime.Value.Day == item.DayOfMonth)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else if (item.DiffTime.Value.AddMonths((int)item.MonthAdd).Day == item.DayOfMonth)
                                {
                                    item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);
                                }
                            }
                            else
                            {
                                item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);
                                item.DiffTime = item.DiffTime.Value.AddDays(item.DayOfMonth - item.DiffTime.Value.Day);
                            }

                            item.Time = DateTime.Now;

                            #region time4NameFullB

                            string D;
                            if (item.BackupScheme == 1)
                            {
                                D = "Günlük";
                            }
                            else if (item.BackupScheme == 2)
                            {
                                D = "Haftalık";
                            }
                            else if (item.BackupScheme == 3)
                            {
                                D = "Aylık";
                            }
                            else
                            {
                                D = "Yıllık";
                            }
                            string time4Name = D + "-" + item.Time.Date.ToShortDateString() + "-" + item.Time.Hour + "." + item.Time.Minute + "-";

                            #endregion

                            if (item.BackupScheme != 4)
                            {
                                #region time4NameDiffB

                                string BS;
                                if (item.BackupScheme == 1)
                                {
                                    BS = "Günlük";
                                }
                                else if (item.BackupScheme == 2)
                                {
                                    BS = "Haftalık";
                                }
                                else if (item.BackupScheme == 3)
                                {
                                    BS = "Aylık";
                                }
                                else
                                {
                                    BS = "Yıllık";
                                }
                                string time4NameDiff = BS + "-" + item.DiffTime.Value.Date.ToShortDateString() + "-" + item.DiffTime.Value.Hour + "." + item.DiffTime.Value.Minute + "-";

                                #endregion
                                item.JustDiffName = time4NameDiff + item.BackupName + "-" + item.DbName;
                            }

                            item.JustName = time4Name + item.BackupName + "-" + item.DbName;
                            item.IsDiffBackup = false;
                            context.Update(item);
                            context.SaveChanges();
                            #endregion

                            #region SaveDbTo

                            if (item.IsDrive == true)
                            {
                                DriveUser driveUser = new DriveUser();

                                foreach (var Drives in context.DriveUsers.ToList())
                                {
                                    if (Drives.Id == item.DriveUserId)
                                    {
                                        driveUser = Drives;
                                    }
                                }
                                DriveLogin(driveUser.User);

                                if (driveUser != null)
                                {
                                    Backup(item);
                                    Rar(item);
                                    UploadFiles(item, false);
                                    DeleteFullBackupsFromFolder(pathCTemp);
                                }
                            }

                            if (item.IsFtp == true)
                            {
                                Backup(item);
                                Rar(item);
                                var ftpRecord = context.FtpThings.Where(f => f.Id == item.FtpThingId).FirstOrDefault();
                                if (ftpRecord != null)
                                {
                                    Ftp(pathCTemp + item.JustName + "Backup.zip", ftpRecord);
                                }
                                DeleteFullBackupsFromFolder(pathCTemp);
                            }

                            if (item.IsLocal == true)
                            {
                                Backup(item);
                                Rar(item);
                                sendFile(pathCTemp, item.LocalLocation);
                            }

                            #endregion
                            SendMail("Alındı", item.JustName + 
                                     "Backup.bak Başarı ile alındı. Hata yok", 
                                     "taylancanh@gmail.com", 
                                     "taylancanh@gmail.com", 
                                     "smtp.gmail.com", 
                                     587);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Dönemi dolmuş haftalık Full Backup alınamadı");
                            SendMail("Alınamadı", item.JustName + 
                                     "Backup.bak alınamadı." + "Hata mesajı", 
                                     "taylancanh@gmail.com", 
                                     "taylancanh@gmail.com", 
                                     "smtp.gmail.com", 
                                      587);
                        }
                    }
                    #endregion

                    #region DiffBackup'tan DiffBackup'a 

                    else if (item.IsDiffBackup == true && DateTime.Now > item.DiffTime)
                    {
                        try
                        {
                            #region DeleteDiffs

                            if (item.IsDrive == true)
                            {
                                DriveUser driveUser = new DriveUser();

                                foreach (var Drives in context.DriveUsers.ToList())
                                {
                                    if (Drives.Id == item.DriveUserId)
                                    {
                                        driveUser = Drives;
                                    }
                                }
                                DriveLogin(driveUser.User);
                                var deleteDrive = GetFilesToDrive().ToList().Where(f => f.Name.Contains(item.JustDiffName + "DiffBackup.zip"));
                                foreach (var itemDe in deleteDrive)
                                {
                                    DeleteFiles(itemDe.Id);
                                }
                            }

                            if (item.IsLocal)
                            {
                                var deleteDiff = Directory.GetFiles(item.LocalLocation).ToList().Where(f => f.Contains(item.JustDiffName + "DiffBackup.bak") || f.Contains(item.JustDiffName + "DiffBackup.zip")).ToList();
                                foreach (var dic in deleteDiff)
                                {
                                    File.Delete(dic);
                                }

                            }

                            if (item.IsFtp)
                            {
                                FtpThing ftp = new FtpThing();

                                foreach (var Ftps in context.FtpThings.ToList())
                                {
                                    if (Ftps.Id == item.FtpThingId)
                                    {
                                        ftp = Ftps;
                                    }
                                }
                                string fileDName = item.JustDiffName + "DiffBackup.zip";
                                FtpDeleteFile(fileDName, ftp);

                            }
                            #endregion

                            #region time4NameDiffB

                            string BS;
                            if (item.BackupScheme == 1)
                            {
                                BS = "Günlük";
                            }
                            else if (item.BackupScheme == 2)
                            {
                                BS = "Haftalık";
                            }
                            else if (item.BackupScheme == 3)
                            {
                                BS = "Aylık";
                            }
                            else
                            {
                                BS = "Yıllık";
                            }
                            string time4NameDiff = BS + "-" + DateTime.Now.ToShortDateString() + "-" + DateTime.Now.Hour + "." + DateTime.Now.Minute + "-";
                            item.JustDiffName = time4NameDiff + item.BackupName + "-" + item.DbName;

                            context.Update(item);
                            context.SaveChanges();

                            #endregion

                            #region SaveDbTo

                            if (item.IsDrive == true)
                            {
                                DriveUser driveUser = new DriveUser();

                                foreach (var Drives in context.DriveUsers.ToList())
                                {
                                    if (Drives.Id == item.DriveUserId)
                                    {
                                        driveUser = Drives;
                                    }
                                }
                                DriveLogin(driveUser.User);

                                if (driveUser != null)
                                {
                                    DifferentialBackup(item);
                                    DiffRar(item);
                                    UploadFiles(item, true);
                                    DeleteFullBackupsFromFolder(pathCTemp);
                                }
                            }

                            if (item.IsFtp == true)
                            {
                                DifferentialBackup(item);
                                DiffRar(item);
                                var ftpRecord = context.FtpThings.Where(f => f.Id == item.FtpThingId).FirstOrDefault();
                                if (ftpRecord != null)
                                {
                                    Ftp(pathCTemp + item.JustDiffName + "DiffBackup.zip", ftpRecord);
                                }
                                DeleteFullBackupsFromFolder(pathCTemp);
                            }
                            if (item.IsLocal == true)
                            {
                                DifferentialBackup(item);
                                DiffRar(item);
                                sendFile(pathCTemp, item.LocalLocation);
                            }
                            #endregion

                            #region Edit Datas
                            if (item.DayOfMonth > DateTime.DaysInMonth(item.DiffTime.Value.AddMonths((int)item.MonthAdd).Year, item.DiffTime.Value.AddMonths((int)item.MonthAdd).Month))
                            {
                                item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);
                            }
                            else if (item.DayOfMonth == DateTime.DaysInMonth(item.DiffTime.Value.AddMonths((int)item.MonthAdd).Year, item.DiffTime.Value.AddMonths((int)item.MonthAdd).Month))
                            {
                                if (item.DiffTime.Value.AddMonths((int)item.MonthAdd).Day < item.DayOfMonth)
                                {
                                    item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);

                                    for (; ; )
                                    {
                                        item.DiffTime = item.DiffTime.Value.AddDays(1);
                                        if (item.DiffTime.Value.Day == item.DayOfMonth)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else if (item.DiffTime.Value.AddMonths((int)item.MonthAdd).Day == item.DayOfMonth)
                                {
                                    item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);
                                }
                            }
                            else
                            {
                                item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);
                                item.DiffTime = item.DiffTime.Value.AddDays(item.DayOfMonth - item.DiffTime.Value.Day);
                            }

                            context.Update(item);
                            context.SaveChanges();
                            #endregion
                            SendMail("Alındı", item.JustName + 
                                     "DiffBackup.bak Başarı ile alındı. Hata yok", 
                                     "taylancanh@gmail.com", 
                                     "taylancanh@gmail.com", 
                                     "smtp.gmail.com", 
                                      587);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Dönemi dolmuş haftalık Full Backup alınamadı");
                            SendMail("Alınamadı", item.JustName + 
                                     "DiffBackup.bak alınamadı." + "Hata mesajı", 
                                     "taylancanh@gmail.com", 
                                     "taylancanh@gmail.com", 
                                     "smtp.gmail.com", 
                                      587);
                        }
                    }

                    #endregion

                    #region İlk kez FullBackup'tan Diff alınıyor

                    else if (item.IsDiffBackup == false && DateTime.Now > item.DiffTime)
                    {
                        try
                        {
                            #region SaveDbTo

                            if (item.IsDrive == true)
                            {
                                DriveUser driveUser = new DriveUser();
                                DifferentialBackup(item);
                                DiffRar(item);

                                foreach (var Drives in context.DriveUsers.ToList())
                                {
                                    if (Drives.Id == item.DriveUserId)
                                    {
                                        driveUser = Drives;
                                    }
                                }
                                if (driveUser != null)
                                {
                                    DriveLogin(driveUser.User);
                                    UploadFiles(item, true);
                                    DeleteFullBackupsFromFolder(pathCTemp);
                                }

                            }

                            if (item.IsFtp == true)
                            {
                                FtpThing ObjectFtp = new FtpThing();
                                DifferentialBackup(item);
                                DiffRar(item);
                                foreach (var Ftps in context.FtpThings.ToList())
                                {
                                    if (Ftps.Id == item.FtpThingId)
                                    {
                                        ObjectFtp = Ftps;
                                    }
                                }
                                if (ObjectFtp != null)
                                {
                                    Ftp(pathCTemp + item.JustDiffName + "DiffBackup.zip", ObjectFtp);
                                }
                                DeleteFullBackupsFromFolder(pathCTemp);
                            }

                            if (item.IsLocal == true)
                            {
                                DifferentialBackup(item);
                                DiffRar(item);
                                sendFile(pathCTemp, item.LocalLocation);
                            }

                            #endregion

                            #region Edit Datas Diff
                            item.IsDiffBackup = true;

                            if (item.DayOfMonth > DateTime.DaysInMonth(item.DiffTime.Value.AddMonths((int)item.MonthAdd).Year, item.DiffTime.Value.AddMonths((int)item.MonthAdd).Month))
                            {
                                item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);
                            }
                            else if (item.DayOfMonth == DateTime.DaysInMonth(item.DiffTime.Value.AddMonths((int)item.MonthAdd).Year, item.DiffTime.Value.AddMonths((int)item.MonthAdd).Month))
                            {
                                if (item.DiffTime.Value.AddMonths((int)item.MonthAdd).Day < item.DayOfMonth)
                                {
                                    item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);

                                    for (; ; )
                                    {
                                        item.DiffTime = item.DiffTime.Value.AddDays(1);
                                        if (item.DiffTime.Value.Day == item.DayOfMonth)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else if (item.DiffTime.Value.AddMonths((int)item.MonthAdd).Day == item.DayOfMonth)
                                {
                                    item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);
                                }
                            }
                            else
                            {
                                item.DiffTime = item.DiffTime.Value.AddMonths((int)item.MonthAdd);
                                item.DiffTime = item.DiffTime.Value.AddDays(item.DayOfMonth - item.DiffTime.Value.Day);
                            }

                            context.Update(item);
                            context.SaveChanges();
                            #endregion
                            SendMail("Alındı", item.JustName + 
                                     "DiffBackup.bak Başarı ile alındı. Hata yok", 
                                     "taylancanh@gmail.com", 
                                     "taylancanh@gmail.com", 
                                     "smtp.gmail.com", 
                                      587);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Dönemi dolmuş haftalık Full Backup alınamadı");
                            SendMail("Alınamadı", item.JustName + 
                                     "DiffBackup.bak alınamadı." + "Hata mesajı", 
                                     "taylancanh@gmail.com", 
                                     "taylancanh@gmail.com", 
                                     "smtp.gmail.com", 
                                      587);
                        }
                    }
                    #endregion
                }
                #endregion
            }
            aTimer.Start();
        }
        #endregion

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

        #region Mail
        public void SendMail(string SuccestOrNot, string ErrorMessage, string From, string To,string Host,int Port)
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
            NetworkCredential AccountInfo = new NetworkCredential("taylancanh@gmail.com", "Istisna-iHareket1");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = AccountInfo;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(eMail);
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
        private string FtpDeleteFile(string fileName, FtpThing ftpModel)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpModel.FtpLocation + fileName);
            request.Method = WebRequestMethods.Ftp.DeleteFile;
            request.Credentials = new NetworkCredential(ftpModel.FtpUserName, ftpModel.FtpPassword);

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.StatusDescription;
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
        public void DifferentialBackup(BackupSchedule backup)
        {
            try
            {
                string connetionString = null;
                SqlConnection cnn;
                connetionString = @"Data Source=" + SqlAddress + ";Integrated Security=True;MultipleActiveResultSets=True;Connection Timeout=900;";
                cnn = new SqlConnection(connetionString);
                cnn.Open();
                string cmdText = "backup database " + backup.DbName + " to disk = '" + pathCTemp + backup.JustDiffName + "DiffBackup.bak' with differential; ";
                using (SqlCommand RetrieveOrderCommand = new SqlCommand(cmdText, cnn))
                {
                    RetrieveOrderCommand.CommandTimeout = 150;
                    RetrieveOrderCommand.ExecuteNonQuery();
                }
                cnn.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Diff Backup alma başarısız");
            }

        }
        public void DiffRar(BackupSchedule backup)
        {
            try
            {
                string zipLocation = pathCTemp + backup.JustDiffName + "DiffBackup.zip";
                string fileName = pathCTemp + backup.JustDiffName + "DiffBackup.bak";
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
                MessageBox.Show("DiffRar Başarısız");
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


        #region Form1Load
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            aTimer.Stop();
            aTimer.Dispose();
        }


        #endregion

        
    }
}
