using System;
using System.Collections.Generic;
using System.Text;

namespace ApmDbBackupManager.Models
{
   public class FtpThing
    {  
        public int Id { get; set; }
        public string FtpLocation { get; set; }//Sunucu adresi
        public string FtpPassword { get; set; }//Sunucu şifresi
        public string FtpUserName { get; set; }//Sunucu kullanıcı adı
        public List<BackupSchedule> backupSchedules { get; set; }
    }
}
