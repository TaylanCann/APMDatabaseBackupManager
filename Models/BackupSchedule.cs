using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ApmDbBackupManager.Models
{
    public class BackupSchedule
    {
        public int Id { get; set; }
        public string BackupName { get; set; }
        //BackupName kullanıcın isteği doğrultusunda verdiği isim
        public bool IsAuto { get; set; }//Alınacak Backup'ın manuel mi otomatik mi alınacağını belirtir.
        public DateTime Time { get; set; }//Database'in FullBackup alındığı tarihler
        public int? BackupScheme { get; set; }
        //Backupscheme == 1 Daily
        //Backupscheme == 2 Weekly
        //Backupscheme == 3 Monthly
        //Backupscheme == 4 Yearly
        public string DbName { get; set; }
        //DbName orijinal isimdir. Yedeğini aldığımız database'in SQL'deki kayıtlı adı.
        public int? DaysAdd { get; set; } //DiffBackup almak için geçmesi gereken gün sayısı
        public int? MonthAdd { get; set; }//DiffBackup almak için geçmesi gereken ay sayısı.
        public int DayOfMonth { get; set; }//Ayın günü.
        public int? MonthAddTerm { get; set; }//Full Backup almak için geçmesi gereken ay sayısı.
        public int? DaysAddTerm { get; set; }//Full Backup almak için geçmesi gereken gün sayısı
        public bool IsDiffBackup { get; set; } //Alınan Backup Diff mi değil mi kontrol sistemi.
        public DateTime? DiffTime { get; set; } // Diff Backup zamanı
        public bool HaveIt { get; set; }//Daha önce Auto Backup alındı mı?
        public bool IsDrive { get; set; }//Otomatik olarak Drive'a yedeklensin mi
        public bool IsFtp { get; set; }//Otomatik olarak sunucuya yedeklensin mi
        public bool IsLocal { get; set; }//Otomatik olarak local'e yedeklesin mi
        public string JustName {get;set;}//Kaydedileceği isim
        public string JustDiffName { get; set; }//Kaydedileceği Diff isim
        public string LocalLocation { get; set; }//Local'de kaydedileceği yer
        public FtpThing FtpThing { get; set; }
        public int? FtpThingId { get; set; }
        public int? DriveUserId { get; set; }
        public DriveUser DriveUser { get; set; }

    }
}
