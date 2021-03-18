using System;
using System.Collections.Generic;
using System.Text;

namespace ApmDbBackupManager.Models
{
    public class DriveUser
    {
        public int Id { get; set; } 
        public string User { get; set; }
        public List<BackupSchedule> BackupSchedules { get; set; }
        
    }
}
